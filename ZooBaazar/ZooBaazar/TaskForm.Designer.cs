namespace ZooBaazar
{
    partial class TasksForm
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
            btnAddTasksForm = new Button();
            btnBackTasksForm = new Button();
            ssTaskForm = new StatusStrip();
            tsslblTaskForm = new ToolStripStatusLabel();
            panel1 = new Panel();
            cbEmployee = new ComboBox();
            label1 = new Label();
            lblEmployeeTasks = new Label();
            nudDurationTasks = new NumericUpDown();
            cbNightTask = new CheckBox();
            cbDayTask = new CheckBox();
            nudNumberOfRepeatsTasks = new NumericUpDown();
            pictureBox4 = new PictureBox();
            cbLocationTasksForm = new ComboBox();
            cbRepeatTasksForm = new ComboBox();
            lblRepeatTasksForm = new Label();
            lblEndTasksForm = new Label();
            lblStartTasksForm = new Label();
            dpStartTasksEmployee = new DateTimePicker();
            lblDeadLine = new Label();
            lblFunction = new Label();
            cbFunction = new ComboBox();
            tbDescriptionTasksForm = new TextBox();
            lblDescriptionTasksForm = new Label();
            lblLocationTasksForm = new Label();
            tbTitle = new TextBox();
            lblTitle = new Label();
            lblTasks = new Label();
            ssTaskForm.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDurationTasks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfRepeatsTasks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // btnAddTasksForm
            // 
            btnAddTasksForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAddTasksForm.Location = new Point(998, 229);
            btnAddTasksForm.Margin = new Padding(3, 2, 3, 2);
            btnAddTasksForm.Name = "btnAddTasksForm";
            btnAddTasksForm.Size = new Size(94, 35);
            btnAddTasksForm.TabIndex = 16;
            btnAddTasksForm.Text = "Add";
            btnAddTasksForm.UseVisualStyleBackColor = true;
            btnAddTasksForm.Click += BtnAddTask_Click;
            // 
            // btnBackTasksForm
            // 
            btnBackTasksForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnBackTasksForm.Location = new Point(998, 274);
            btnBackTasksForm.Margin = new Padding(3, 2, 3, 2);
            btnBackTasksForm.Name = "btnBackTasksForm";
            btnBackTasksForm.Size = new Size(94, 35);
            btnBackTasksForm.TabIndex = 19;
            btnBackTasksForm.Text = "Back";
            btnBackTasksForm.UseVisualStyleBackColor = true;
            btnBackTasksForm.Click += btnBack_Click;
            // 
            // ssTaskForm
            // 
            ssTaskForm.BackColor = Color.DarkSeaGreen;
            ssTaskForm.ImageScalingSize = new Size(24, 24);
            ssTaskForm.Items.AddRange(new ToolStripItem[] { tsslblTaskForm });
            ssTaskForm.Location = new Point(0, 486);
            ssTaskForm.Name = "ssTaskForm";
            ssTaskForm.Padding = new Padding(1, 0, 10, 0);
            ssTaskForm.Size = new Size(1104, 22);
            ssTaskForm.TabIndex = 20;
            ssTaskForm.Text = "statusStrip1";
            // 
            // tsslblTaskForm
            // 
            tsslblTaskForm.Name = "tsslblTaskForm";
            tsslblTaskForm.Size = new Size(0, 17);
            // 
            // panel1
            // 
            panel1.BackColor = Color.AntiqueWhite;
            panel1.Controls.Add(cbEmployee);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblEmployeeTasks);
            panel1.Controls.Add(nudDurationTasks);
            panel1.Controls.Add(cbNightTask);
            panel1.Controls.Add(cbDayTask);
            panel1.Controls.Add(nudNumberOfRepeatsTasks);
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(cbLocationTasksForm);
            panel1.Controls.Add(cbRepeatTasksForm);
            panel1.Controls.Add(lblRepeatTasksForm);
            panel1.Controls.Add(lblEndTasksForm);
            panel1.Controls.Add(lblStartTasksForm);
            panel1.Controls.Add(dpStartTasksEmployee);
            panel1.Controls.Add(lblDeadLine);
            panel1.Controls.Add(lblFunction);
            panel1.Controls.Add(cbFunction);
            panel1.Controls.Add(tbDescriptionTasksForm);
            panel1.Controls.Add(lblDescriptionTasksForm);
            panel1.Controls.Add(lblLocationTasksForm);
            panel1.Controls.Add(tbTitle);
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(lblTasks);
            panel1.Font = new Font("Segoe UI", 11F);
            panel1.Location = new Point(10, 22);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(972, 461);
            panel1.TabIndex = 21;
            // 
            // cbEmployee
            // 
            cbEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEmployee.FormattingEnabled = true;
            cbEmployee.Location = new Point(386, 298);
            cbEmployee.Name = "cbEmployee";
            cbEmployee.Size = new Size(213, 28);
            cbEmployee.TabIndex = 65;
            cbEmployee.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(891, 234);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 64;
            label1.Text = "Hours";
            // 
            // lblEmployeeTasks
            // 
            lblEmployeeTasks.AutoSize = true;
            lblEmployeeTasks.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployeeTasks.Location = new Point(386, 266);
            lblEmployeeTasks.Name = "lblEmployeeTasks";
            lblEmployeeTasks.Size = new Size(98, 21);
            lblEmployeeTasks.TabIndex = 63;
            lblEmployeeTasks.Text = "Employee : ";
            lblEmployeeTasks.Visible = false;
            // 
            // nudDurationTasks
            // 
            nudDurationTasks.Location = new Point(801, 232);
            nudDurationTasks.Margin = new Padding(3, 2, 3, 2);
            nudDurationTasks.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudDurationTasks.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDurationTasks.Name = "nudDurationTasks";
            nudDurationTasks.Size = new Size(88, 27);
            nudDurationTasks.TabIndex = 61;
            nudDurationTasks.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cbNightTask
            // 
            cbNightTask.AutoSize = true;
            cbNightTask.Location = new Point(655, 401);
            cbNightTask.Name = "cbNightTask";
            cbNightTask.Size = new Size(96, 24);
            cbNightTask.TabIndex = 60;
            cbNightTask.Text = "Night Task";
            cbNightTask.UseVisualStyleBackColor = true;
            // 
            // cbDayTask
            // 
            cbDayTask.AutoSize = true;
            cbDayTask.Location = new Point(655, 368);
            cbDayTask.Name = "cbDayTask";
            cbDayTask.Size = new Size(85, 24);
            cbDayTask.TabIndex = 59;
            cbDayTask.Text = "Day Task";
            cbDayTask.UseVisualStyleBackColor = true;
            // 
            // nudNumberOfRepeatsTasks
            // 
            nudNumberOfRepeatsTasks.Location = new Point(776, 329);
            nudNumberOfRepeatsTasks.Margin = new Padding(3, 2, 3, 2);
            nudNumberOfRepeatsTasks.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudNumberOfRepeatsTasks.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNumberOfRepeatsTasks.Name = "nudNumberOfRepeatsTasks";
            nudNumberOfRepeatsTasks.Size = new Size(109, 27);
            nudNumberOfRepeatsTasks.TabIndex = 58;
            nudNumberOfRepeatsTasks.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Tasks;
            pictureBox4.Location = new Point(386, 85);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(213, 142);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 57;
            pictureBox4.TabStop = false;
            // 
            // cbLocationTasksForm
            // 
            cbLocationTasksForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLocationTasksForm.FormattingEnabled = true;
            cbLocationTasksForm.Location = new Point(655, 85);
            cbLocationTasksForm.Margin = new Padding(3, 2, 3, 2);
            cbLocationTasksForm.Name = "cbLocationTasksForm";
            cbLocationTasksForm.Size = new Size(234, 28);
            cbLocationTasksForm.TabIndex = 55;
            // 
            // cbRepeatTasksForm
            // 
            cbRepeatTasksForm.FormattingEnabled = true;
            cbRepeatTasksForm.Location = new Point(655, 328);
            cbRepeatTasksForm.Margin = new Padding(3, 2, 3, 2);
            cbRepeatTasksForm.Name = "cbRepeatTasksForm";
            cbRepeatTasksForm.Size = new Size(117, 28);
            cbRepeatTasksForm.TabIndex = 54;
            // 
            // lblRepeatTasksForm
            // 
            lblRepeatTasksForm.AutoSize = true;
            lblRepeatTasksForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRepeatTasksForm.Location = new Point(652, 305);
            lblRepeatTasksForm.Name = "lblRepeatTasksForm";
            lblRepeatTasksForm.Size = new Size(75, 21);
            lblRepeatTasksForm.TabIndex = 53;
            lblRepeatTasksForm.Text = "Repeat : ";
            // 
            // lblEndTasksForm
            // 
            lblEndTasksForm.AutoSize = true;
            lblEndTasksForm.Location = new Point(721, 234);
            lblEndTasksForm.Name = "lblEndTasksForm";
            lblEndTasksForm.Size = new Size(74, 20);
            lblEndTasksForm.TabIndex = 51;
            lblEndTasksForm.Text = "Duration :";
            // 
            // lblStartTasksForm
            // 
            lblStartTasksForm.AutoSize = true;
            lblStartTasksForm.Location = new Point(652, 171);
            lblStartTasksForm.Name = "lblStartTasksForm";
            lblStartTasksForm.Size = new Size(47, 20);
            lblStartTasksForm.TabIndex = 50;
            lblStartTasksForm.Text = "Start :";
            // 
            // dpStartTasksEmployee
            // 
            dpStartTasksEmployee.Checked = false;
            dpStartTasksEmployee.ImeMode = ImeMode.NoControl;
            dpStartTasksEmployee.Location = new Point(654, 193);
            dpStartTasksEmployee.Margin = new Padding(3, 2, 3, 2);
            dpStartTasksEmployee.Name = "dpStartTasksEmployee";
            dpStartTasksEmployee.Size = new Size(235, 27);
            dpStartTasksEmployee.TabIndex = 49;
            // 
            // lblDeadLine
            // 
            lblDeadLine.AutoSize = true;
            lblDeadLine.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDeadLine.Location = new Point(652, 143);
            lblDeadLine.Name = "lblDeadLine";
            lblDeadLine.Size = new Size(94, 21);
            lblDeadLine.TabIndex = 48;
            lblDeadLine.Text = "DeadLine : ";
            // 
            // lblFunction
            // 
            lblFunction.AutoSize = true;
            lblFunction.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFunction.Location = new Point(29, 131);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(89, 21);
            lblFunction.TabIndex = 45;
            lblFunction.Text = "Function : ";
            // 
            // cbFunction
            // 
            cbFunction.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFunction.FormattingEnabled = true;
            cbFunction.Location = new Point(29, 154);
            cbFunction.Margin = new Padding(3, 2, 3, 2);
            cbFunction.Name = "cbFunction";
            cbFunction.Size = new Size(234, 28);
            cbFunction.TabIndex = 44;
            // 
            // tbDescriptionTasksForm
            // 
            tbDescriptionTasksForm.Location = new Point(29, 252);
            tbDescriptionTasksForm.Margin = new Padding(3, 2, 3, 2);
            tbDescriptionTasksForm.Multiline = true;
            tbDescriptionTasksForm.Name = "tbDescriptionTasksForm";
            tbDescriptionTasksForm.Size = new Size(329, 198);
            tbDescriptionTasksForm.TabIndex = 43;
            // 
            // lblDescriptionTasksForm
            // 
            lblDescriptionTasksForm.AutoSize = true;
            lblDescriptionTasksForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescriptionTasksForm.Location = new Point(29, 219);
            lblDescriptionTasksForm.Name = "lblDescriptionTasksForm";
            lblDescriptionTasksForm.Size = new Size(110, 21);
            lblDescriptionTasksForm.TabIndex = 42;
            lblDescriptionTasksForm.Text = "Description : ";
            // 
            // lblLocationTasksForm
            // 
            lblLocationTasksForm.AutoSize = true;
            lblLocationTasksForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLocationTasksForm.Location = new Point(655, 50);
            lblLocationTasksForm.Name = "lblLocationTasksForm";
            lblLocationTasksForm.Size = new Size(88, 21);
            lblLocationTasksForm.TabIndex = 41;
            lblLocationTasksForm.Text = "Location : ";
            // 
            // tbTitle
            // 
            tbTitle.Location = new Point(29, 94);
            tbTitle.Margin = new Padding(3, 2, 3, 2);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new Size(234, 27);
            tbTitle.TabIndex = 38;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(29, 73);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(56, 21);
            lblTitle.TabIndex = 37;
            lblTitle.Text = "Title : ";
            // 
            // lblTasks
            // 
            lblTasks.AutoSize = true;
            lblTasks.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTasks.Location = new Point(447, 23);
            lblTasks.Name = "lblTasks";
            lblTasks.Size = new Size(85, 37);
            lblTasks.TabIndex = 56;
            lblTasks.Text = "Tasks";
            // 
            // TasksForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1104, 508);
            Controls.Add(panel1);
            Controls.Add(ssTaskForm);
            Controls.Add(btnBackTasksForm);
            Controls.Add(btnAddTasksForm);
            Margin = new Padding(3, 2, 3, 2);
            MaximumSize = new Size(1120, 547);
            MinimumSize = new Size(1120, 547);
            Name = "TasksForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TasksFormcs";
            FormClosed += TasksForm_FormClosed;
            ssTaskForm.ResumeLayout(false);
            ssTaskForm.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudDurationTasks).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfRepeatsTasks).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddTasksForm;
        private Button btnBackTasksForm;
        private StatusStrip ssTaskForm;
        private ToolStripStatusLabel tsslblTaskForm;
        private Panel panel1;
        private Label lblTasks;
        private ComboBox cbLocationTasksForm;
        private ComboBox cbRepeatTasksForm;
        private Label lblRepeatTasksForm;
        private Label lblEndTasksForm;
        private Label lblStartTasksForm;
        private DateTimePicker dpStartTasksEmployee;
        private Label lblDeadLine;
        private Label lblFunction;
        private ComboBox cbFunction;
        private TextBox tbDescriptionTasksForm;
        private Label lblDescriptionTasksForm;
        private Label lblLocationTasksForm;
        private TextBox tbTitle;
        private Label lblTitle;
        private PictureBox pictureBox4;
        private NumericUpDown nudNumberOfRepeatsTasks;
        private CheckBox cbNightTask;
        private CheckBox cbDayTask;
        private NumericUpDown nudDurationTasks;
        private Label lblEmployeeTasks;
        private ComboBox cbEmployee;
        private Label label1;
    }
}