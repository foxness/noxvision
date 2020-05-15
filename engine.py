import numpy as np
import cv2
import dlib
import imutils

class Detector:
    def __init__(self, width, height, confidence_threshold = 0.4):
        self.prototxt_path = 'mobilenet_ssd\\MobileNetSSD_deploy.prototxt'
        self.model_path = 'mobilenet_ssd\\MobileNetSSD_deploy.caffemodel'

        self.CLASSES = ["background", "aeroplane", "bicycle", "bird", "boat",
            "bottle", "bus", "car", "cat", "chair", "cow", "diningtable",
            "dog", "horse", "motorbike", "person", "pottedplant", "sheep",
            "sofa", "train", "tvmonitor"]

        self.net = cv2.dnn.readNetFromCaffe(self.prototxt_path, self.model_path)

        self.confidence_threshold = confidence_threshold
        self.width = width
        self.height = height

    def detect(self, frame):
        blob = cv2.dnn.blobFromImage(frame, 0.007843, (self.width, self.height), 127.5)

        self.net.setInput(blob)
        output = self.net.forward()

        detections = []
        for i in np.arange(0, output.shape[2]):
            confidence = output[0, 0, i, 2]

            if confidence > self.confidence_threshold:
                idx = int(output[0, 0, i, 1])
                label = self.CLASSES[idx]

                box = output[0, 0, i, 3:7] * np.array([self.width, self.height, self.width, self.height])
                (startX, startY, endX, endY) = box.astype("int")
                detections.append({ 'label': label, 'rect': [startX, startY, endX, endY] })
        
        return detections

class Tracker:
    def __init__(self):
        self.t = dlib.correlation_tracker()
    
    def frame2rgb(self, frame):
        return cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    
    def start(self, frame, rect):
        rgb_frame = self.frame2rgb(frame)

        (startX, startY, endX, endY) = rect
        dlib_rect = dlib.rectangle(startX, startY, endX, endY)
        self.t.start_track(rgb_frame, dlib_rect)
    
    def get_rect(self):
        position = self.t.get_position()

        startX = int(position.left())
        startY = int(position.top())
        endX = int(position.right())
        endY = int(position.bottom())

        return (startX, startY, endX, endY)
    
    def update(self, frame):
        rgb_frame = self.frame2rgb(frame)
        self.t.update(rgb_frame)

class Engine:
    def __init__(self):
        self.detector = None
        self.trackers = []
        self.labels = []
        self.rects = []
    
    def update(self, frame):
        self.rects.clear()

        frame = imutils.resize(frame, width = 600)

        if self.detector is None:
            (frame_height, frame_width) = frame.shape[:2]
            detector = Detector(frame_width, frame_height)
        
        if len(self.trackers) == 0:
            detections = detector.detect(frame)
            for detection in detections:
                label = detection['label']
                rect = detection['rect']

                tracker = Tracker()
                tracker.start(frame, rect)

                self.trackers.append(tracker)
                self.labels.append(label)
                self.rects.append(rect)
        else:
            for tracker in self.trackers:
                rect = tracker.update(frame)
                self.rects.append(rect)