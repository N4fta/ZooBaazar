namespace ZooBaazar
{
    partial class AddEmployeeForm
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
            btnAddEmployeeForm = new Button();
            btnCancelEmployeeForm = new Button();
            PanelEmployee = new Panel();
            lblEmployeeManager = new Label();
            panel1 = new Panel();
            pictureBox5 = new PictureBox();
            tbNotesEmployeeForm = new TextBox();
            lblNotesEmployeeForm = new Label();
            lblEmployeeDetails = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblDateOfBirth = new Label();
            tbPasswordEmployeeForm = new TextBox();
            lblPasswordEmployeeForm = new Label();
            tbUsernameEmployeeForm = new TextBox();
            lblUsernameEmployeeForm = new Label();
            tbPhoneNumber = new TextBox();
            lblPhoneNumber = new Label();
            tbEmail = new TextBox();
            lblEmail = new Label();
            tbAdress = new TextBox();
            lblAdress = new Label();
            lblNameEmployeeForm = new Label();
            tbNameEmployeeForm = new TextBox();
            PanelContract = new Panel();
            tbContractNotes = new TextBox();
            lblContractNotes = new Label();
            chkDayShifts = new CheckBox();
            chkNightShifts = new CheckBox();
            lblContractDetails = new Label();
            pbEmployeeForm = new PictureBox();
            clbWorkDays = new CheckedListBox();
            lblContractType = new Label();
            cbContractType = new ComboBox();
            tbUnpaidLeaveDays = new TextBox();
            lblUnpaidLeaveDays = new Label();
            lblPaidLeaveDays = new Label();
            tbPaidLeaveDays = new TextBox();
            lblWorkDays = new Label();
            chkOvertime = new CheckBox();
            chkChangeShifts = new CheckBox();
            lblHoursPerWeek = new Label();
            tbHoursPerWeek = new TextBox();
            tbBankNumber = new TextBox();
            lblBankNumber = new Label();
            tbBSN = new TextBox();
            lblBSN = new Label();
            lblRole = new Label();
            cbRole = new ComboBox();
            tbSalary = new TextBox();
            lblSalary = new Label();
            dtpEndDate = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            lblStartDate = new Label();
            ssAddEmployeeForm = new StatusStrip();
            tsslblAddEmployeeForm = new ToolStripStatusLabel();
            PanelEmployee.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            PanelContract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbEmployeeForm).BeginInit();
            ssAddEmployeeForm.SuspendLayout();
            SuspendLayout();
            // 
            // btnAddEmployeeForm
            // 
            btnAddEmployeeForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAddEmployeeForm.Location = new Point(1141, 305);
            btnAddEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            btnAddEmployeeForm.Name = "btnAddEmployeeForm";
            btnAddEmployeeForm.Size = new Size(107, 47);
            btnAddEmployeeForm.TabIndex = 101;
            btnAddEmployeeForm.Text = "Add";
            btnAddEmployeeForm.UseVisualStyleBackColor = true;
            btnAddEmployeeForm.Click += btnAddEmployee_Click;
            // 
            // btnCancelEmployeeForm
            // 
            btnCancelEmployeeForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancelEmployeeForm.Location = new Point(1141, 365);
            btnCancelEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            btnCancelEmployeeForm.Name = "btnCancelEmployeeForm";
            btnCancelEmployeeForm.Size = new Size(107, 47);
            btnCancelEmployeeForm.TabIndex = 100;
            btnCancelEmployeeForm.Text = "Back";
            btnCancelEmployeeForm.UseVisualStyleBackColor = true;
            btnCancelEmployeeForm.Click += btnCancel_Click;
            // 
            // PanelEmployee
            // 
            PanelEmployee.BackColor = Color.AntiqueWhite;
            PanelEmployee.Controls.Add(lblEmployeeManager);
            PanelEmployee.Controls.Add(panel1);
            PanelEmployee.Controls.Add(PanelContract);
            PanelEmployee.Location = new Point(12, 37);
            PanelEmployee.Name = "PanelEmployee";
            PanelEmployee.Size = new Size(1111, 615);
            PanelEmployee.TabIndex = 144;
            // 
            // lblEmployeeManager
            // 
            lblEmployeeManager.AutoSize = true;
            lblEmployeeManager.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblEmployeeManager.Location = new Point(391, 11);
            lblEmployeeManager.Name = "lblEmployeeManager";
            lblEmployeeManager.Size = new Size(327, 46);
            lblEmployeeManager.TabIndex = 147;
            lblEmployeeManager.Text = "Employee Manager";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox5);
            panel1.Controls.Add(tbNotesEmployeeForm);
            panel1.Controls.Add(lblNotesEmployeeForm);
            panel1.Controls.Add(lblEmployeeDetails);
            panel1.Controls.Add(dtpDateOfBirth);
            panel1.Controls.Add(lblDateOfBirth);
            panel1.Controls.Add(tbPasswordEmployeeForm);
            panel1.Controls.Add(lblPasswordEmployeeForm);
            panel1.Controls.Add(tbUsernameEmployeeForm);
            panel1.Controls.Add(lblUsernameEmployeeForm);
            panel1.Controls.Add(tbPhoneNumber);
            panel1.Controls.Add(lblPhoneNumber);
            panel1.Controls.Add(tbEmail);
            panel1.Controls.Add(lblEmail);
            panel1.Controls.Add(tbAdress);
            panel1.Controls.Add(lblAdress);
            panel1.Controls.Add(lblNameEmployeeForm);
            panel1.Controls.Add(tbNameEmployeeForm);
            panel1.Location = new Point(18, 72);
            panel1.Name = "panel1";
            panel1.Size = new Size(413, 488);
            panel1.TabIndex = 146;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.employees;
            pictureBox5.Location = new Point(287, 110);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(109, 97);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 149;
            pictureBox5.TabStop = false;
            // 
            // tbNotesEmployeeForm
            // 
            tbNotesEmployeeForm.Font = new Font("Segoe UI", 9F);
            tbNotesEmployeeForm.Location = new Point(137, 358);
            tbNotesEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            tbNotesEmployeeForm.Multiline = true;
            tbNotesEmployeeForm.Name = "tbNotesEmployeeForm";
            tbNotesEmployeeForm.Size = new Size(268, 112);
            tbNotesEmployeeForm.TabIndex = 149;
            // 
            // lblNotesEmployeeForm
            // 
            lblNotesEmployeeForm.AutoSize = true;
            lblNotesEmployeeForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNotesEmployeeForm.Location = new Point(71, 362);
            lblNotesEmployeeForm.Name = "lblNotesEmployeeForm";
            lblNotesEmployeeForm.Size = new Size(55, 20);
            lblNotesEmployeeForm.TabIndex = 148;
            lblNotesEmployeeForm.Text = "Notes:";
            // 
            // lblEmployeeDetails
            // 
            lblEmployeeDetails.AutoSize = true;
            lblEmployeeDetails.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployeeDetails.Location = new Point(137, 14);
            lblEmployeeDetails.Name = "lblEmployeeDetails";
            lblEmployeeDetails.Size = new Size(158, 25);
            lblEmployeeDetails.TabIndex = 146;
            lblEmployeeDetails.Text = "Employee Details";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Font = new Font("Segoe UI", 9F);
            dtpDateOfBirth.Location = new Point(137, 321);
            dtpDateOfBirth.Margin = new Padding(3, 4, 3, 4);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(268, 27);
            dtpDateOfBirth.TabIndex = 147;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDateOfBirth.Location = new Point(19, 325);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(106, 20);
            lblDateOfBirth.TabIndex = 146;
            lblDateOfBirth.Text = "Date Of Birth:";
            // 
            // tbPasswordEmployeeForm
            // 
            tbPasswordEmployeeForm.Font = new Font("Segoe UI", 9F);
            tbPasswordEmployeeForm.Location = new Point(137, 277);
            tbPasswordEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            tbPasswordEmployeeForm.Name = "tbPasswordEmployeeForm";
            tbPasswordEmployeeForm.PasswordChar = '*';
            tbPasswordEmployeeForm.Size = new Size(128, 27);
            tbPasswordEmployeeForm.TabIndex = 145;
            // 
            // lblPasswordEmployeeForm
            // 
            lblPasswordEmployeeForm.AutoSize = true;
            lblPasswordEmployeeForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPasswordEmployeeForm.Location = new Point(49, 280);
            lblPasswordEmployeeForm.Name = "lblPasswordEmployeeForm";
            lblPasswordEmployeeForm.Size = new Size(80, 20);
            lblPasswordEmployeeForm.TabIndex = 144;
            lblPasswordEmployeeForm.Text = "Password:";
            // 
            // tbUsernameEmployeeForm
            // 
            tbUsernameEmployeeForm.Font = new Font("Segoe UI", 9F);
            tbUsernameEmployeeForm.Location = new Point(137, 228);
            tbUsernameEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            tbUsernameEmployeeForm.Name = "tbUsernameEmployeeForm";
            tbUsernameEmployeeForm.Size = new Size(128, 27);
            tbUsernameEmployeeForm.TabIndex = 143;
            // 
            // lblUsernameEmployeeForm
            // 
            lblUsernameEmployeeForm.AutoSize = true;
            lblUsernameEmployeeForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsernameEmployeeForm.Location = new Point(43, 232);
            lblUsernameEmployeeForm.Name = "lblUsernameEmployeeForm";
            lblUsernameEmployeeForm.Size = new Size(84, 20);
            lblUsernameEmployeeForm.TabIndex = 142;
            lblUsernameEmployeeForm.Text = "Username:";
            // 
            // tbPhoneNumber
            // 
            tbPhoneNumber.Font = new Font("Segoe UI", 9F);
            tbPhoneNumber.Location = new Point(137, 136);
            tbPhoneNumber.Margin = new Padding(3, 4, 3, 4);
            tbPhoneNumber.Name = "tbPhoneNumber";
            tbPhoneNumber.Size = new Size(128, 27);
            tbPhoneNumber.TabIndex = 141;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPhoneNumber.Location = new Point(6, 140);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(120, 20);
            lblPhoneNumber.TabIndex = 140;
            lblPhoneNumber.Text = "Phone Number:";
            // 
            // tbEmail
            // 
            tbEmail.Font = new Font("Segoe UI", 9F);
            tbEmail.Location = new Point(137, 101);
            tbEmail.Margin = new Padding(3, 4, 3, 4);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(128, 27);
            tbEmail.TabIndex = 139;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.Location = new Point(69, 100);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(57, 20);
            lblEmail.TabIndex = 138;
            lblEmail.Text = "E-Mail:";
            // 
            // tbAdress
            // 
            tbAdress.Font = new Font("Segoe UI", 9F);
            tbAdress.Location = new Point(137, 183);
            tbAdress.Margin = new Padding(3, 4, 3, 4);
            tbAdress.Name = "tbAdress";
            tbAdress.Size = new Size(128, 27);
            tbAdress.TabIndex = 137;
            // 
            // lblAdress
            // 
            lblAdress.AutoSize = true;
            lblAdress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAdress.Location = new Point(68, 187);
            lblAdress.Name = "lblAdress";
            lblAdress.Size = new Size(61, 20);
            lblAdress.TabIndex = 136;
            lblAdress.Text = "Adress:";
            // 
            // lblNameEmployeeForm
            // 
            lblNameEmployeeForm.AutoSize = true;
            lblNameEmployeeForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNameEmployeeForm.Location = new Point(72, 64);
            lblNameEmployeeForm.Name = "lblNameEmployeeForm";
            lblNameEmployeeForm.Size = new Size(55, 20);
            lblNameEmployeeForm.TabIndex = 135;
            lblNameEmployeeForm.Text = "Name:";
            // 
            // tbNameEmployeeForm
            // 
            tbNameEmployeeForm.Font = new Font("Segoe UI", 9F);
            tbNameEmployeeForm.Location = new Point(137, 60);
            tbNameEmployeeForm.Margin = new Padding(3, 4, 3, 4);
            tbNameEmployeeForm.Name = "tbNameEmployeeForm";
            tbNameEmployeeForm.Size = new Size(128, 27);
            tbNameEmployeeForm.TabIndex = 134;
            // 
            // PanelContract
            // 
            PanelContract.BackColor = Color.AntiqueWhite;
            PanelContract.BorderStyle = BorderStyle.FixedSingle;
            PanelContract.Controls.Add(tbContractNotes);
            PanelContract.Controls.Add(lblContractNotes);
            PanelContract.Controls.Add(chkDayShifts);
            PanelContract.Controls.Add(chkNightShifts);
            PanelContract.Controls.Add(lblContractDetails);
            PanelContract.Controls.Add(pbEmployeeForm);
            PanelContract.Controls.Add(clbWorkDays);
            PanelContract.Controls.Add(lblContractType);
            PanelContract.Controls.Add(cbContractType);
            PanelContract.Controls.Add(tbUnpaidLeaveDays);
            PanelContract.Controls.Add(lblUnpaidLeaveDays);
            PanelContract.Controls.Add(lblPaidLeaveDays);
            PanelContract.Controls.Add(tbPaidLeaveDays);
            PanelContract.Controls.Add(lblWorkDays);
            PanelContract.Controls.Add(chkOvertime);
            PanelContract.Controls.Add(chkChangeShifts);
            PanelContract.Controls.Add(lblHoursPerWeek);
            PanelContract.Controls.Add(tbHoursPerWeek);
            PanelContract.Controls.Add(tbBankNumber);
            PanelContract.Controls.Add(lblBankNumber);
            PanelContract.Controls.Add(tbBSN);
            PanelContract.Controls.Add(lblBSN);
            PanelContract.Controls.Add(lblRole);
            PanelContract.Controls.Add(cbRole);
            PanelContract.Controls.Add(tbSalary);
            PanelContract.Controls.Add(lblSalary);
            PanelContract.Controls.Add(dtpEndDate);
            PanelContract.Controls.Add(dtpStartDate);
            PanelContract.Controls.Add(lblEndDate);
            PanelContract.Controls.Add(lblStartDate);
            PanelContract.Location = new Point(437, 72);
            PanelContract.Name = "PanelContract";
            PanelContract.Size = new Size(662, 488);
            PanelContract.TabIndex = 145;
            // 
            // tbContractNotes
            // 
            tbContractNotes.Font = new Font("Segoe UI", 9F);
            tbContractNotes.Location = new Point(438, 392);
            tbContractNotes.Margin = new Padding(3, 4, 3, 4);
            tbContractNotes.Multiline = true;
            tbContractNotes.Name = "tbContractNotes";
            tbContractNotes.Size = new Size(207, 78);
            tbContractNotes.TabIndex = 174;
            // 
            // lblContractNotes
            // 
            lblContractNotes.AutoSize = true;
            lblContractNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContractNotes.Location = new Point(372, 396);
            lblContractNotes.Name = "lblContractNotes";
            lblContractNotes.Size = new Size(55, 20);
            lblContractNotes.TabIndex = 173;
            lblContractNotes.Text = "Notes:";
            // 
            // chkDayShifts
            // 
            chkDayShifts.AutoSize = true;
            chkDayShifts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkDayShifts.Location = new Point(255, 321);
            chkDayShifts.Margin = new Padding(3, 4, 3, 4);
            chkDayShifts.Name = "chkDayShifts";
            chkDayShifts.Size = new Size(197, 24);
            chkDayShifts.TabIndex = 172;
            chkDayShifts.Text = "Available For Day Shifts";
            chkDayShifts.UseVisualStyleBackColor = true;
            // 
            // chkNightShifts
            // 
            chkNightShifts.AutoSize = true;
            chkNightShifts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkNightShifts.Location = new Point(255, 353);
            chkNightShifts.Margin = new Padding(3, 4, 3, 4);
            chkNightShifts.Name = "chkNightShifts";
            chkNightShifts.Size = new Size(210, 24);
            chkNightShifts.TabIndex = 171;
            chkNightShifts.Text = "Available For Night Shifts";
            chkNightShifts.UseVisualStyleBackColor = true;
            // 
            // lblContractDetails
            // 
            lblContractDetails.AutoSize = true;
            lblContractDetails.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContractDetails.Location = new Point(247, 31);
            lblContractDetails.Name = "lblContractDetails";
            lblContractDetails.Size = new Size(148, 25);
            lblContractDetails.TabIndex = 147;
            lblContractDetails.Text = "Contract Details";
            // 
            // pbEmployeeForm
            // 
            pbEmployeeForm.Image = Properties.Resources.DocumentContract;
            pbEmployeeForm.Location = new Point(538, -1);
            pbEmployeeForm.Name = "pbEmployeeForm";
            pbEmployeeForm.Size = new Size(123, 88);
            pbEmployeeForm.SizeMode = PictureBoxSizeMode.Zoom;
            pbEmployeeForm.TabIndex = 170;
            pbEmployeeForm.TabStop = false;
            // 
            // clbWorkDays
            // 
            clbWorkDays.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clbWorkDays.FormattingEnabled = true;
            clbWorkDays.Location = new Point(127, 248);
            clbWorkDays.Margin = new Padding(3, 4, 3, 4);
            clbWorkDays.Name = "clbWorkDays";
            clbWorkDays.Size = new Size(154, 70);
            clbWorkDays.TabIndex = 169;
            // 
            // lblContractType
            // 
            lblContractType.AutoSize = true;
            lblContractType.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContractType.Location = new Point(8, 164);
            lblContractType.Name = "lblContractType";
            lblContractType.Size = new Size(110, 20);
            lblContractType.TabIndex = 168;
            lblContractType.Text = "Contract Type:";
            // 
            // cbContractType
            // 
            cbContractType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbContractType.Font = new Font("Segoe UI", 9F);
            cbContractType.FormattingEnabled = true;
            cbContractType.Location = new Point(126, 163);
            cbContractType.Margin = new Padding(3, 4, 3, 4);
            cbContractType.Name = "cbContractType";
            cbContractType.Size = new Size(155, 28);
            cbContractType.TabIndex = 167;
            // 
            // tbUnpaidLeaveDays
            // 
            tbUnpaidLeaveDays.Font = new Font("Segoe UI", 9F);
            tbUnpaidLeaveDays.Location = new Point(164, 449);
            tbUnpaidLeaveDays.Margin = new Padding(3, 4, 3, 4);
            tbUnpaidLeaveDays.Name = "tbUnpaidLeaveDays";
            tbUnpaidLeaveDays.Size = new Size(154, 27);
            tbUnpaidLeaveDays.TabIndex = 166;
            // 
            // lblUnpaidLeaveDays
            // 
            lblUnpaidLeaveDays.AutoSize = true;
            lblUnpaidLeaveDays.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUnpaidLeaveDays.Location = new Point(13, 452);
            lblUnpaidLeaveDays.Name = "lblUnpaidLeaveDays";
            lblUnpaidLeaveDays.Size = new Size(145, 20);
            lblUnpaidLeaveDays.TabIndex = 165;
            lblUnpaidLeaveDays.Text = "Unpaid Leave Days:";
            // 
            // lblPaidLeaveDays
            // 
            lblPaidLeaveDays.AutoSize = true;
            lblPaidLeaveDays.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaidLeaveDays.Location = new Point(33, 417);
            lblPaidLeaveDays.Name = "lblPaidLeaveDays";
            lblPaidLeaveDays.Size = new Size(125, 20);
            lblPaidLeaveDays.TabIndex = 164;
            lblPaidLeaveDays.Text = "Paid Leave Days:";
            // 
            // tbPaidLeaveDays
            // 
            tbPaidLeaveDays.Font = new Font("Segoe UI", 9F);
            tbPaidLeaveDays.Location = new Point(164, 414);
            tbPaidLeaveDays.Margin = new Padding(3, 4, 3, 4);
            tbPaidLeaveDays.Name = "tbPaidLeaveDays";
            tbPaidLeaveDays.Size = new Size(154, 27);
            tbPaidLeaveDays.TabIndex = 163;
            // 
            // lblWorkDays
            // 
            lblWorkDays.AutoSize = true;
            lblWorkDays.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblWorkDays.Location = new Point(27, 247);
            lblWorkDays.Name = "lblWorkDays";
            lblWorkDays.Size = new Size(89, 20);
            lblWorkDays.TabIndex = 160;
            lblWorkDays.Text = "Work Days:";
            // 
            // chkOvertime
            // 
            chkOvertime.AutoSize = true;
            chkOvertime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkOvertime.Location = new Point(13, 353);
            chkOvertime.Margin = new Padding(3, 4, 3, 4);
            chkOvertime.Name = "chkOvertime";
            chkOvertime.Size = new Size(191, 24);
            chkOvertime.TabIndex = 159;
            chkOvertime.Text = "Available For Overtime";
            chkOvertime.UseVisualStyleBackColor = true;
            // 
            // chkChangeShifts
            // 
            chkChangeShifts.AutoSize = true;
            chkChangeShifts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkChangeShifts.Location = new Point(13, 321);
            chkChangeShifts.Margin = new Padding(3, 4, 3, 4);
            chkChangeShifts.Name = "chkChangeShifts";
            chkChangeShifts.Size = new Size(236, 24);
            chkChangeShifts.TabIndex = 158;
            chkChangeShifts.Text = "Available For Changing Shifts";
            chkChangeShifts.UseVisualStyleBackColor = true;
            // 
            // lblHoursPerWeek
            // 
            lblHoursPerWeek.AutoSize = true;
            lblHoursPerWeek.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHoursPerWeek.Location = new Point(330, 197);
            lblHoursPerWeek.Name = "lblHoursPerWeek";
            lblHoursPerWeek.Size = new Size(55, 20);
            lblHoursPerWeek.TabIndex = 157;
            lblHoursPerWeek.Text = "Hours:";
            // 
            // tbHoursPerWeek
            // 
            tbHoursPerWeek.Font = new Font("Segoe UI", 9F);
            tbHoursPerWeek.Location = new Point(391, 194);
            tbHoursPerWeek.Margin = new Padding(3, 4, 3, 4);
            tbHoursPerWeek.Name = "tbHoursPerWeek";
            tbHoursPerWeek.Size = new Size(128, 27);
            tbHoursPerWeek.TabIndex = 156;
            // 
            // tbBankNumber
            // 
            tbBankNumber.Font = new Font("Segoe UI", 9F);
            tbBankNumber.Location = new Point(391, 229);
            tbBankNumber.Margin = new Padding(3, 4, 3, 4);
            tbBankNumber.Name = "tbBankNumber";
            tbBankNumber.Size = new Size(128, 27);
            tbBankNumber.TabIndex = 155;
            // 
            // lblBankNumber
            // 
            lblBankNumber.AutoSize = true;
            lblBankNumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBankNumber.Location = new Point(281, 232);
            lblBankNumber.Name = "lblBankNumber";
            lblBankNumber.Size = new Size(104, 20);
            lblBankNumber.TabIndex = 154;
            lblBankNumber.Text = "Banknumber:";
            // 
            // tbBSN
            // 
            tbBSN.Font = new Font("Segoe UI", 9F);
            tbBSN.Location = new Point(391, 264);
            tbBSN.Margin = new Padding(3, 4, 3, 4);
            tbBSN.Name = "tbBSN";
            tbBSN.Size = new Size(128, 27);
            tbBSN.TabIndex = 153;
            // 
            // lblBSN
            // 
            lblBSN.AutoSize = true;
            lblBSN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBSN.Location = new Point(342, 267);
            lblBSN.Name = "lblBSN";
            lblBSN.Size = new Size(43, 20);
            lblBSN.TabIndex = 152;
            lblBSN.Text = "BSN:";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.Location = new Point(72, 131);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(44, 20);
            lblRole.TabIndex = 151;
            lblRole.Text = "Role:";
            // 
            // cbRole
            // 
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.Font = new Font("Segoe UI", 9F);
            cbRole.FormattingEnabled = true;
            cbRole.Location = new Point(126, 128);
            cbRole.Margin = new Padding(3, 4, 3, 4);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(155, 28);
            cbRole.TabIndex = 150;
            // 
            // tbSalary
            // 
            tbSalary.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbSalary.Location = new Point(126, 202);
            tbSalary.Margin = new Padding(3, 4, 3, 4);
            tbSalary.Name = "tbSalary";
            tbSalary.Size = new Size(155, 27);
            tbSalary.TabIndex = 149;
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSalary.Location = new Point(61, 209);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(56, 20);
            lblSalary.TabIndex = 148;
            lblSalary.Text = "Salary:";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Font = new Font("Segoe UI", 9F);
            dtpEndDate.Location = new Point(391, 125);
            dtpEndDate.Margin = new Padding(3, 4, 3, 4);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(268, 27);
            dtpEndDate.TabIndex = 147;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Font = new Font("Segoe UI", 9F);
            dtpStartDate.Location = new Point(126, 92);
            dtpStartDate.Margin = new Padding(3, 4, 3, 4);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(268, 27);
            dtpStartDate.TabIndex = 146;
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEndDate.Location = new Point(309, 128);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(76, 20);
            lblEndDate.TabIndex = 145;
            lblEndDate.Text = "End Date:";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStartDate.Location = new Point(32, 97);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(84, 20);
            lblStartDate.TabIndex = 144;
            lblStartDate.Text = "Start Date:";
            // 
            // ssAddEmployeeForm
            // 
            ssAddEmployeeForm.BackColor = Color.DarkSeaGreen;
            ssAddEmployeeForm.ImageScalingSize = new Size(24, 24);
            ssAddEmployeeForm.Items.AddRange(new ToolStripItem[] { tsslblAddEmployeeForm });
            ssAddEmployeeForm.Location = new Point(0, 648);
            ssAddEmployeeForm.Name = "ssAddEmployeeForm";
            ssAddEmployeeForm.Size = new Size(1260, 22);
            ssAddEmployeeForm.TabIndex = 148;
            // 
            // tsslblAddEmployeeForm
            // 
            tsslblAddEmployeeForm.ForeColor = Color.Red;
            tsslblAddEmployeeForm.Name = "tsslblAddEmployeeForm";
            tsslblAddEmployeeForm.Size = new Size(0, 16);
            // 
            // AddEmployeeForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1260, 670);
            Controls.Add(ssAddEmployeeForm);
            Controls.Add(PanelEmployee);
            Controls.Add(btnAddEmployeeForm);
            Controls.Add(btnCancelEmployeeForm);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1278, 717);
            MinimumSize = new Size(1278, 717);
            Name = "AddEmployeeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddEmployeeForm";
            FormClosed += AddEmployeeForm_FormClosed;
            PanelEmployee.ResumeLayout(false);
            PanelEmployee.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            PanelContract.ResumeLayout(false);
            PanelContract.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbEmployeeForm).EndInit();
            ssAddEmployeeForm.ResumeLayout(false);
            ssAddEmployeeForm.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddEmployeeForm;
        private Button btnCancelEmployeeForm;
        private Panel PanelEmployee;
        private Panel PanelContract;
        private CheckedListBox clbWorkDays;
        private Label lblContractType;
        private ComboBox cbContractType;
        private TextBox tbUnpaidLeaveDays;
        private Label lblUnpaidLeaveDays;
        private Label lblPaidLeaveDays;
        private TextBox tbPaidLeaveDays;
        private Label lblWorkDays;
        private CheckBox chkOvertime;
        private CheckBox chkChangeShifts;
        private Label lblHoursPerWeek;
        private TextBox tbHoursPerWeek;
        private TextBox tbBankNumber;
        private Label lblBankNumber;
        private TextBox tbBSN;
        private Label lblBSN;
        private Label lblRole;
        private ComboBox cbRole;
        private TextBox tbSalary;
        private Label lblSalary;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
        private Label lblEndDate;
        private Label lblStartDate;
        private Label lblEmployeeDetails;
        private Label lblContractDetails;
        private PictureBox pbEmployeeForm;
        private StatusStrip ssAddEmployeeForm;
        private ToolStripStatusLabel tsslblAddEmployeeForm;
        private Panel panel1;
        private TextBox tbNotesEmployeeForm;
        private Label lblNotesEmployeeForm;
        private DateTimePicker dtpDateOfBirth;
        private Label lblDateOfBirth;
        private TextBox tbPasswordEmployeeForm;
        private Label lblPasswordEmployeeForm;
        private TextBox tbUsernameEmployeeForm;
        private Label lblUsernameEmployeeForm;
        private TextBox tbPhoneNumber;
        private Label lblPhoneNumber;
        private TextBox tbEmail;
        private Label lblEmail;
        private TextBox tbAdress;
        private Label lblAdress;
        private Label lblNameEmployeeForm;
        private TextBox tbNameEmployeeForm;
        private Label lblEmployeeManager;
        private PictureBox pictureBox5;
        private CheckBox chkDayShifts;
        private CheckBox chkNightShifts;
        private TextBox tbContractNotes;
        private Label lblContractNotes;
    }
}