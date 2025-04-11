namespace ZooBaazar
{
    partial class LocationForm
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
            btnAddLocationForm = new Button();
            btnBackLocationForm = new Button();
            statusStrip1 = new StatusStrip();
            tsslblLocationForm = new ToolStripStatusLabel();
            panel1 = new Panel();
            cbSpecies_LocationForm = new ComboBox();
            clbListOfAnimals = new CheckedListBox();
            pictureBox3 = new PictureBox();
            tbSearch = new TextBox();
            lblSearch = new Label();
            lblListOfAnimals = new Label();
            lblLocation = new Label();
            cbDangerLevel = new ComboBox();
            nudCapacity = new NumericUpDown();
            tbDescriptionLocationForm = new TextBox();
            tbNameLocationForm = new TextBox();
            lblDangerLevel = new Label();
            lblSpeciesLocationForm = new Label();
            lblDescriptionLocationForm = new Label();
            lblCapacity = new Label();
            lblNameLocationForm = new Label();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCapacity).BeginInit();
            SuspendLayout();
            // 
            // btnAddLocationForm
            // 
            btnAddLocationForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAddLocationForm.Location = new Point(1141, 305);
            btnAddLocationForm.Name = "btnAddLocationForm";
            btnAddLocationForm.Size = new Size(107, 47);
            btnAddLocationForm.TabIndex = 12;
            btnAddLocationForm.Text = "Add";
            btnAddLocationForm.UseVisualStyleBackColor = true;
            btnAddLocationForm.Click += BtnAddLocation_Click;
            // 
            // btnBackLocationForm
            // 
            btnBackLocationForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBackLocationForm.Location = new Point(1141, 365);
            btnBackLocationForm.Name = "btnBackLocationForm";
            btnBackLocationForm.Size = new Size(107, 47);
            btnBackLocationForm.TabIndex = 16;
            btnBackLocationForm.Text = "Back";
            btnBackLocationForm.UseVisualStyleBackColor = true;
            btnBackLocationForm.Click += BtnBack_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.DarkSeaGreen;
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslblLocationForm });
            statusStrip1.Location = new Point(0, 646);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 11, 0);
            statusStrip1.Size = new Size(1259, 22);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "ssLocationForm";
            // 
            // tsslblLocationForm
            // 
            tsslblLocationForm.ForeColor = Color.Red;
            tsslblLocationForm.Name = "tsslblLocationForm";
            tsslblLocationForm.Size = new Size(0, 16);
            // 
            // panel1
            // 
            panel1.BackColor = Color.AntiqueWhite;
            panel1.Controls.Add(cbSpecies_LocationForm);
            panel1.Controls.Add(clbListOfAnimals);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(tbSearch);
            panel1.Controls.Add(lblSearch);
            panel1.Controls.Add(lblListOfAnimals);
            panel1.Controls.Add(lblLocation);
            panel1.Controls.Add(cbDangerLevel);
            panel1.Controls.Add(nudCapacity);
            panel1.Controls.Add(tbDescriptionLocationForm);
            panel1.Controls.Add(tbNameLocationForm);
            panel1.Controls.Add(lblDangerLevel);
            panel1.Controls.Add(lblSpeciesLocationForm);
            panel1.Controls.Add(lblDescriptionLocationForm);
            panel1.Controls.Add(lblCapacity);
            panel1.Controls.Add(lblNameLocationForm);
            panel1.Location = new Point(12, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(1111, 615);
            panel1.TabIndex = 18;
            // 
            // cbSpecies_LocationForm
            // 
            cbSpecies_LocationForm.FormattingEnabled = true;
            cbSpecies_LocationForm.Location = new Point(33, 287);
            cbSpecies_LocationForm.Margin = new Padding(2);
            cbSpecies_LocationForm.Name = "cbSpecies_LocationForm";
            cbSpecies_LocationForm.Size = new Size(267, 28);
            cbSpecies_LocationForm.TabIndex = 171;
            // 
            // clbListOfAnimals
            // 
            clbListOfAnimals.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbListOfAnimals.FormattingEnabled = true;
            clbListOfAnimals.Location = new Point(793, 144);
            clbListOfAnimals.Margin = new Padding(3, 4, 3, 4);
            clbListOfAnimals.Name = "clbListOfAnimals";
            clbListOfAnimals.Size = new Size(285, 422);
            clbListOfAnimals.TabIndex = 170;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.location;
            pictureBox3.Location = new Point(459, 113);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(208, 175);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 50;
            pictureBox3.TabStop = false;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(834, 52);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(244, 27);
            tbSearch.TabIndex = 49;
            tbSearch.TextChanged += tbSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(736, 52);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(92, 28);
            lblSearch.TabIndex = 48;
            lblSearch.Text = "Search : ";
            // 
            // lblListOfAnimals
            // 
            lblListOfAnimals.AutoSize = true;
            lblListOfAnimals.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblListOfAnimals.Location = new Point(793, 113);
            lblListOfAnimals.Name = "lblListOfAnimals";
            lblListOfAnimals.Size = new Size(173, 28);
            lblListOfAnimals.TabIndex = 40;
            lblListOfAnimals.Text = "List Of Animals : ";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLocation.Location = new Point(512, 11);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(157, 46);
            lblLocation.TabIndex = 23;
            lblLocation.Text = "Location";
            // 
            // cbDangerLevel
            // 
            cbDangerLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDangerLevel.FormattingEnabled = true;
            cbDangerLevel.Location = new Point(35, 397);
            cbDangerLevel.Name = "cbDangerLevel";
            cbDangerLevel.Size = new Size(265, 28);
            cbDangerLevel.TabIndex = 21;
            // 
            // nudCapacity
            // 
            nudCapacity.Location = new Point(33, 188);
            nudCapacity.Name = "nudCapacity";
            nudCapacity.Size = new Size(267, 27);
            nudCapacity.TabIndex = 20;
            // 
            // tbDescriptionLocationForm
            // 
            tbDescriptionLocationForm.Location = new Point(344, 385);
            tbDescriptionLocationForm.Multiline = true;
            tbDescriptionLocationForm.Name = "tbDescriptionLocationForm";
            tbDescriptionLocationForm.Size = new Size(425, 203);
            tbDescriptionLocationForm.TabIndex = 19;
            // 
            // tbNameLocationForm
            // 
            tbNameLocationForm.Location = new Point(33, 97);
            tbNameLocationForm.Name = "tbNameLocationForm";
            tbNameLocationForm.Size = new Size(267, 27);
            tbNameLocationForm.TabIndex = 18;
            // 
            // lblDangerLevel
            // 
            lblDangerLevel.AutoSize = true;
            lblDangerLevel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDangerLevel.Location = new Point(33, 344);
            lblDangerLevel.Name = "lblDangerLevel";
            lblDangerLevel.Size = new Size(147, 28);
            lblDangerLevel.TabIndex = 17;
            lblDangerLevel.Text = "Danger Level :";
            // 
            // lblSpeciesLocationForm
            // 
            lblSpeciesLocationForm.AutoSize = true;
            lblSpeciesLocationForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSpeciesLocationForm.Location = new Point(33, 247);
            lblSpeciesLocationForm.Name = "lblSpeciesLocationForm";
            lblSpeciesLocationForm.Size = new Size(93, 28);
            lblSpeciesLocationForm.TabIndex = 16;
            lblSpeciesLocationForm.Text = "Species :";
            // 
            // lblDescriptionLocationForm
            // 
            lblDescriptionLocationForm.AutoSize = true;
            lblDescriptionLocationForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescriptionLocationForm.Location = new Point(344, 344);
            lblDescriptionLocationForm.Name = "lblDescriptionLocationForm";
            lblDescriptionLocationForm.Size = new Size(132, 28);
            lblDescriptionLocationForm.TabIndex = 15;
            lblDescriptionLocationForm.Text = "Description :";
            // 
            // lblCapacity
            // 
            lblCapacity.AutoSize = true;
            lblCapacity.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCapacity.Location = new Point(33, 147);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new Size(104, 28);
            lblCapacity.TabIndex = 14;
            lblCapacity.Text = "Capacity :";
            // 
            // lblNameLocationForm
            // 
            lblNameLocationForm.AutoSize = true;
            lblNameLocationForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNameLocationForm.Location = new Point(33, 69);
            lblNameLocationForm.Name = "lblNameLocationForm";
            lblNameLocationForm.Size = new Size(79, 28);
            lblNameLocationForm.TabIndex = 13;
            lblNameLocationForm.Text = "Name :";
            // 
            // LocationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1259, 668);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(btnBackLocationForm);
            Controls.Add(btnAddLocationForm);
            MaximumSize = new Size(1277, 715);
            MinimumSize = new Size(1277, 715);
            Name = "LocationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Location";
            FormClosed += LocationForm_FormClosed;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCapacity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddLocationForm;
        private Button btnBackLocationForm;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslblLocationForm;
        private Panel panel1;
        private Label lblLocation;
        private ComboBox cbDangerLevel;
        private NumericUpDown nudCapacity;
        private TextBox tbDescriptionLocationForm;
        private TextBox tbNameLocationForm;
        private Label lblDangerLevel;
        private Label lblSpeciesLocationForm;
        private Label lblDescriptionLocationForm;
        private Label lblCapacity;
        private Label lblNameLocationForm;
        private Label lblListOfAnimals;
        private TextBox tbSearch;
        private Label lblSearch;
        private PictureBox pictureBox3;
        private CheckedListBox clbListOfAnimals;
        private ComboBox cbSpecies_LocationForm;
    }
}