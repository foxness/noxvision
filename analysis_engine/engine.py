import numpy as np
import cv2
import dlib
import imutils
import json

class Serializer:
    def __init__(self):
        self.contents = None
        self.start()
    
    def start(self):
        self.contents = { 'frames': [] }
    
    def process(self, objs):
        frame = []
        for obj in objs:
            serialized_obj = { 'label': obj.label, 'rect': obj.rect.tolist() }
            frame.append(serialized_obj)
        
        self.contents['frames'].append(frame)
    
    def serialize(self):
        return json.dumps(self.contents)

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

class RecognizedObject:
    def __init__(self):
        self.label = None
        self.rect = None
        self.tracker = None

class Engine:
    def __init__(self, width, height, detection_period = 30):
        self.orig_width = width
        self.orig_height = height

        self.desired_w = 500
        self.desired_h = None
        self.scale = None

        self.detection_period = detection_period
        self.frames_processed = 0

        self.objects = []

        self.calculate_desired()
        self.detector = Detector(self.desired_w, self.desired_h)
    
    def scale_to_desired(self, frame):
        dim = (self.desired_w, self.desired_h)
        resized = cv2.resize(frame, dim, interpolation = cv2.INTER_AREA)

        return resized
    
    def scale_to_orig(self, arr):
        return (np.array(arr) / self.scale).astype('int')
    
    def calculate_desired(self):
        if self.desired_w is None:
            self.scale = self.desired_h / float(self.orig_height)
            self.desired_w = int(self.orig_width * self.scale)
        else:
            self.scale = self.desired_w / float(self.orig_width)
            self.desired_h = int(self.orig_height * self.scale)
    
    def get_objects(self):
        newobjs = []
        for obj in self.objects:
            newobj = RecognizedObject()
            newobj.label = obj.label
            newobj.rect = self.scale_to_orig(obj.rect)
            newobjs.append(newobj)
        
        return newobjs
    
    def process(self, frame):
        frame = self.scale_to_desired(frame)
        
        if self.frames_processed % self.detection_period == 0:
            self.objects.clear()
            detections = self.detector.detect(frame)
            for detection in detections:
                label = detection['label']
                rect = detection['rect']

                tracker = Tracker()
                tracker.start(frame, rect)

                obj = RecognizedObject()
                obj.tracker = tracker
                obj.label = label
                obj.rect = rect
                self.objects.append(obj)
        else:
            for obj in self.objects:
                obj.tracker.update(frame)
                obj.rect = obj.tracker.get_rect()
        
        self.frames_processed += 1