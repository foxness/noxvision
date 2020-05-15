import numpy as np
import cv2
import dlib

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
    
    def start(self, rgb_frame, rect):
        (startX, startY, endX, endY) = rect
        dlib_rect = dlib.rectangle(startX, startY, endX, endY)
        self.t.start_track(rgb_frame, dlib_rect)
    
    def update(self, rgb_frame):
        self.t.update(rgb_frame)

        position = self.t.get_position()

        startX = int(position.left())
        startY = int(position.top())
        endX = int(position.right())
        endY = int(position.bottom())

        return (startX, startY, endX, endY)