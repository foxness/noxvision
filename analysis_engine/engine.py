import numpy as np
import cv2
import dlib
import imutils
import json
from sklearn.preprocessing import LabelEncoder
from sklearn.svm import SVC
from sklearn.cluster import DBSCAN

class FaceClusterer:
    def __init__(self, eps = 0.56, min_samples = 2):
        self.faces = []
        self.dbscan = DBSCAN(eps = eps, min_samples = min_samples)
    
    def get_clusters(self):
        return self.dbscan.fit_predict(self.faces).tolist()

class Analyzer:
    def __init__(self):
        self.face_clusterer = FaceClusterer()
        self.objects = []
        self.faces = []
    
    def add_frame(self, objs, faces):
        self.objects.append(objs)
        self.faces.append(faces)
    
    def cluster_faces(self):
        faces = []
        for frame in self.faces:
            for face in frame:
                faces.append(face)
        
        embeddings = [face.embedding for face in faces]
        self.face_clusterer.faces = embeddings
        clusters = self.face_clusterer.get_clusters()

        for (i, face) in enumerate(faces):
            face.cluster = clusters[i]
    
    def analyze(self):
        self.cluster_faces()
    
    def serialize(self):
        contents = { 'frames': [] }

        for (objs, faces) in zip(self.objects, self.faces):
            frame = { 'objs': [], 'faces': [] }
            for obj in objs:
                serialized_obj = { 'id': obj.id, 'label': obj.label, 'rect': obj.rect.tolist() }
                frame['objs'].append(serialized_obj)
            
            for face in faces:
                serialized_face = { 'embedding': face.embedding.tolist(), 'rect': face.rect.tolist(), 'cluster': face.cluster }
                frame['faces'].append(serialized_face)
            
            contents['frames'].append(frame)
        
        return json.dumps(contents)
    
    def deserialize(self, raw):
        self.__init__()

        jsn = json.loads(raw)
        for sframe in jsn['frames']:
            sobjs = sframe['objs']
            objs = []
            for sobj in sobjs:
                obj = RecognizedObject()
                obj.rect = sobj['rect']
                obj.id = sobj['id']
                obj.label = sobj['label']

                objs.append(obj)
            
            self.objects.append(objs)

            sfaces = sframe['faces']
            faces = []
            for sface in sfaces:
                face = Face()
                face.embedding = np.array(sface['embedding'])
                face.rect = sface['rect']
                face.cluster = sface['cluster']

                faces.append(face)
            
            self.faces.append(faces)

class FaceRecognizer:
    def __init__(self, confidence_threshold = 0.4):
        self.recognizer = SVC(C = 1.0, kernel = "linear", probability = True)
        self.le = LabelEncoder()

    def fit(self, names, embeddings):
        labels = self.le.fit_transform(names)
        self.recognizer.fit(embeddings, labels)

class FaceDetector:
    def __init__(self, width, height, confidence_threshold = 0.4):
        self.prototxt_path = 'models\\face_detector_model.prototxt'
        self.model_path = 'models\\face_detector_model.caffemodel'

        self.net = cv2.dnn.readNetFromCaffe(self.prototxt_path, self.model_path)

        self.confidence_threshold = confidence_threshold
        self.width = width
        self.height = height

    def detect(self, frame):
        blob = cv2.dnn.blobFromImage(frame, 1.0, (300, 300), (104.0, 177.0, 123.0), swapRB=False, crop=False)

        self.net.setInput(blob)
        output = self.net.forward()

        detections = []
        for i in np.arange(0, output.shape[2]):
            confidence = output[0, 0, i, 2]

            if confidence > self.confidence_threshold:
                box = output[0, 0, i, 3:7] * np.array([self.width, self.height, self.width, self.height])
                (startX, startY, endX, endY) = box.astype("int")

                if startX < 0 or startY < 0 or endX >= self.width or endY >= self.height:
                    continue

                detections.append([startX, startY, endX, endY])
        
        return detections

class FaceEmbedder:
    def __init__(self):
        self.model_path = 'models\\openface_nn4.small2.v1.t7'

        self.net = cv2.dnn.readNetFromTorch(self.model_path)

    def get_embedding(self, frame, face_rect):
        (startX, startY, endX, endY) = face_rect
        face = frame[startY:endY, startX:endX]

        # (fH, fW) = face.shape[:2]
        # if fW < 20 or fH < 20:
        #     return 'face too small'

        blob = cv2.dnn.blobFromImage(face, 1.0 / 255, (96, 96), (0, 0, 0), swapRB=True, crop=False)
        self.net.setInput(blob)
        embedding = self.net.forward().flatten()

        return embedding

class Detector:
    def __init__(self, width, height, confidence_threshold = 0.5):
        self.prototxt_path = 'models\\object_detector_model.prototxt'
        self.model_path = 'models\\object_detector_model.caffemodel'

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
        self.id = None
        self.label = None
        self.rect = None
        self.tracker = None

class Face:
    def __init__(self):
        self.rect = None
        self.embedding = None
        self.cluster = None

class Engine:
    def __init__(self, width, height, detection_period = 30, confidence_threshold = 0.5, face_confidence_threshold = 0.5):
        self.orig_width = width
        self.orig_height = height

        self.desired_w = 500
        self.desired_h = None
        self.scale = None

        self.detection_period = detection_period
        self.confidence_threshold = confidence_threshold
        self.face_confidence_threshold = face_confidence_threshold
        self.frames_processed = 0

        self.nextId = 0
        self.objects = []
        self.faces = []

        self.calculate_desired()
        self.detector = Detector(self.desired_w, self.desired_h, confidence_threshold = self.confidence_threshold)
        self.face_detector = FaceDetector(self.desired_w, self.desired_h, confidence_threshold = self.face_confidence_threshold)
        self.face_embedder = FaceEmbedder()
    
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
            newobj.id = obj.id
            newobj.label = obj.label
            newobj.rect = self.scale_to_orig(obj.rect)
            newobjs.append(newobj)
        
        return newobjs
    
    def get_faces(self):
        newfaces = []
        for face in self.faces:
            newface = Face()
            newface.rect = self.scale_to_orig(face.rect)
            newface.embedding = face.embedding
            newfaces.append(newface)
        
        return newfaces
    
    def detect_objects(self, frame):
        detections = self.detector.detect(frame)
        objs = []
        for detection in detections:
            label = detection['label']
            rect = detection['rect']

            tracker = Tracker()
            tracker.start(frame, rect)

            obj = RecognizedObject()
            obj.id = self.nextId
            self.nextId += 1
            obj.tracker = tracker
            obj.label = label
            obj.rect = rect
            objs.append(obj)
        
        return objs
    
    def detect_faces(self, frame):
        detections = self.face_detector.detect(frame)

        faces = []
        for face_rect in detections:
            embedding = self.face_embedder.get_embedding(frame, face_rect)

            face = Face()
            face.rect = face_rect
            face.embedding = embedding
            faces.append(face)
        
        return faces
    
    def process(self, frame):
        self.faces.clear()
        frame = self.scale_to_desired(frame)
        
        if self.frames_processed % self.detection_period == 0:
            self.objects = self.detect_objects(frame)
            self.faces = self.detect_faces(frame)
        else:
            for obj in self.objects:
                obj.tracker.update(frame)
                obj.rect = obj.tracker.get_rect()
        
        self.frames_processed += 1