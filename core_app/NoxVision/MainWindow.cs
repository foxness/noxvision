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
    public partial class MainWindow : Form
    {
        private static readonly string analysisEnginePath = @"R:\my\projects\noxvision\analysis_engine\main.py";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchEngine(string inputPath)
        {
            var process = new Process();
            process.StartInfo.FileName = "python";
            process.StartInfo.Arguments = $"{analysisEnginePath} -i \"{inputPath}\" -ov output.avi -oa analysis.json";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            MessageBox.Show("done");
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
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "Video files (*.mp4;*.avi)|*.mp4;*.avi";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;

                    ToggleProgressbar(true);
                    LaunchEngine(filePath);

                    //LaunchEngine

                    ////Read the contents of the file into a stream
                    //var fileStream = ofd.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
        }
    }
}
