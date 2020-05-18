from engine import *
import numpy as np

analyzer = Analyzer()

file = open("C:\\Users\\Rivershy\\Desktop\\19CF4F8D83B959C3D338AD319DB830CE7C1177327255431408DED11AEB988DB5.json", 'r') 
rawjson = file.read() 
file.close()

analyzer.deserialize(rawjson)

face_embeddings = []
for frame in analyzer.faces:
    for face in frame:
        face_embeddings.append(face.embedding)

for i in range(20):
    eps = 0.3 + (0.7 - 0.3) / 20 * i
    min_samples = 2
    face_clusterer = FaceClusterer(eps = eps, min_samples = min_samples)
    face_clusterer.faces = face_embeddings
    clusters = face_clusterer.get_clusters()
    # print(clusters)
    print("eps: {:.3f}, min_samples: {}, count: {}".format(eps, min_samples, len(np.unique(clusters))))
    # print(np.unique(clusters))

