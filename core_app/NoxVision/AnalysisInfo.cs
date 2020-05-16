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
        public Label label { get; set; }
        public IList<int> rect { get; set; }
    }
    public class AnalysisInfo
    {
        public IList<IList<RecognizedObject>> frames { get; set; }
    }
}
