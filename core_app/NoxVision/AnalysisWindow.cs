using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class AnalysisWindow : Form
    {
        private static readonly string analysisEnginePath = @"R:\my\projects\noxvision\analysis_engine\main.py";
        private static readonly string analysisEngineDir = @"R:\my\projects\noxvision\analysis_engine\";
        private static readonly string progressFilename = @"progress";
        private static readonly string analysisFilename = @"analysis.json";

        public string OutputAnalysisFilepath { get; set; }
        public string FilePath { get; set; }

        public AnalysisWindow()
        {
            InitializeComponent();
        }

        public void DoWork(IProgress<int> progress)
        {
            var process = LaunchProcess();

            var progressFile = Path.Combine(analysisEngineDir, progressFilename);
            while (!process.HasExited)
            {
                if (File.Exists(progressFile))
                {
                    var progressString = File.ReadAllText(progressFile);
                    var progressInt = Int32.Parse(progressString);

                    progress.Report(progressInt);
                }

                Thread.Sleep(1000);
            }

            // progress.Report(progressBar.Maximum);
        }

        private async void LaunchEngine()
        {
            progressBar.Value = 0;
            var progress = new Progress<int>(v =>
            {
                progressBar.Value = v;
            });

            await Task.Run(() => DoWork(progress));

            DialogResult = DialogResult.OK;
            OutputAnalysisFilepath = Path.Combine(analysisEngineDir, analysisFilename);
            Close();
        }

        private Process LaunchProcess()
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Users\Rivershy\AppData\Local\Programs\Python\Python38\python.exe";
            process.StartInfo.Arguments = $"{analysisEnginePath} -i {FilePath} -ov output.avi -oa analysis.json";
            process.StartInfo.WorkingDirectory = analysisEngineDir;
            process.StartInfo.CreateNoWindow = true;
            //process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            //process.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            //{
            //    // frame 30/1283 (2%)
            //    var line = e.Data;
            //    if (string.IsNullOrEmpty(line))
            //    {
            //        return;
            //    }

            //    var i1 = line.IndexOf('(');
            //    if (i1 == -1)
            //    {
            //        return;
            //    }

            //    var i2 = line.IndexOf('%');
            //    var percentageString = line.Substring(i1 + 1, i2 - i1 - 1);
            //    var percentage = Int32.Parse(percentageString);

            //    progressBar.Invoke((MethodInvoker)delegate
            //    {
            //        progressBar.Value = percentage;
            //    });
            //});

            process.Start();
            //process.BeginOutputReadLine();

            return process;
        }

        private void AnalysisWindow_Load(object sender, EventArgs e)
        {
            LaunchEngine();
        }
    }
}
