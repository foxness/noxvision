using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoxVision
{
    public partial class SettingsWindow : Form
    {
        private SettingsDb settings;

        public SettingsWindow()
        {
            InitializeComponent();

            settings = new SettingsDb();
        }

        private void UpdateConfidenceLabel()
        {
            confidenceLabel.Text = settings.ConfidenceThreshold.ToString();
        }

        private void SetConfidenceThreshold()
        {
            confidenceBar.Value = settings.ConfidenceThreshold;
            UpdateConfidenceLabel();
        }

        private void SettingsWindow_Load(Object sender, EventArgs e)
        {
            SetConfidenceThreshold();
        }

        private void confidenceBar_ValueChanged(Object sender, EventArgs e)
        {
            settings.ConfidenceThreshold = confidenceBar.Value;
            UpdateConfidenceLabel();
        }

        private void SettingsWindow_FormClosing(Object sender, FormClosingEventArgs e)
        {
            settings.SaveDb();
        }
    }
}
