using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    public enum Label
    {
        background, aeroplane, bicycle, bird, boat,
        bottle, bus, car, cat, chair, cow, diningtable,
        dog, horse, motorbike, person, pottedplant, sheep,
        sofa, train, tvmonitor
    }

    public class RecognizedObject
    {
        public int id { get; set; }
        public Label label { get; set; }
        public IList<int> rect { get; set; }
    }

    public class Face
    {
        public IList<double> embedding { get; set; }
        public IList<int> rect { get; set; }
    }

    public class Frame
    {
        public IList<RecognizedObject> objs { get; set; }
        public IList<Face> faces { get; set; }
    }
    public class AnalysisInfo
    {
        public IList<Frame> frames { get; set; }
    }
}
