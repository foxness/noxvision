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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.confidenceTextLabel = new System.Windows.Forms.Label();
            this.confidenceBar = new System.Windows.Forms.TrackBar();
            this.confidenceLabel = new System.Windows.Forms.Label();
            this.faceConfidenceTextLabel = new System.Windows.Forms.Label();
            this.faceConfidenceBar = new System.Windows.Forms.TrackBar();
            this.faceConfidenceLabel = new System.Windows.Forms.Label();
            this.personCheck = new System.Windows.Forms.CheckBox();
            this.drawLabel = new System.Windows.Forms.Label();
            this.aeroplaneCheck = new System.Windows.Forms.CheckBox();
            this.bicycleCheck = new System.Windows.Forms.CheckBox();
            this.birdCheck = new System.Windows.Forms.CheckBox();
            this.boatCheck = new System.Windows.Forms.CheckBox();
            this.bottleCheck = new System.Windows.Forms.CheckBox();
            this.busCheck = new System.Windows.Forms.CheckBox();
            this.carCheck = new System.Windows.Forms.CheckBox();
            this.catCheck = new System.Windows.Forms.CheckBox();
            this.chairCheck = new System.Windows.Forms.CheckBox();
            this.cowCheck = new System.Windows.Forms.CheckBox();
            this.diningtableCheck = new System.Windows.Forms.CheckBox();
            this.dogCheck = new System.Windows.Forms.CheckBox();
            this.horseCheck = new System.Windows.Forms.CheckBox();
            this.motorbikeCheck = new System.Windows.Forms.CheckBox();
            this.pottedplantCheck = new System.Windows.Forms.CheckBox();
            this.sheepCheck = new System.Windows.Forms.CheckBox();
            this.sofaCheck = new System.Windows.Forms.CheckBox();
            this.trainCheck = new System.Windows.Forms.CheckBox();
            this.tvCheck = new System.Windows.Forms.CheckBox();
            this.backgroundCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.confidenceBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceConfidenceBar)).BeginInit();
            this.SuspendLayout();
            // 
            // confidenceTextLabel
            // 
            this.confidenceTextLabel.AutoSize = true;
            this.confidenceTextLabel.Location = new System.Drawing.Point(12, 22);
            this.confidenceTextLabel.Name = "confidenceTextLabel";
            this.confidenceTextLabel.Size = new System.Drawing.Size(168, 13);
            this.confidenceTextLabel.TabIndex = 0;
            this.confidenceTextLabel.Text = "Порог достоверности объектов";
            // 
            // confidenceBar
            // 
            this.confidenceBar.Location = new System.Drawing.Point(183, 16);
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
            this.confidenceLabel.Location = new System.Drawing.Point(375, 15);
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
            this.faceConfidenceTextLabel.Size = new System.Drawing.Size(144, 13);
            this.faceConfidenceTextLabel.TabIndex = 3;
            this.faceConfidenceTextLabel.Text = "Порог достоверности лица";
            // 
            // faceConfidenceBar
            // 
            this.faceConfidenceBar.Location = new System.Drawing.Point(183, 67);
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
            this.faceConfidenceLabel.Location = new System.Drawing.Point(375, 67);
            this.faceConfidenceLabel.Name = "faceConfidenceLabel";
            this.faceConfidenceLabel.Size = new System.Drawing.Size(40, 23);
            this.faceConfidenceLabel.TabIndex = 5;
            this.faceConfidenceLabel.Text = "50";
            this.faceConfidenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // personCheck
            // 
            this.personCheck.AutoSize = true;
            this.personCheck.Location = new System.Drawing.Point(15, 138);
            this.personCheck.Name = "personCheck";
            this.personCheck.Size = new System.Drawing.Size(70, 17);
            this.personCheck.TabIndex = 7;
            this.personCheck.Text = "Человек";
            this.personCheck.UseVisualStyleBackColor = true;
            this.personCheck.CheckedChanged += new System.EventHandler(this.personCheck_CheckedChanged);
            // 
            // drawLabel
            // 
            this.drawLabel.AutoSize = true;
            this.drawLabel.Location = new System.Drawing.Point(12, 112);
            this.drawLabel.Name = "drawLabel";
            this.drawLabel.Size = new System.Drawing.Size(156, 13);
            this.drawLabel.TabIndex = 8;
            this.drawLabel.Text = "Показывать рамки объектов";
            // 
            // aeroplaneCheck
            // 
            this.aeroplaneCheck.AutoSize = true;
            this.aeroplaneCheck.Location = new System.Drawing.Point(15, 162);
            this.aeroplaneCheck.Name = "aeroplaneCheck";
            this.aeroplaneCheck.Size = new System.Drawing.Size(70, 17);
            this.aeroplaneCheck.TabIndex = 9;
            this.aeroplaneCheck.Text = "Самолет";
            this.aeroplaneCheck.UseVisualStyleBackColor = true;
            this.aeroplaneCheck.CheckedChanged += new System.EventHandler(this.aeroplaneCheck_CheckedChanged);
            // 
            // bicycleCheck
            // 
            this.bicycleCheck.AutoSize = true;
            this.bicycleCheck.Location = new System.Drawing.Point(15, 186);
            this.bicycleCheck.Name = "bicycleCheck";
            this.bicycleCheck.Size = new System.Drawing.Size(81, 17);
            this.bicycleCheck.TabIndex = 10;
            this.bicycleCheck.Text = "Велосипед";
            this.bicycleCheck.UseVisualStyleBackColor = true;
            this.bicycleCheck.CheckedChanged += new System.EventHandler(this.bicycleCheck_CheckedChanged);
            // 
            // birdCheck
            // 
            this.birdCheck.AutoSize = true;
            this.birdCheck.Location = new System.Drawing.Point(15, 210);
            this.birdCheck.Name = "birdCheck";
            this.birdCheck.Size = new System.Drawing.Size(57, 17);
            this.birdCheck.TabIndex = 11;
            this.birdCheck.Text = "Птица";
            this.birdCheck.UseVisualStyleBackColor = true;
            this.birdCheck.CheckedChanged += new System.EventHandler(this.birdCheck_CheckedChanged);
            // 
            // boatCheck
            // 
            this.boatCheck.AutoSize = true;
            this.boatCheck.Location = new System.Drawing.Point(15, 234);
            this.boatCheck.Name = "boatCheck";
            this.boatCheck.Size = new System.Drawing.Size(58, 17);
            this.boatCheck.TabIndex = 12;
            this.boatCheck.Text = "Лодка";
            this.boatCheck.UseVisualStyleBackColor = true;
            this.boatCheck.CheckedChanged += new System.EventHandler(this.boatCheck_CheckedChanged);
            // 
            // bottleCheck
            // 
            this.bottleCheck.AutoSize = true;
            this.bottleCheck.Location = new System.Drawing.Point(15, 258);
            this.bottleCheck.Name = "bottleCheck";
            this.bottleCheck.Size = new System.Drawing.Size(69, 17);
            this.bottleCheck.TabIndex = 13;
            this.bottleCheck.Text = "Бутылка";
            this.bottleCheck.UseVisualStyleBackColor = true;
            this.bottleCheck.CheckedChanged += new System.EventHandler(this.bottleCheck_CheckedChanged);
            // 
            // busCheck
            // 
            this.busCheck.AutoSize = true;
            this.busCheck.Location = new System.Drawing.Point(15, 282);
            this.busCheck.Name = "busCheck";
            this.busCheck.Size = new System.Drawing.Size(67, 17);
            this.busCheck.TabIndex = 14;
            this.busCheck.Text = "Автобус";
            this.busCheck.UseVisualStyleBackColor = true;
            this.busCheck.CheckedChanged += new System.EventHandler(this.busCheck_CheckedChanged);
            // 
            // carCheck
            // 
            this.carCheck.AutoSize = true;
            this.carCheck.Location = new System.Drawing.Point(160, 138);
            this.carCheck.Name = "carCheck";
            this.carCheck.Size = new System.Drawing.Size(67, 17);
            this.carCheck.TabIndex = 15;
            this.carCheck.Text = "Машина";
            this.carCheck.UseVisualStyleBackColor = true;
            this.carCheck.CheckedChanged += new System.EventHandler(this.carCheck_CheckedChanged);
            // 
            // catCheck
            // 
            this.catCheck.AutoSize = true;
            this.catCheck.Location = new System.Drawing.Point(160, 162);
            this.catCheck.Name = "catCheck";
            this.catCheck.Size = new System.Drawing.Size(59, 17);
            this.catCheck.TabIndex = 16;
            this.catCheck.Text = "Кошка";
            this.catCheck.UseVisualStyleBackColor = true;
            this.catCheck.CheckedChanged += new System.EventHandler(this.catCheck_CheckedChanged);
            // 
            // chairCheck
            // 
            this.chairCheck.AutoSize = true;
            this.chairCheck.Location = new System.Drawing.Point(160, 186);
            this.chairCheck.Name = "chairCheck";
            this.chairCheck.Size = new System.Drawing.Size(49, 17);
            this.chairCheck.TabIndex = 17;
            this.chairCheck.Text = "Стул";
            this.chairCheck.UseVisualStyleBackColor = true;
            this.chairCheck.CheckedChanged += new System.EventHandler(this.chairCheck_CheckedChanged);
            // 
            // cowCheck
            // 
            this.cowCheck.AutoSize = true;
            this.cowCheck.Location = new System.Drawing.Point(160, 210);
            this.cowCheck.Name = "cowCheck";
            this.cowCheck.Size = new System.Drawing.Size(63, 17);
            this.cowCheck.TabIndex = 18;
            this.cowCheck.Text = "Корова";
            this.cowCheck.UseVisualStyleBackColor = true;
            this.cowCheck.CheckedChanged += new System.EventHandler(this.cowCheck_CheckedChanged);
            // 
            // diningtableCheck
            // 
            this.diningtableCheck.AutoSize = true;
            this.diningtableCheck.Location = new System.Drawing.Point(160, 234);
            this.diningtableCheck.Name = "diningtableCheck";
            this.diningtableCheck.Size = new System.Drawing.Size(50, 17);
            this.diningtableCheck.TabIndex = 19;
            this.diningtableCheck.Text = "Стол";
            this.diningtableCheck.UseVisualStyleBackColor = true;
            this.diningtableCheck.CheckedChanged += new System.EventHandler(this.diningtableCheck_CheckedChanged);
            // 
            // dogCheck
            // 
            this.dogCheck.AutoSize = true;
            this.dogCheck.Location = new System.Drawing.Point(160, 258);
            this.dogCheck.Name = "dogCheck";
            this.dogCheck.Size = new System.Drawing.Size(63, 17);
            this.dogCheck.TabIndex = 20;
            this.dogCheck.Text = "Собака";
            this.dogCheck.UseVisualStyleBackColor = true;
            this.dogCheck.CheckedChanged += new System.EventHandler(this.dogCheck_CheckedChanged);
            // 
            // horseCheck
            // 
            this.horseCheck.AutoSize = true;
            this.horseCheck.Location = new System.Drawing.Point(160, 282);
            this.horseCheck.Name = "horseCheck";
            this.horseCheck.Size = new System.Drawing.Size(66, 17);
            this.horseCheck.TabIndex = 21;
            this.horseCheck.Text = "Лошадь";
            this.horseCheck.UseVisualStyleBackColor = true;
            this.horseCheck.CheckedChanged += new System.EventHandler(this.horseCheck_CheckedChanged);
            // 
            // motorbikeCheck
            // 
            this.motorbikeCheck.AutoSize = true;
            this.motorbikeCheck.Location = new System.Drawing.Point(287, 138);
            this.motorbikeCheck.Name = "motorbikeCheck";
            this.motorbikeCheck.Size = new System.Drawing.Size(76, 17);
            this.motorbikeCheck.TabIndex = 22;
            this.motorbikeCheck.Text = "Мотоцикл";
            this.motorbikeCheck.UseVisualStyleBackColor = true;
            this.motorbikeCheck.CheckedChanged += new System.EventHandler(this.motorbikeCheck_CheckedChanged);
            // 
            // pottedplantCheck
            // 
            this.pottedplantCheck.AutoSize = true;
            this.pottedplantCheck.Location = new System.Drawing.Point(287, 162);
            this.pottedplantCheck.Name = "pottedplantCheck";
            this.pottedplantCheck.Size = new System.Drawing.Size(123, 17);
            this.pottedplantCheck.TabIndex = 23;
            this.pottedplantCheck.Text = "Растение в горшке";
            this.pottedplantCheck.UseVisualStyleBackColor = true;
            this.pottedplantCheck.CheckedChanged += new System.EventHandler(this.pottedplantCheck_CheckedChanged);
            // 
            // sheepCheck
            // 
            this.sheepCheck.AutoSize = true;
            this.sheepCheck.Location = new System.Drawing.Point(287, 186);
            this.sheepCheck.Name = "sheepCheck";
            this.sheepCheck.Size = new System.Drawing.Size(52, 17);
            this.sheepCheck.TabIndex = 24;
            this.sheepCheck.Text = "Овца";
            this.sheepCheck.UseVisualStyleBackColor = true;
            this.sheepCheck.CheckedChanged += new System.EventHandler(this.sheepCheck_CheckedChanged);
            // 
            // sofaCheck
            // 
            this.sofaCheck.AutoSize = true;
            this.sofaCheck.Location = new System.Drawing.Point(287, 210);
            this.sofaCheck.Name = "sofaCheck";
            this.sofaCheck.Size = new System.Drawing.Size(59, 17);
            this.sofaCheck.TabIndex = 25;
            this.sofaCheck.Text = "Диван";
            this.sofaCheck.UseVisualStyleBackColor = true;
            this.sofaCheck.CheckedChanged += new System.EventHandler(this.sofaCheck_CheckedChanged);
            // 
            // trainCheck
            // 
            this.trainCheck.AutoSize = true;
            this.trainCheck.Location = new System.Drawing.Point(287, 234);
            this.trainCheck.Name = "trainCheck";
            this.trainCheck.Size = new System.Drawing.Size(58, 17);
            this.trainCheck.TabIndex = 26;
            this.trainCheck.Text = "Поезд";
            this.trainCheck.UseVisualStyleBackColor = true;
            this.trainCheck.CheckedChanged += new System.EventHandler(this.trainCheck_CheckedChanged);
            // 
            // tvCheck
            // 
            this.tvCheck.AutoSize = true;
            this.tvCheck.Location = new System.Drawing.Point(287, 258);
            this.tvCheck.Name = "tvCheck";
            this.tvCheck.Size = new System.Drawing.Size(81, 17);
            this.tvCheck.TabIndex = 27;
            this.tvCheck.Text = "Телевизор";
            this.tvCheck.UseVisualStyleBackColor = true;
            this.tvCheck.CheckedChanged += new System.EventHandler(this.tvCheck_CheckedChanged);
            // 
            // backgroundCheck
            // 
            this.backgroundCheck.AutoSize = true;
            this.backgroundCheck.Location = new System.Drawing.Point(287, 282);
            this.backgroundCheck.Name = "backgroundCheck";
            this.backgroundCheck.Size = new System.Drawing.Size(49, 17);
            this.backgroundCheck.TabIndex = 28;
            this.backgroundCheck.Text = "Фон";
            this.backgroundCheck.UseVisualStyleBackColor = true;
            this.backgroundCheck.CheckedChanged += new System.EventHandler(this.backgroundCheck_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 321);
            this.Controls.Add(this.backgroundCheck);
            this.Controls.Add(this.tvCheck);
            this.Controls.Add(this.trainCheck);
            this.Controls.Add(this.sofaCheck);
            this.Controls.Add(this.sheepCheck);
            this.Controls.Add(this.pottedplantCheck);
            this.Controls.Add(this.motorbikeCheck);
            this.Controls.Add(this.horseCheck);
            this.Controls.Add(this.dogCheck);
            this.Controls.Add(this.diningtableCheck);
            this.Controls.Add(this.cowCheck);
            this.Controls.Add(this.chairCheck);
            this.Controls.Add(this.catCheck);
            this.Controls.Add(this.carCheck);
            this.Controls.Add(this.busCheck);
            this.Controls.Add(this.bottleCheck);
            this.Controls.Add(this.boatCheck);
            this.Controls.Add(this.birdCheck);
            this.Controls.Add(this.bicycleCheck);
            this.Controls.Add(this.aeroplaneCheck);
            this.Controls.Add(this.drawLabel);
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
            this.Text = "Настройки";
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
        private System.Windows.Forms.Label drawLabel;
        private System.Windows.Forms.CheckBox aeroplaneCheck;
        private System.Windows.Forms.CheckBox bicycleCheck;
        private System.Windows.Forms.CheckBox birdCheck;
        private System.Windows.Forms.CheckBox boatCheck;
        private System.Windows.Forms.CheckBox bottleCheck;
        private System.Windows.Forms.CheckBox busCheck;
        private System.Windows.Forms.CheckBox carCheck;
        private System.Windows.Forms.CheckBox catCheck;
        private System.Windows.Forms.CheckBox chairCheck;
        private System.Windows.Forms.CheckBox cowCheck;
        private System.Windows.Forms.CheckBox diningtableCheck;
        private System.Windows.Forms.CheckBox dogCheck;
        private System.Windows.Forms.CheckBox horseCheck;
        private System.Windows.Forms.CheckBox motorbikeCheck;
        private System.Windows.Forms.CheckBox pottedplantCheck;
        private System.Windows.Forms.CheckBox sheepCheck;
        private System.Windows.Forms.CheckBox sofaCheck;
        private System.Windows.Forms.CheckBox trainCheck;
        private System.Windows.Forms.CheckBox tvCheck;
        private System.Windows.Forms.CheckBox backgroundCheck;
    }
}