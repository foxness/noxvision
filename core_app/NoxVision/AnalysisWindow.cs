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

        public void LaunchAndReadProgressFile(IProgress<int> progress)
        {
            var confidenceThreshold = new SettingsDb().ConfidenceThreshold;
            var process = LaunchEngine(confidenceThreshold);

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

        private async void LaunchEngineLoop()
        {
            progressBar.Value = 0;
            var progress = new Progress<int>(v =>
            {
                progressBar.Value = v;
            });

            await Task.Run(() => LaunchAndReadProgressFile(progress));

            DialogResult = DialogResult.OK;
            OutputAnalysisFilepath = Path.Combine(analysisEngineDir, analysisFilename);
            Close();
        }

        private Process LaunchEngine(int confidenceThreshold)
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Users\Rivershy\AppData\Local\Programs\Python\Python38\python.exe";
            process.StartInfo.Arguments = $"{analysisEnginePath} -i \"{FilePath}\" -ov output.avi -oa analysis.json -oct {confidenceThreshold}";
            process.StartInfo.WorkingDirectory = analysisEngineDir;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();

            return process;
        }

        private void AnalysisWindow_Load(object sender, EventArgs e)
        {
            LaunchEngineLoop();
        }
    }
}
