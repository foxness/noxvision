using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class MainWindow : Form
    {
        private static readonly string analysisEnginePath = @"R:\my\projects\noxvision\analysis_engine\main.py";
        private static readonly string analysisEngineDir = @"R:\my\projects\noxvision\analysis_engine\";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchEngine(string inputPath)
        {
            var process = new Process();
            process.StartInfo.FileName = @"C:\Users\Rivershy\AppData\Local\Programs\Python\Python38\python.exe";
            process.StartInfo.Arguments = $"{analysisEnginePath} -i {inputPath} -ov output.avi -oa analysis.json";
            process.StartInfo.WorkingDirectory = analysisEngineDir;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.EnableRaisingEvents = true;

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

        private void ToggleProgressbar(bool run)
        {
            progressBar.Visible = run;
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (var ofd = new OpenFileDialog())
            {
                // ofd.InitialDirectory = "c:\\";
                ofd.InitialDirectory = @"R:\my\drive\sync\things\projects\noxvisioncloud\people - counting - opencv\videos";
                ofd.Filter = "Video files (*.mp4;*.avi)|*.mp4;*.avi";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;

                    ToggleProgressbar(true);

                    LaunchEngine(filePath);
                }
            }
        }
    }
}
