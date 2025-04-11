using Microsoft.Data.SqlClient;

namespace ZooBaazar
{
    partial class AnimalForm
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
            panel1 = new Panel();
            dtpBirthday = new DateTimePicker();
            cbSpecies = new ComboBox();
            label1 = new Label();
            btnNewLocation = new Button();
            pictureBox2 = new PictureBox();
            plRelationship = new Panel();
            dgvAnimalRelations = new DataGridView();
            btnDeleteRelation = new Button();
            clbAnimals = new CheckedListBox();
            lblSearchRelations = new Label();
            tbSearchRelations = new TextBox();
            btnAddRelation = new Button();
            lblRelationShips = new Label();
            lblSearch = new Label();
            lblRelations = new Label();
            tbRelations = new TextBox();
            tbSearch = new TextBox();
            plMedicalRecords = new Panel();
            lblMedicalRecords = new Label();
            lblMedication = new Label();
            lblNotesMedical = new Label();
            lblOperations = new Label();
            lblInjuries = new Label();
            lblDiseases = new Label();
            cbDangerlvl = new ComboBox();
            lblDangerlvl = new Label();
            lblAllergies = new Label();
            tbDiseases = new TextBox();
            tbInjuries = new TextBox();
            tbOperations = new TextBox();
            tbNotesMedical = new TextBox();
            tbMedication = new TextBox();
            tbMedicalAllergies = new TextBox();
            plFeedingPlan = new Panel();
            clbFoodAllergies = new CheckedListBox();
            lblIdealWeight = new Label();
            lblWeight = new Label();
            lblCaloriesIntake = new Label();
            lblNotes = new Label();
            lblAllergiesFood = new Label();
            lblDislikedFood = new Label();
            lblFavoriteFood = new Label();
            lblDiet = new Label();
            cbDiet = new ComboBox();
            lblFeedingPlanFeedingPlanForm = new Label();
            tbNotesFeeding = new TextBox();
            tbFavoriteFood = new TextBox();
            tbDislikedFood = new TextBox();
            tbCaloriesIntake = new TextBox();
            tbWeight = new TextBox();
            tbIdealWeight = new TextBox();
            dtpExitDate = new DateTimePicker();
            dtpEntryDate = new DateTimePicker();
            cbBloodType = new ComboBox();
            lblBloodType = new Label();
            lblLocation = new Label();
            lblOrigin = new Label();
            lblSpecies = new Label();
            lblName = new Label();
            cbLocationAnimalForm = new ComboBox();
            lblExitAnimalForm = new Label();
            lblEntryAnimalForm = new Label();
            tbOrigin = new TextBox();
            tbNameAnimalForm = new TextBox();
            lblAnimalManager = new Label();
            btnSaveAnimalForm = new Button();
            btnBackAnimalForm = new Button();
            ssAnimalForm = new StatusStrip();
            tsslblAnimalForm = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            plRelationship.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimalRelations).BeginInit();
            plMedicalRecords.SuspendLayout();
            plFeedingPlan.SuspendLayout();
            ssAnimalForm.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.AntiqueWhite;
            panel1.Controls.Add(dtpBirthday);
            panel1.Controls.Add(cbSpecies);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnNewLocation);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(plRelationship);
            panel1.Controls.Add(plMedicalRecords);
            panel1.Controls.Add(plFeedingPlan);
            panel1.Controls.Add(dtpExitDate);
            panel1.Controls.Add(dtpEntryDate);
            panel1.Controls.Add(cbBloodType);
            panel1.Controls.Add(lblBloodType);
            panel1.Controls.Add(lblLocation);
            panel1.Controls.Add(lblOrigin);
            panel1.Controls.Add(lblSpecies);
            panel1.Controls.Add(lblName);
            panel1.Controls.Add(cbLocationAnimalForm);
            panel1.Controls.Add(lblExitAnimalForm);
            panel1.Controls.Add(lblEntryAnimalForm);
            panel1.Controls.Add(tbOrigin);
            panel1.Controls.Add(tbNameAnimalForm);
            panel1.Controls.Add(lblAnimalManager);
            panel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            panel1.Location = new Point(11, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(1111, 615);
            panel1.TabIndex = 0;
            // 
            // dtpBirthday
            // 
            dtpBirthday.Font = new Font("Segoe UI", 9F);
            dtpBirthday.Location = new Point(117, 181);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(268, 27);
            dtpBirthday.TabIndex = 79;
            // 
            // cbSpecies
            // 
            cbSpecies.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbSpecies.FormattingEnabled = true;
            cbSpecies.Location = new Point(537, 75);
            cbSpecies.Margin = new Padding(2, 3, 2, 3);
            cbSpecies.Name = "cbSpecies";
            cbSpecies.Size = new Size(162, 28);
            cbSpecies.TabIndex = 81;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 187);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 78;
            label1.Text = "Birthday :";
            // 
            // btnNewLocation
            // 
            btnNewLocation.Location = new Point(470, 171);
            btnNewLocation.Name = "btnNewLocation";
            btnNewLocation.Size = new Size(177, 35);
            btnNewLocation.TabIndex = 80;
            btnNewLocation.Text = "New Location";
            btnNewLocation.UseVisualStyleBackColor = true;
            btnNewLocation.Click += btnNewLocation_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Animals;
            pictureBox2.Location = new Point(325, 21);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(85, 57);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 79;
            pictureBox2.TabStop = false;
            // 
            // plRelationship
            // 
            plRelationship.BackColor = Color.AntiqueWhite;
            plRelationship.BorderStyle = BorderStyle.FixedSingle;
            plRelationship.Controls.Add(dgvAnimalRelations);
            plRelationship.Controls.Add(btnDeleteRelation);
            plRelationship.Controls.Add(clbAnimals);
            plRelationship.Controls.Add(lblSearchRelations);
            plRelationship.Controls.Add(tbSearchRelations);
            plRelationship.Controls.Add(btnAddRelation);
            plRelationship.Controls.Add(lblRelationShips);
            plRelationship.Controls.Add(lblSearch);
            plRelationship.Controls.Add(lblRelations);
            plRelationship.Controls.Add(tbRelations);
            plRelationship.Controls.Add(tbSearch);
            plRelationship.Location = new Point(747, 81);
            plRelationship.Name = "plRelationship";
            plRelationship.Size = new Size(349, 517);
            plRelationship.TabIndex = 78;
            plRelationship.Visible = false;
            // 
            // dgvAnimalRelations
            // 
            dgvAnimalRelations.BackgroundColor = SystemColors.Window;
            dgvAnimalRelations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimalRelations.Location = new Point(22, 329);
            dgvAnimalRelations.Margin = new Padding(3, 4, 3, 4);
            dgvAnimalRelations.Name = "dgvAnimalRelations";
            dgvAnimalRelations.RowHeadersWidth = 51;
            dgvAnimalRelations.Size = new Size(310, 137);
            dgvAnimalRelations.TabIndex = 85;
            // 
            // btnDeleteRelation
            // 
            btnDeleteRelation.Location = new Point(263, 473);
            btnDeleteRelation.Name = "btnDeleteRelation";
            btnDeleteRelation.Size = new Size(69, 29);
            btnDeleteRelation.TabIndex = 84;
            btnDeleteRelation.Text = "Delete";
            btnDeleteRelation.UseVisualStyleBackColor = true;
            btnDeleteRelation.Click += btnDeleteRelation_Click;
            // 
            // clbAnimals
            // 
            clbAnimals.Font = new Font("Segoe UI", 9F);
            clbAnimals.FormattingEnabled = true;
            clbAnimals.Location = new Point(22, 75);
            clbAnimals.Name = "clbAnimals";
            clbAnimals.Size = new Size(309, 202);
            clbAnimals.TabIndex = 81;
            // 
            // lblSearchRelations
            // 
            lblSearchRelations.AutoSize = true;
            lblSearchRelations.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearchRelations.Location = new Point(22, 475);
            lblSearchRelations.Margin = new Padding(2, 0, 2, 0);
            lblSearchRelations.Name = "lblSearchRelations";
            lblSearchRelations.Size = new Size(73, 23);
            lblSearchRelations.TabIndex = 83;
            lblSearchRelations.Text = "Search :";
            // 
            // tbSearchRelations
            // 
            tbSearchRelations.Font = new Font("Segoe UI", 9F);
            tbSearchRelations.Location = new Point(99, 475);
            tbSearchRelations.Margin = new Padding(2, 3, 2, 3);
            tbSearchRelations.Name = "tbSearchRelations";
            tbSearchRelations.Size = new Size(159, 27);
            tbSearchRelations.TabIndex = 82;
            // 
            // btnAddRelation
            // 
            btnAddRelation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddRelation.Location = new Point(280, 296);
            btnAddRelation.Margin = new Padding(2, 3, 2, 3);
            btnAddRelation.Name = "btnAddRelation";
            btnAddRelation.Size = new Size(51, 27);
            btnAddRelation.TabIndex = 80;
            btnAddRelation.Text = "Add";
            btnAddRelation.UseVisualStyleBackColor = true;
            btnAddRelation.Click += btnAddRelation_Click;
            // 
            // lblRelationShips
            // 
            lblRelationShips.AutoSize = true;
            lblRelationShips.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRelationShips.Location = new Point(133, 7);
            lblRelationShips.Margin = new Padding(2, 0, 2, 0);
            lblRelationShips.Name = "lblRelationShips";
            lblRelationShips.Size = new Size(126, 25);
            lblRelationShips.TabIndex = 79;
            lblRelationShips.Text = "Relationships";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(22, 43);
            lblSearch.Margin = new Padding(2, 0, 2, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(73, 23);
            lblSearch.TabIndex = 9;
            lblSearch.Text = "Search :";
            // 
            // lblRelations
            // 
            lblRelations.AutoSize = true;
            lblRelations.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRelations.Location = new Point(21, 296);
            lblRelations.Margin = new Padding(2, 0, 2, 0);
            lblRelations.Name = "lblRelations";
            lblRelations.Size = new Size(93, 23);
            lblRelations.TabIndex = 6;
            lblRelations.Text = "Relations :";
            // 
            // tbRelations
            // 
            tbRelations.Font = new Font("Segoe UI", 9F);
            tbRelations.Location = new Point(117, 295);
            tbRelations.Margin = new Padding(2, 3, 2, 3);
            tbRelations.Name = "tbRelations";
            tbRelations.Size = new Size(159, 27);
            tbRelations.TabIndex = 8;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(99, 43);
            tbSearch.Margin = new Padding(2, 3, 2, 3);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(177, 27);
            tbSearch.TabIndex = 6;
            // 
            // plMedicalRecords
            // 
            plMedicalRecords.BackColor = Color.AntiqueWhite;
            plMedicalRecords.BorderStyle = BorderStyle.FixedSingle;
            plMedicalRecords.Controls.Add(lblMedicalRecords);
            plMedicalRecords.Controls.Add(lblMedication);
            plMedicalRecords.Controls.Add(lblNotesMedical);
            plMedicalRecords.Controls.Add(lblOperations);
            plMedicalRecords.Controls.Add(lblInjuries);
            plMedicalRecords.Controls.Add(lblDiseases);
            plMedicalRecords.Controls.Add(cbDangerlvl);
            plMedicalRecords.Controls.Add(lblDangerlvl);
            plMedicalRecords.Controls.Add(lblAllergies);
            plMedicalRecords.Controls.Add(tbDiseases);
            plMedicalRecords.Controls.Add(tbInjuries);
            plMedicalRecords.Controls.Add(tbOperations);
            plMedicalRecords.Controls.Add(tbNotesMedical);
            plMedicalRecords.Controls.Add(tbMedication);
            plMedicalRecords.Controls.Add(tbMedicalAllergies);
            plMedicalRecords.Location = new Point(393, 287);
            plMedicalRecords.Name = "plMedicalRecords";
            plMedicalRecords.Size = new Size(337, 311);
            plMedicalRecords.TabIndex = 76;
            // 
            // lblMedicalRecords
            // 
            lblMedicalRecords.AutoSize = true;
            lblMedicalRecords.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMedicalRecords.Location = new Point(101, 28);
            lblMedicalRecords.Margin = new Padding(2, 0, 2, 0);
            lblMedicalRecords.Name = "lblMedicalRecords";
            lblMedicalRecords.Size = new Size(152, 25);
            lblMedicalRecords.TabIndex = 77;
            lblMedicalRecords.Text = "Medical Records";
            // 
            // lblMedication
            // 
            lblMedication.AutoSize = true;
            lblMedication.Location = new Point(5, 235);
            lblMedication.Name = "lblMedication";
            lblMedication.Size = new Size(91, 20);
            lblMedication.TabIndex = 43;
            lblMedication.Text = "Medicaton :";
            // 
            // lblNotesMedical
            // 
            lblNotesMedical.AutoSize = true;
            lblNotesMedical.Location = new Point(37, 204);
            lblNotesMedical.Name = "lblNotesMedical";
            lblNotesMedical.Size = new Size(59, 20);
            lblNotesMedical.TabIndex = 42;
            lblNotesMedical.Text = "Notes :";
            // 
            // lblOperations
            // 
            lblOperations.AutoSize = true;
            lblOperations.Location = new Point(1, 171);
            lblOperations.Name = "lblOperations";
            lblOperations.Size = new Size(94, 20);
            lblOperations.TabIndex = 40;
            lblOperations.Text = "Operations :";
            // 
            // lblInjuries
            // 
            lblInjuries.AutoSize = true;
            lblInjuries.Location = new Point(26, 139);
            lblInjuries.Name = "lblInjuries";
            lblInjuries.Size = new Size(69, 20);
            lblInjuries.TabIndex = 39;
            lblInjuries.Text = "Injuries :";
            // 
            // lblDiseases
            // 
            lblDiseases.AutoSize = true;
            lblDiseases.Location = new Point(18, 105);
            lblDiseases.Name = "lblDiseases";
            lblDiseases.Size = new Size(77, 20);
            lblDiseases.TabIndex = 38;
            lblDiseases.Text = "Diseases :";
            // 
            // cbDangerlvl
            // 
            cbDangerlvl.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDangerlvl.Font = new Font("Segoe UI", 9F);
            cbDangerlvl.FormattingEnabled = true;
            cbDangerlvl.Location = new Point(101, 260);
            cbDangerlvl.Name = "cbDangerlvl";
            cbDangerlvl.Size = new Size(163, 28);
            cbDangerlvl.TabIndex = 37;
            // 
            // lblDangerlvl
            // 
            lblDangerlvl.AutoSize = true;
            lblDangerlvl.Location = new Point(8, 268);
            lblDangerlvl.Name = "lblDangerlvl";
            lblDangerlvl.Size = new Size(88, 20);
            lblDangerlvl.TabIndex = 36;
            lblDangerlvl.Text = "Danger lvl :";
            // 
            // lblAllergies
            // 
            lblAllergies.AutoSize = true;
            lblAllergies.Location = new Point(18, 75);
            lblAllergies.Name = "lblAllergies";
            lblAllergies.Size = new Size(78, 20);
            lblAllergies.TabIndex = 28;
            lblAllergies.Text = "Allergies :";
            // 
            // tbDiseases
            // 
            tbDiseases.Font = new Font("Segoe UI", 9F);
            tbDiseases.Location = new Point(101, 101);
            tbDiseases.Margin = new Padding(2, 3, 2, 3);
            tbDiseases.Name = "tbDiseases";
            tbDiseases.Size = new Size(164, 27);
            tbDiseases.TabIndex = 33;
            // 
            // tbInjuries
            // 
            tbInjuries.Font = new Font("Segoe UI", 9F);
            tbInjuries.Location = new Point(101, 131);
            tbInjuries.Margin = new Padding(2, 3, 2, 3);
            tbInjuries.Name = "tbInjuries";
            tbInjuries.Size = new Size(164, 27);
            tbInjuries.TabIndex = 32;
            // 
            // tbOperations
            // 
            tbOperations.Font = new Font("Segoe UI", 9F);
            tbOperations.Location = new Point(101, 163);
            tbOperations.Margin = new Padding(2, 3, 2, 3);
            tbOperations.Name = "tbOperations";
            tbOperations.Size = new Size(164, 27);
            tbOperations.TabIndex = 31;
            // 
            // tbNotesMedical
            // 
            tbNotesMedical.Font = new Font("Segoe UI", 9F);
            tbNotesMedical.Location = new Point(101, 196);
            tbNotesMedical.Margin = new Padding(2, 3, 2, 3);
            tbNotesMedical.Name = "tbNotesMedical";
            tbNotesMedical.Size = new Size(164, 27);
            tbNotesMedical.TabIndex = 29;
            // 
            // tbMedication
            // 
            tbMedication.Font = new Font("Segoe UI", 9F);
            tbMedication.Location = new Point(101, 228);
            tbMedication.Margin = new Padding(2, 3, 2, 3);
            tbMedication.Name = "tbMedication";
            tbMedication.Size = new Size(164, 27);
            tbMedication.TabIndex = 27;
            // 
            // tbMedicalAllergies
            // 
            tbMedicalAllergies.Font = new Font("Segoe UI", 9F);
            tbMedicalAllergies.Location = new Point(101, 69);
            tbMedicalAllergies.Margin = new Padding(2, 3, 2, 3);
            tbMedicalAllergies.Name = "tbMedicalAllergies";
            tbMedicalAllergies.Size = new Size(164, 27);
            tbMedicalAllergies.TabIndex = 26;
            // 
            // plFeedingPlan
            // 
            plFeedingPlan.BackColor = Color.AntiqueWhite;
            plFeedingPlan.BorderStyle = BorderStyle.FixedSingle;
            plFeedingPlan.Controls.Add(clbFoodAllergies);
            plFeedingPlan.Controls.Add(lblIdealWeight);
            plFeedingPlan.Controls.Add(lblWeight);
            plFeedingPlan.Controls.Add(lblCaloriesIntake);
            plFeedingPlan.Controls.Add(lblNotes);
            plFeedingPlan.Controls.Add(lblAllergiesFood);
            plFeedingPlan.Controls.Add(lblDislikedFood);
            plFeedingPlan.Controls.Add(lblFavoriteFood);
            plFeedingPlan.Controls.Add(lblDiet);
            plFeedingPlan.Controls.Add(cbDiet);
            plFeedingPlan.Controls.Add(lblFeedingPlanFeedingPlanForm);
            plFeedingPlan.Controls.Add(tbNotesFeeding);
            plFeedingPlan.Controls.Add(tbFavoriteFood);
            plFeedingPlan.Controls.Add(tbDislikedFood);
            plFeedingPlan.Controls.Add(tbCaloriesIntake);
            plFeedingPlan.Controls.Add(tbWeight);
            plFeedingPlan.Controls.Add(tbIdealWeight);
            plFeedingPlan.Location = new Point(37, 215);
            plFeedingPlan.Name = "plFeedingPlan";
            plFeedingPlan.Size = new Size(337, 383);
            plFeedingPlan.TabIndex = 75;
            // 
            // clbFoodAllergies
            // 
            clbFoodAllergies.Font = new Font("Segoe UI", 9F);
            clbFoodAllergies.FormattingEnabled = true;
            clbFoodAllergies.Location = new Point(136, 171);
            clbFoodAllergies.Name = "clbFoodAllergies";
            clbFoodAllergies.Size = new Size(164, 70);
            clbFoodAllergies.TabIndex = 80;
            // 
            // lblIdealWeight
            // 
            lblIdealWeight.AutoSize = true;
            lblIdealWeight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblIdealWeight.Location = new Point(21, 320);
            lblIdealWeight.Margin = new Padding(2, 0, 2, 0);
            lblIdealWeight.Name = "lblIdealWeight";
            lblIdealWeight.Size = new Size(106, 20);
            lblIdealWeight.TabIndex = 43;
            lblIdealWeight.Text = "Ideal Weight :";
            // 
            // lblWeight
            // 
            lblWeight.AutoSize = true;
            lblWeight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblWeight.Location = new Point(58, 289);
            lblWeight.Margin = new Padding(2, 0, 2, 0);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(68, 20);
            lblWeight.TabIndex = 42;
            lblWeight.Text = "Weight :";
            // 
            // lblCaloriesIntake
            // 
            lblCaloriesIntake.AutoSize = true;
            lblCaloriesIntake.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCaloriesIntake.Location = new Point(6, 259);
            lblCaloriesIntake.Margin = new Padding(2, 0, 2, 0);
            lblCaloriesIntake.Name = "lblCaloriesIntake";
            lblCaloriesIntake.Size = new Size(120, 20);
            lblCaloriesIntake.TabIndex = 41;
            lblCaloriesIntake.Text = "Calories Intake :";
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNotes.Location = new Point(67, 349);
            lblNotes.Margin = new Padding(2, 0, 2, 0);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(59, 20);
            lblNotes.TabIndex = 40;
            lblNotes.Text = "Notes :";
            // 
            // lblAllergiesFood
            // 
            lblAllergiesFood.AutoSize = true;
            lblAllergiesFood.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAllergiesFood.Location = new Point(48, 173);
            lblAllergiesFood.Margin = new Padding(2, 0, 2, 0);
            lblAllergiesFood.Name = "lblAllergiesFood";
            lblAllergiesFood.Size = new Size(78, 20);
            lblAllergiesFood.TabIndex = 39;
            lblAllergiesFood.Text = "Allergies :";
            // 
            // lblDislikedFood
            // 
            lblDislikedFood.AutoSize = true;
            lblDislikedFood.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDislikedFood.Location = new Point(19, 99);
            lblDislikedFood.Margin = new Padding(2, 0, 2, 0);
            lblDislikedFood.Name = "lblDislikedFood";
            lblDislikedFood.Size = new Size(111, 20);
            lblDislikedFood.TabIndex = 38;
            lblDislikedFood.Text = "Disliked Food :";
            // 
            // lblFavoriteFood
            // 
            lblFavoriteFood.AutoSize = true;
            lblFavoriteFood.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFavoriteFood.Location = new Point(21, 72);
            lblFavoriteFood.Margin = new Padding(2, 0, 2, 0);
            lblFavoriteFood.Name = "lblFavoriteFood";
            lblFavoriteFood.Size = new Size(113, 20);
            lblFavoriteFood.TabIndex = 37;
            lblFavoriteFood.Text = "Favorite Food :";
            // 
            // lblDiet
            // 
            lblDiet.AutoSize = true;
            lblDiet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDiet.Location = new Point(80, 131);
            lblDiet.Margin = new Padding(2, 0, 2, 0);
            lblDiet.Name = "lblDiet";
            lblDiet.Size = new Size(46, 20);
            lblDiet.TabIndex = 34;
            lblDiet.Text = "Diet :";
            // 
            // cbDiet
            // 
            cbDiet.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDiet.Font = new Font("Segoe UI", 9F);
            cbDiet.FormattingEnabled = true;
            cbDiet.Location = new Point(136, 128);
            cbDiet.Margin = new Padding(2, 3, 2, 3);
            cbDiet.Name = "cbDiet";
            cbDiet.Size = new Size(164, 28);
            cbDiet.TabIndex = 32;
            // 
            // lblFeedingPlanFeedingPlanForm
            // 
            lblFeedingPlanFeedingPlanForm.AutoSize = true;
            lblFeedingPlanFeedingPlanForm.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFeedingPlanFeedingPlanForm.Location = new Point(110, 23);
            lblFeedingPlanFeedingPlanForm.Margin = new Padding(2, 0, 2, 0);
            lblFeedingPlanFeedingPlanForm.Name = "lblFeedingPlanFeedingPlanForm";
            lblFeedingPlanFeedingPlanForm.Size = new Size(121, 25);
            lblFeedingPlanFeedingPlanForm.TabIndex = 28;
            lblFeedingPlanFeedingPlanForm.Text = "Feeding Plan";
            // 
            // tbNotesFeeding
            // 
            tbNotesFeeding.Font = new Font("Segoe UI", 9F);
            tbNotesFeeding.Location = new Point(136, 347);
            tbNotesFeeding.Margin = new Padding(2, 3, 2, 3);
            tbNotesFeeding.Name = "tbNotesFeeding";
            tbNotesFeeding.Size = new Size(164, 27);
            tbNotesFeeding.TabIndex = 24;
            // 
            // tbFavoriteFood
            // 
            tbFavoriteFood.Font = new Font("Segoe UI", 9F);
            tbFavoriteFood.Location = new Point(136, 69);
            tbFavoriteFood.Margin = new Padding(2, 3, 2, 3);
            tbFavoriteFood.Name = "tbFavoriteFood";
            tbFavoriteFood.Size = new Size(164, 27);
            tbFavoriteFood.TabIndex = 27;
            // 
            // tbDislikedFood
            // 
            tbDislikedFood.Font = new Font("Segoe UI", 9F);
            tbDislikedFood.Location = new Point(136, 99);
            tbDislikedFood.Margin = new Padding(2, 3, 2, 3);
            tbDislikedFood.Name = "tbDislikedFood";
            tbDislikedFood.Size = new Size(164, 27);
            tbDislikedFood.TabIndex = 26;
            // 
            // tbCaloriesIntake
            // 
            tbCaloriesIntake.Font = new Font("Segoe UI", 9F);
            tbCaloriesIntake.Location = new Point(136, 255);
            tbCaloriesIntake.Margin = new Padding(2, 3, 2, 3);
            tbCaloriesIntake.Name = "tbCaloriesIntake";
            tbCaloriesIntake.Size = new Size(164, 27);
            tbCaloriesIntake.TabIndex = 23;
            // 
            // tbWeight
            // 
            tbWeight.Font = new Font("Segoe UI", 9F);
            tbWeight.Location = new Point(136, 285);
            tbWeight.Margin = new Padding(2, 3, 2, 3);
            tbWeight.Name = "tbWeight";
            tbWeight.Size = new Size(164, 27);
            tbWeight.TabIndex = 22;
            // 
            // tbIdealWeight
            // 
            tbIdealWeight.Font = new Font("Segoe UI", 9F);
            tbIdealWeight.Location = new Point(136, 317);
            tbIdealWeight.Margin = new Padding(2, 3, 2, 3);
            tbIdealWeight.Name = "tbIdealWeight";
            tbIdealWeight.Size = new Size(164, 27);
            tbIdealWeight.TabIndex = 21;
            // 
            // dtpExitDate
            // 
            dtpExitDate.Font = new Font("Segoe UI", 9F);
            dtpExitDate.Location = new Point(117, 144);
            dtpExitDate.Name = "dtpExitDate";
            dtpExitDate.Size = new Size(268, 27);
            dtpExitDate.TabIndex = 73;
            // 
            // dtpEntryDate
            // 
            dtpEntryDate.Font = new Font("Segoe UI", 9F);
            dtpEntryDate.Location = new Point(117, 105);
            dtpEntryDate.Name = "dtpEntryDate";
            dtpEntryDate.Size = new Size(268, 27);
            dtpEntryDate.TabIndex = 72;
            // 
            // cbBloodType
            // 
            cbBloodType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBloodType.Font = new Font("Segoe UI", 9F);
            cbBloodType.FormattingEnabled = true;
            cbBloodType.Location = new Point(535, 212);
            cbBloodType.Name = "cbBloodType";
            cbBloodType.Size = new Size(164, 28);
            cbBloodType.TabIndex = 71;
            // 
            // lblBloodType
            // 
            lblBloodType.AutoSize = true;
            lblBloodType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBloodType.Location = new Point(439, 215);
            lblBloodType.Margin = new Padding(2, 0, 2, 0);
            lblBloodType.Name = "lblBloodType";
            lblBloodType.Size = new Size(91, 20);
            lblBloodType.TabIndex = 69;
            lblBloodType.Text = "BloodType :";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLocation.Location = new Point(453, 140);
            lblLocation.Margin = new Padding(2, 0, 2, 0);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(77, 20);
            lblLocation.TabIndex = 68;
            lblLocation.Text = "Location :";
            // 
            // lblOrigin
            // 
            lblOrigin.AutoSize = true;
            lblOrigin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOrigin.Location = new Point(470, 108);
            lblOrigin.Margin = new Padding(2, 0, 2, 0);
            lblOrigin.Name = "lblOrigin";
            lblOrigin.Size = new Size(60, 20);
            lblOrigin.TabIndex = 67;
            lblOrigin.Text = "Origin :";
            // 
            // lblSpecies
            // 
            lblSpecies.AutoSize = true;
            lblSpecies.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSpecies.Location = new Point(458, 77);
            lblSpecies.Margin = new Padding(2, 0, 2, 0);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new Size(72, 20);
            lblSpecies.TabIndex = 66;
            lblSpecies.Text = "Species  :";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.Location = new Point(54, 73);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(59, 20);
            lblName.TabIndex = 65;
            lblName.Text = "Name :";
            // 
            // cbLocationAnimalForm
            // 
            cbLocationAnimalForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLocationAnimalForm.Font = new Font("Segoe UI", 9F);
            cbLocationAnimalForm.FormattingEnabled = true;
            cbLocationAnimalForm.Location = new Point(537, 137);
            cbLocationAnimalForm.Name = "cbLocationAnimalForm";
            cbLocationAnimalForm.Size = new Size(164, 28);
            cbLocationAnimalForm.TabIndex = 64;
            // 
            // lblExitAnimalForm
            // 
            lblExitAnimalForm.AutoSize = true;
            lblExitAnimalForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblExitAnimalForm.Location = new Point(70, 149);
            lblExitAnimalForm.Margin = new Padding(2, 0, 2, 0);
            lblExitAnimalForm.Name = "lblExitAnimalForm";
            lblExitAnimalForm.Size = new Size(43, 20);
            lblExitAnimalForm.TabIndex = 45;
            lblExitAnimalForm.Text = "Exit :";
            // 
            // lblEntryAnimalForm
            // 
            lblEntryAnimalForm.AutoSize = true;
            lblEntryAnimalForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEntryAnimalForm.Location = new Point(58, 108);
            lblEntryAnimalForm.Margin = new Padding(2, 0, 2, 0);
            lblEntryAnimalForm.Name = "lblEntryAnimalForm";
            lblEntryAnimalForm.Size = new Size(55, 20);
            lblEntryAnimalForm.TabIndex = 44;
            lblEntryAnimalForm.Text = "Entry :";
            // 
            // tbOrigin
            // 
            tbOrigin.Font = new Font("Segoe UI", 9F);
            tbOrigin.Location = new Point(537, 105);
            tbOrigin.Margin = new Padding(2, 3, 2, 3);
            tbOrigin.Name = "tbOrigin";
            tbOrigin.Size = new Size(164, 27);
            tbOrigin.TabIndex = 42;
            // 
            // tbNameAnimalForm
            // 
            tbNameAnimalForm.Font = new Font("Segoe UI", 9F);
            tbNameAnimalForm.Location = new Point(117, 69);
            tbNameAnimalForm.Margin = new Padding(2, 3, 2, 3);
            tbNameAnimalForm.Name = "tbNameAnimalForm";
            tbNameAnimalForm.Size = new Size(164, 27);
            tbNameAnimalForm.TabIndex = 39;
            // 
            // lblAnimalManager
            // 
            lblAnimalManager.AutoSize = true;
            lblAnimalManager.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblAnimalManager.Location = new Point(433, 11);
            lblAnimalManager.Name = "lblAnimalManager";
            lblAnimalManager.Size = new Size(286, 46);
            lblAnimalManager.TabIndex = 36;
            lblAnimalManager.Text = "Animal Manager";
            // 
            // btnSaveAnimalForm
            // 
            btnSaveAnimalForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSaveAnimalForm.Location = new Point(1141, 305);
            btnSaveAnimalForm.Margin = new Padding(2, 3, 2, 3);
            btnSaveAnimalForm.Name = "btnSaveAnimalForm";
            btnSaveAnimalForm.Size = new Size(107, 47);
            btnSaveAnimalForm.TabIndex = 47;
            btnSaveAnimalForm.Text = "Add";
            btnSaveAnimalForm.UseVisualStyleBackColor = true;
            btnSaveAnimalForm.Click += btnSaveAnimalForm_Click;
            // 
            // btnBackAnimalForm
            // 
            btnBackAnimalForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBackAnimalForm.Location = new Point(1141, 365);
            btnBackAnimalForm.Margin = new Padding(2, 3, 2, 3);
            btnBackAnimalForm.Name = "btnBackAnimalForm";
            btnBackAnimalForm.Size = new Size(107, 47);
            btnBackAnimalForm.TabIndex = 46;
            btnBackAnimalForm.Text = "Back";
            btnBackAnimalForm.UseVisualStyleBackColor = true;
            btnBackAnimalForm.Click += btnBackAnimalForm_Click;
            // 
            // ssAnimalForm
            // 
            ssAnimalForm.BackColor = Color.DarkSeaGreen;
            ssAnimalForm.ImageScalingSize = new Size(24, 24);
            ssAnimalForm.Items.AddRange(new ToolStripItem[] { tsslblAnimalForm });
            ssAnimalForm.Location = new Point(0, 638);
            ssAnimalForm.Name = "ssAnimalForm";
            ssAnimalForm.Padding = new Padding(1, 0, 11, 0);
            ssAnimalForm.Size = new Size(1257, 22);
            ssAnimalForm.TabIndex = 1;
            ssAnimalForm.Text = "statusStrip1";
            // 
            // tsslblAnimalForm
            // 
            tsslblAnimalForm.ForeColor = Color.Red;
            tsslblAnimalForm.Name = "tsslblAnimalForm";
            tsslblAnimalForm.Size = new Size(0, 16);
            // 
            // AnimalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1257, 660);
            Controls.Add(ssAnimalForm);
            Controls.Add(panel1);
            Controls.Add(btnSaveAnimalForm);
            Controls.Add(btnBackAnimalForm);
            Margin = new Padding(2, 3, 2, 3);
            MaximumSize = new Size(1275, 707);
            MinimumSize = new Size(1275, 707);
            Name = "AnimalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AnimalManager";
            FormClosed += AnimalManager_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            plRelationship.ResumeLayout(false);
            plRelationship.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimalRelations).EndInit();
            plMedicalRecords.ResumeLayout(false);
            plMedicalRecords.PerformLayout();
            plFeedingPlan.ResumeLayout(false);
            plFeedingPlan.PerformLayout();
            ssAnimalForm.ResumeLayout(false);
            ssAnimalForm.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBox2;
        private Panel panel1;
        private Button btnSaveAnimalForm;
        private Button btnBackAnimalForm;
        private Label lblExitAnimalForm;
        private Label lblEntryAnimalForm;
        private TextBox tbOrigin;
        private TextBox tbNameAnimalForm;
        private Label lblAnimalManager;
        private StatusStrip ssAnimalForm;
        private ToolStripStatusLabel tsslblAnimalForm;
        private ComboBox cbLocationAnimalForm;
        private Label lblLocation;
        private Label lblOrigin;
        private Label lblSpecies;
        private Label lblName;
        private ComboBox cbBloodType;
        private Label lblBloodType;
        private DateTimePicker dtpEntryDate;
        private DateTimePicker dtpExitDate;
        private Panel plFeedingPlan;
        private Label lblFeedingPlanFeedingPlanForm;
        private TextBox tbFavoriteFood;
        private TextBox tbDislikedFood;
        private TextBox tbFoodAllergies;
        private TextBox tbNotesFeeding;
        private TextBox tbCaloriesIntake;
        private TextBox tbWeight;
        private TextBox tbIdealWeight;
        private Panel plMedicalRecords;
        private Label lblMedicalRecords;
        private Label lblMedication;
        private Label lblNotesMedical;
        private Label lblOperations;
        private Label lblInjuries;
        private Label lblDiseases;
        private ComboBox cbDangerlvl;
        private Label lblDangerlvl;
        private Label lblAllergies;
        private TextBox tbDiseases;
        private TextBox tbInjuries;
        private TextBox tbOperations;
        private TextBox tbNotesMedical;
        private TextBox tbMedication;
        private Panel plRelationship;
        private Label lblRelationShips;
        private Label lblSearch;
        private Label lblRelations;
        private TextBox tbRelations;
        private TextBox tbSearch;
        private Button btnAddRelation;
        private ListBox lbAnimals;
        private Label lblSearchRelations;
        private TextBox tbSearchRelations;
        private Label lblIdealWeight;
        private Label lblWeight;
        private Label lblCaloriesIntake;
        private Label lblNotes;
        private Label lblAllergiesFood;
        private Label lblDislikedFood;
        private Label lblFavoriteFood;
        private PictureBox pictureBox2;
        private TextBox tbMedicalAllergies;
        private CheckedListBox clbFoodAllergies;
        private Button btnNewLocation;
        private CheckedListBox clbAnimals;
        private Label label1;
        private DateTimePicker dtpBirthday;
        private Button btnDeleteRelation;
        private DataGridViewTextBoxColumn relationshipsDataGridViewTextBoxColumn;
        private ComboBox cbSpecies;
        private Label lblDiet;
        private ComboBox cbDiet;
        private DataGridView dgvAnimalRelations;
    }
}