from imutils.video import FPS
import numpy as np
import argparse
import imutils
import dlib
import cv2

from engine import *

# ---------------------------------------------------------------------------

draw = True
input_video = "R:\\my\\drive\\sync\\things\\projects\\noxvisioncloud\\people-counting-opencv\\videos\\example_01.mp4"
export_analysis = 'analysis.json'
export_video = 'output.avi'
confidence_threshold = 0.4
skip_frames = 30

# ---------------------------------------------------------------------------

# ---------------------------------------------------------------------------

# ap = argparse.ArgumentParser()
# ap.add_argument("-i", "--input", type=str,
#     help="path to optional input video file")
# ap.add_argument("-o", "--output", type=str,
#     help="path to optional output video file")
# ap.add_argument("-c", "--confidence", type=float, default=0.4,
#     help="minimum probability to filter weak detections")
# ap.add_argument("-s", "--skip-frames", type=int, default=30,
#     help="# of skip frames between detections")
# args = vars(ap.parse_args())

# ---------------------------------------------------------------------------

def draw_box(frame, startX, startY, endX, endY):
    cv2.rectangle(frame, (startX, startY), (endX, endY), (0, 255, 0), 2)

def draw_box_text(frame, startX, startY, endX, endY, text):
    draw_box(frame, startX, startY, endX, endY)
    cv2.putText(frame, text, (startX, startY - 15), cv2.FONT_HERSHEY_SIMPLEX, 0.45, (0, 255, 0), 2)

def main():
    detector = Detector()

    print("[INFO] opening video file...")
    video = cv2.VideoCapture(input_video)

    writer = None
    trackers = []
    labels = []
    W = None
    H = None
    analysis = { 'frames': [] }
    frames_processed = 0
    frame_count = int(video.get(cv2.CAP_PROP_FRAME_COUNT))
    fps = FPS().start()
    rects = []

    while True:
        rects.clear()
        (grabbed, frame) = video.read()

        if frame is None:
            # end of video
            break

        frame = imutils.resize(frame, width = 600)
        rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

        if W is None or H is None:
            (H, W) = frame.shape[:2]

        if export_video is not None and writer is None:
            fourcc = cv2.VideoWriter_fourcc(*"MJPG")
            writer = cv2.VideoWriter(export_video, fourcc, 30, (W, H), True)

        if frames_processed % skip_frames == 0:
            print("frame {}/{} ({:.0%})".format(frames_processed, frame_count, frames_processed / frame_count))

        if len(trackers) == 0:
            status = "Detecting"

            blob = cv2.dnn.blobFromImage(frame, 0.007843, (W, H), 127.5)
            net.setInput(blob)
            detections = net.forward()

            for i in np.arange(0, detections.shape[2]):
                confidence = detections[0, 0, i, 2]

                if confidence > confidence_threshold:
                    idx = int(detections[0, 0, i, 1])
                    label = CLASSES[idx]

                    if label != "person":
                        continue

                    box = detections[0, 0, i, 3:7] * np.array([W, H, W, H])
                    (startX, startY, endX, endY) = box.astype("int")

                    tracker = dlib.correlation_tracker()
                    rect = dlib.rectangle(startX, startY, endX, endY)
                    tracker.start_track(rgb, rect)

                    labels.append(label)
                    trackers.append(tracker)

                    draw_box_text(frame, startX, startY, endX, endY, label)
        else:
            for (t, l) in zip(trackers, labels):
                status = "Tracking"

                t.update(rgb)
                pos = t.get_position()

                startX = int(pos.left())
                startY = int(pos.top())
                endX = int(pos.right())
                endY = int(pos.bottom())

                rects.append((startX, startY, endX, endY))
                draw_box_text(frame, startX, startY, endX, endY, l)

        info = [
            ("Status", status)
        ]

        for (i, (k, v)) in enumerate(info):
            text = "{}: {}".format(k, v)
            cv2.putText(frame, text, (10, H - ((i * 20) + 20)),
                cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 0, 255), 2)

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

main()