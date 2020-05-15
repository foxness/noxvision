from imutils.video import FPS
import numpy as np
import argparse
import imutils
import dlib
import cv2

from engine import *

# ---------------------------------------------------------------------------

draw = False
skip_frames = 30

# ---------------------------------------------------------------------------

ap = argparse.ArgumentParser()

# ap.add_argument("-i", "--input", type = str, help = "path to input video file", required = True)
# ap.add_argument("-ov", "--outputvideo", type = str, help = "path to optional output video file", required = False)
# ap.add_argument("-oa", "--outputanalysis", type = str, help = "path to optional output analysis file", required = False)

ap.add_argument("-i", "--input", type = str, help = "path to input video file", required = False, default = "R:\\my\\drive\\sync\\things\\projects\\noxvisioncloud\\people-counting-opencv\\videos\\example_01.mp4")
ap.add_argument("-ov", "--outputvideo", type = str, help = "path to optional output video file", required = False, default = 'output.avi')
ap.add_argument("-oa", "--outputanalysis", type = str, help = "path to optional output analysis file", required = False, default = 'analysis.json')

args = vars(ap.parse_args())

# ---------------------------------------------------------------------------

input_video = args['input']
output_video = args['outputvideo']
output_analysis = args['outputanalysis']

# ---------------------------------------------------------------------------

def draw_box(frame, rect):
    (startX, startY, endX, endY) = rect
    cv2.rectangle(frame, (startX, startY), (endX, endY), (0, 255, 0), 2)

def draw_box_text(frame, rect, text):
    draw_box(frame, rect)
    (startX, startY, _, _) = rect
    cv2.putText(frame, text, (startX, startY - 15), cv2.FONT_HERSHEY_SIMPLEX, 0.45, (0, 255, 0), 2)

def main():
    video = cv2.VideoCapture(input_video)
    frame_count = int(video.get(cv2.CAP_PROP_FRAME_COUNT))
    frame_width = int(video.get(cv2.CAP_PROP_FRAME_WIDTH))
    frame_height = int(video.get(cv2.CAP_PROP_FRAME_HEIGHT))

    engine = Engine(frame_width, frame_height)
    writer = None
    frames_processed = 0

    if output_analysis:
        serializer = Serializer()

    fps = FPS().start()

    while True:
        (grabbed, frame) = video.read()

        if frame is None:
            # end of video
            break

        engine.process(frame)
        objs = engine.get_objects()
        for obj in objs:
            draw_box_text(frame, obj.rect, obj.label)
        
        if output_analysis:
            serializer.process(objs)

        if frame_width is None or frame_height is None:
            (frame_height, frame_width) = frame.shape[:2]
            detector = Detector(frame_width, frame_height)

        if output_video is not None and writer is None:
            fourcc = cv2.VideoWriter_fourcc(*"MJPG")
            writer = cv2.VideoWriter(output_video, fourcc, 30, (frame_width, frame_height), True)

        if frames_processed % skip_frames == 0:
            print("frame {}/{} ({:.0%})".format(frames_processed, frame_count, frames_processed / frame_count))

        # info = [
        #     ("Status", status)
        # ]

        # for (i, (k, v)) in enumerate(info):
        #     text = "{}: {}".format(k, v)
        #     cv2.putText(frame, text, (10, frame_height - ((i * 20) + 20)),
        #         cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 0, 255), 2)

        if writer is not None:
            writer.write(frame)

        if draw:
            cv2.imshow("Frame", frame)

        key = cv2.waitKey(1) & 0xFF
        if key == ord("q"):
            break

        frames_processed += 1
        fps.update()

    fps.stop()
    print("[INFO] elapsed time: {:.2f}".format(fps.elapsed()))
    print("[INFO] approx. FPS: {:.2f}".format(fps.fps()))

    if writer is not None:
        writer.release()

    video.release()

    if draw:
        cv2.destroyAllWindows()
    
    if output_analysis:
        serialized = serializer.serialize()
        file = open(output_analysis, 'w')
        file.write(serialized)
        file.close()

main()