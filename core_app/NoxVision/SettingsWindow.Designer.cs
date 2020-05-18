namespace NoxVision
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
            this.confidenceTextLabel = new System.Windows.Forms.Label();
            this.confidenceBar = new System.Windows.Forms.TrackBar();
            this.confidenceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.confidenceBar)).BeginInit();
            this.SuspendLayout();
            // 
            // confidenceTextLabel
            // 
            this.confidenceTextLabel.AutoSize = true;
            this.confidenceTextLabel.Location = new System.Drawing.Point(12, 22);
            this.confidenceTextLabel.Name = "confidenceTextLabel";
            this.confidenceTextLabel.Size = new System.Drawing.Size(141, 13);
            this.confidenceTextLabel.TabIndex = 0;
            this.confidenceTextLabel.Text = "Object detection confidence";
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
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 321);
            this.Controls.Add(this.confidenceLabel);
            this.Controls.Add(this.confidenceBar);
            this.Controls.Add(this.confidenceTextLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.confidenceBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label confidenceTextLabel;
        private System.Windows.Forms.TrackBar confidenceBar;
        private System.Windows.Forms.Label confidenceLabel;
    }
}