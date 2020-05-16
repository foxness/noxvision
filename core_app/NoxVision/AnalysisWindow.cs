using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class AnalysisWindow : Form
    {
        private static readonly string analysisEnginePath = @"R:\my\projects\noxvision\analysis_engine\main.py";
        private static readonly string analysisEngineDir = @"R:\my\projects\noxvision\analysis_engine\";
        public string FilePath { get; set; }

        public AnalysisWindow()
        {
            InitializeComponent();
        }

        private void LaunchEngine()
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Users\Rivershy\AppData\Local\Programs\Python\Python38\python.exe";
            process.StartInfo.Arguments = $"{analysisEnginePath} -i {FilePath} -ov output.avi -oa analysis.json";
            process.StartInfo.WorkingDirectory = analysisEngineDir;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                // frame 30/1283 (2%)
                var line = e.Data;
                if (string.IsNullOrEmpty(line))
                {
                    return;
                }

                var i1 = line.IndexOf('(');
                if (i1 == -1)
                {
                    return;
                }

                var i2 = line.IndexOf('%');
                var percentageString = line.Substring(i1 + 1, i2 - i1 - 1);
                var percentage = Int32.Parse(percentageString);

                progressBar.Invoke((MethodInvoker)delegate
                {
                    progressBar.Value = percentage;
                });
            });

            process.Start();
            process.BeginOutputReadLine();
        }

        private void AnalysisWindow_Load(object sender, EventArgs e)
        {
            LaunchEngine();
        }
    }
}
