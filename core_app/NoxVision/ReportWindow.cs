using System;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class ReportWindow : Form
    {
        private string reportFilepath;
        private string videoFilepath;
        private AnalysisInfo analysis;

        public ReportWindow(string reportFilepath, string videoFilepath, AnalysisInfo analysisInfo)
        {
            InitializeComponent();

            this.reportFilepath = reportFilepath;
            this.videoFilepath = videoFilepath;
            this.analysis = analysisInfo;
        }

        private void ReportWindow_Load(Object sender, EventArgs e)
        {
            Console.WriteLine(reportFilepath);
        }
    }
}
