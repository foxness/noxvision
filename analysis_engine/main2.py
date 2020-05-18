from engine import *

analyzer = Analyzer()

file = open("C:\\Users\\Rivershy\\Desktop\\19CF4F8D83B959C3D338AD319DB830CE7C1177327255431408DED11AEB988DB5.json", 'r') 
rawjson = file.read() 
file.close()

analyzer.deserialize(rawjson)
a = 1