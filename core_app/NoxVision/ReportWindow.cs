using System;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private void GenerateReport()
        {
            var report = rg.GenerateReport();
            report.Save(reportFilepath, ImageFormat.Png);
        }

        private async void ReportWindow_Shown(Object sender, EventArgs e)
        {
            var thread = new Thread(GenerateReport);
            thread.Start();

            await Task.Factory.StartNew(() => GenerateReport());
            Close();
        }
    }
}
