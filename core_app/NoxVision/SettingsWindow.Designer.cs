﻿namespace NoxVision
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.confidenceTextLabel = new System.Windows.Forms.Label();
            this.confidenceBar = new System.Windows.Forms.TrackBar();
            this.confidenceLabel = new System.Windows.Forms.Label();
            this.faceConfidenceTextLabel = new System.Windows.Forms.Label();
            this.faceConfidenceBar = new System.Windows.Forms.TrackBar();
            this.faceConfidenceLabel = new System.Windows.Forms.Label();
            this.personCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.confidenceBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceConfidenceBar)).BeginInit();
            this.SuspendLayout();
            // 
            // confidenceTextLabel
            // 
            this.confidenceTextLabel.AutoSize = true;
            this.confidenceTextLabel.Location = new System.Drawing.Point(12, 22);
            this.confidenceTextLabel.Name = "confidenceTextLabel";
            this.confidenceTextLabel.Size = new System.Drawing.Size(140, 13);
            this.confidenceTextLabel.TabIndex = 0;
            this.confidenceTextLabel.Text = "Object confidence threshold";
            // 
            // confidenceBar
            // 
            this.confidenceBar.Location = new System.Drawing.Point(160, 16);
            this.confidenceBar.Maximum = 100;
            this.confidenceBar.Name = "confidenceBar";
            this.confidenceBar.Size = new System.Drawing.Size(188, 45);
            this.confidenceBar.TabIndex = 1;
            this.confidenceBar.Value = 50;
            this.confidenceBar.ValueChanged += new System.EventHandler(this.confidenceBar_ValueChanged);
            // 
            // confidenceLabel
            // 
            this.confidenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confidenceLabel.Location = new System.Drawing.Point(352, 15);
            this.confidenceLabel.Name = "confidenceLabel";
            this.confidenceLabel.Size = new System.Drawing.Size(40, 23);
            this.confidenceLabel.TabIndex = 2;
            this.confidenceLabel.Text = "50";
            this.confidenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // faceConfidenceTextLabel
            // 
            this.faceConfidenceTextLabel.AutoSize = true;
            this.faceConfidenceTextLabel.Location = new System.Drawing.Point(12, 73);
            this.faceConfidenceTextLabel.Name = "faceConfidenceTextLabel";
            this.faceConfidenceTextLabel.Size = new System.Drawing.Size(133, 13);
            this.faceConfidenceTextLabel.TabIndex = 3;
            this.faceConfidenceTextLabel.Text = "Face confidence threshold";
            // 
            // faceConfidenceBar
            // 
            this.faceConfidenceBar.Location = new System.Drawing.Point(160, 67);
            this.faceConfidenceBar.Maximum = 100;
            this.faceConfidenceBar.Name = "faceConfidenceBar";
            this.faceConfidenceBar.Size = new System.Drawing.Size(188, 45);
            this.faceConfidenceBar.TabIndex = 4;
            this.faceConfidenceBar.Value = 50;
            this.faceConfidenceBar.ValueChanged += new System.EventHandler(this.faceConfidenceBar_ValueChanged);
            // 
            // faceConfidenceLabel
            // 
            this.faceConfidenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faceConfidenceLabel.Location = new System.Drawing.Point(352, 67);
            this.faceConfidenceLabel.Name = "faceConfidenceLabel";
            this.faceConfidenceLabel.Size = new System.Drawing.Size(40, 23);
            this.faceConfidenceLabel.TabIndex = 5;
            this.faceConfidenceLabel.Text = "50";
            this.faceConfidenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // personCheck
            // 
            this.personCheck.AutoSize = true;
            this.personCheck.Location = new System.Drawing.Point(15, 132);
            this.personCheck.Name = "personCheck";
            this.personCheck.Size = new System.Drawing.Size(59, 17);
            this.personCheck.TabIndex = 7;
            this.personCheck.Text = "Person";
            this.personCheck.UseVisualStyleBackColor = true;
            this.personCheck.CheckedChanged += new System.EventHandler(this.personCheck_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 321);
            this.Controls.Add(this.personCheck);
            this.Controls.Add(this.faceConfidenceLabel);
            this.Controls.Add(this.faceConfidenceBar);
            this.Controls.Add(this.faceConfidenceTextLabel);
            this.Controls.Add(this.confidenceLabel);
            this.Controls.Add(this.confidenceBar);
            this.Controls.Add(this.confidenceTextLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.confidenceBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceConfidenceBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label confidenceTextLabel;
        private System.Windows.Forms.TrackBar confidenceBar;
        private System.Windows.Forms.Label confidenceLabel;
        private System.Windows.Forms.Label faceConfidenceTextLabel;
        private System.Windows.Forms.TrackBar faceConfidenceBar;
        private System.Windows.Forms.Label faceConfidenceLabel;
        private System.Windows.Forms.CheckBox personCheck;
    }
}