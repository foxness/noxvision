using System;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class ReportWindow : Form
    {
        private string reportFilepath;
        private ReportGenerator rg;

        public ReportWindow(string reportFilepath, string videoFilepath, AnalysisInfo analysisInfo)
        {
            InitializeComponent();
            
            this.reportFilepath = reportFilepath;
            rg = new ReportGenerator(videoFilepath, analysisInfo);
        }

        private void ReportWindow_Load(Object sender, EventArgs e)
        {
            var report = rg.GenerateReport();
            report.Save(reportFilepath, ImageFormat.Png);

            Close();
        }
    }
}
