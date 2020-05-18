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

        private void UpdateFaceConfidenceLabel()
        {
            faceConfidenceLabel.Text = settings.FaceConfidenceThreshold.ToString();
        }

        private void SetConfidenceThreshold()
        {
            confidenceBar.Value = settings.ConfidenceThreshold;
            UpdateConfidenceLabel();
        }

        private void SetFaceConfidenceThreshold()
        {
            faceConfidenceBar.Value = settings.FaceConfidenceThreshold;
            UpdateFaceConfidenceLabel();
        }

        private void SetDraws()
        {
            personCheck.Checked = settings.DrawPerson;
            backgroundCheck.Checked = settings.DrawBackground;
            aeroplaneCheck.Checked = settings.DrawAeroplane;
            bicycleCheck.Checked = settings.DrawBicycle;
            birdCheck.Checked = settings.DrawBird;
            boatCheck.Checked = settings.DrawBoat;
            bottleCheck.Checked = settings.DrawBottle;
            busCheck.Checked = settings.DrawBus;
            carCheck.Checked = settings.DrawCar;
            catCheck.Checked = settings.DrawCat;
            chairCheck.Checked = settings.DrawChair;
            cowCheck.Checked = settings.DrawCow;
            diningtableCheck.Checked = settings.DrawDiningtable;
            dogCheck.Checked = settings.DrawDog;
            horseCheck.Checked = settings.DrawHorse;
            motorbikeCheck.Checked = settings.DrawMotorbike;
            pottedplantCheck.Checked = settings.DrawPottedplant;
            sheepCheck.Checked = settings.DrawSheep;
            sofaCheck.Checked = settings.DrawSofa;
            trainCheck.Checked = settings.DrawTrain;
            tvCheck.Checked = settings.DrawTv;
        }

        private void SettingsWindow_Load(Object sender, EventArgs e)
        {
            SetConfidenceThreshold();
            SetFaceConfidenceThreshold();
            SetDraws();
        }

        private void confidenceBar_ValueChanged(Object sender, EventArgs e)
        {
            settings.ConfidenceThreshold = confidenceBar.Value;
            UpdateConfidenceLabel();
        }

        private void faceConfidenceBar_ValueChanged(Object sender, EventArgs e)
        {
            settings.FaceConfidenceThreshold = faceConfidenceBar.Value;
            UpdateFaceConfidenceLabel();
        }

        private void SettingsWindow_FormClosing(Object sender, FormClosingEventArgs e)
        {
            settings.SaveDb();
        }

        private void personCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawPerson = personCheck.Checked;
        }

        private void aeroplaneCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawAeroplane = aeroplaneCheck.Checked;
        }

        private void bicycleCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBicycle = bicycleCheck.Checked;
        }

        private void birdCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBird = birdCheck.Checked;
        }

        private void boatCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBoat = boatCheck.Checked;
        }

        private void bottleCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBottle = bottleCheck.Checked;
        }

        private void busCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBus = busCheck.Checked;
        }

        private void carCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawCar = carCheck.Checked;
        }

        private void catCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawCat = catCheck.Checked;
        }

        private void chairCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawChair = chairCheck.Checked;
        }

        private void cowCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawCow = cowCheck.Checked;
        }

        private void diningtableCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawDiningtable = diningtableCheck.Checked;
        }

        private void dogCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawDog = dogCheck.Checked;
        }

        private void horseCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawHorse = horseCheck.Checked;
        }

        private void motorbikeCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawMotorbike = motorbikeCheck.Checked;
        }

        private void pottedplantCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawPottedplant = pottedplantCheck.Checked;
        }

        private void sheepCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawSheep = sheepCheck.Checked;
        }

        private void sofaCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawSofa = sofaCheck.Checked;
        }

        private void trainCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawTrain = trainCheck.Checked;
        }

        private void tvCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawTv = tvCheck.Checked;
        }

        private void backgroundCheck_CheckedChanged(Object sender, EventArgs e)
        {
            settings.DrawBackground = backgroundCheck.Checked;
        }
    }
}
