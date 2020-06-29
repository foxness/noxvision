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
        private static readonly string analysisEngineDirname = "analysis_engine";
        private static readonly string analysisEngineFilename = @"main.py";
        private static readonly string progressFilename = @"progress";
        private static readonly string analysisFilename = @"analysis.json";

        private string analysisEngineDirectory;
        private string analysisEngineFilepath;

        public string OutputAnalysisFilepath { get; set; }
        public string FilePath { get; set; }

        public AnalysisWindow()
        {
            var executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directoryPath = Path.GetDirectoryName(executablePath);

            analysisEngineDirectory = Path.Combine(directoryPath, analysisEngineDirname);
            analysisEngineFilepath = Path.Combine(analysisEngineDirectory, analysisEngineFilename);

            InitializeComponent();
        }

        public void LaunchAndReadProgressFile(IProgress<int> progress)
        {
            var settings = new SettingsDb();

            var confidenceThreshold = settings.ConfidenceThreshold;
            var faceConfidenceThreshold = settings.FaceConfidenceThreshold;

            var process = LaunchEngine(confidenceThreshold, faceConfidenceThreshold);

            var progressFile = Path.Combine(analysisEngineDirectory, progressFilename);
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
            OutputAnalysisFilepath = Path.Combine(analysisEngineDirectory, analysisFilename);
            Close();
        }

        private Process LaunchEngine(int confidenceThreshold, int faceConfidenceThreshold)
        {
            var args = $"{analysisEngineFilepath} -i \"{FilePath}\" -oa analysis.json -oct {confidenceThreshold} -fct {faceConfidenceThreshold}";

            var process = new Process();
            process.StartInfo.FileName = @"python";
            process.StartInfo.Arguments = args;
            process.StartInfo.WorkingDirectory = analysisEngineDirectory;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = true;

            process.Start();

            return process;
        }

        private void AnalysisWindow_Load(object sender, EventArgs e)
        {
            LaunchEngineLoop();
        }
    }
}
