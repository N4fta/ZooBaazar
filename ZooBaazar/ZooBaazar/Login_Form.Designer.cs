namespace ZooBaazar
{
    partial class Login_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbUsernameLoginForm = new TextBox();
            tbPasswordLoginForm = new TextBox();
            lblUsernameLoginForm = new Label();
            lblPasswordLoginForm = new Label();
            btnLogin = new Button();
            pictureBox1 = new PictureBox();
            btnTesting = new Button();
            lblError = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tbUsernameLoginForm
            // 
            tbUsernameLoginForm.BackColor = SystemColors.InactiveBorder;
            tbUsernameLoginForm.Font = new Font("Segoe UI", 16F);
            tbUsernameLoginForm.Location = new Point(446, 295);
            tbUsernameLoginForm.Margin = new Padding(3, 4, 3, 4);
            tbUsernameLoginForm.Name = "tbUsernameLoginForm";
            tbUsernameLoginForm.Size = new Size(422, 43);
            tbUsernameLoginForm.TabIndex = 0;
            tbUsernameLoginForm.Text = "PedroTheAdmin";
            // 
            // tbPasswordLoginForm
            // 
            tbPasswordLoginForm.BackColor = SystemColors.InactiveBorder;
            tbPasswordLoginForm.Font = new Font("Segoe UI", 16F);
            tbPasswordLoginForm.Location = new Point(446, 367);
            tbPasswordLoginForm.Margin = new Padding(3, 4, 3, 4);
            tbPasswordLoginForm.Name = "tbPasswordLoginForm";
            tbPasswordLoginForm.PasswordChar = '*';
            tbPasswordLoginForm.Size = new Size(422, 43);
            tbPasswordLoginForm.TabIndex = 1;
            tbPasswordLoginForm.Text = "test1234";
            // 
            // lblUsernameLoginForm
            // 
            lblUsernameLoginForm.AutoSize = true;
            lblUsernameLoginForm.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsernameLoginForm.Location = new Point(298, 295);
            lblUsernameLoginForm.Name = "lblUsernameLoginForm";
            lblUsernameLoginForm.Size = new Size(142, 38);
            lblUsernameLoginForm.TabIndex = 2;
            lblUsernameLoginForm.Text = "Username";
            // 
            // lblPasswordLoginForm
            // 
            lblPasswordLoginForm.AutoSize = true;
            lblPasswordLoginForm.Font = new Font("Segoe UI", 16F);
            lblPasswordLoginForm.Location = new Point(312, 367);
            lblPasswordLoginForm.Name = "lblPasswordLoginForm";
            lblPasswordLoginForm.Size = new Size(128, 37);
            lblPasswordLoginForm.TabIndex = 3;
            lblPasswordLoginForm.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.AntiqueWhite;
            btnLogin.Font = new Font("Segoe UI", 16F);
            btnLogin.Location = new Point(545, 477);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(153, 57);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ZooBaazar_Logo;
            pictureBox1.Location = new Point(401, 13);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(445, 238);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // btnTesting
            // 
            btnTesting.Location = new Point(12, 12);
            btnTesting.Name = "btnTesting";
            btnTesting.Size = new Size(105, 32);
            btnTesting.TabIndex = 6;
            btnTesting.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 14F);
            lblError.ForeColor = Color.Firebrick;
            lblError.Location = new Point(446, 255);
            lblError.Name = "lblError";
            lblError.Size = new Size(64, 32);
            lblError.TabIndex = 6;
            lblError.Text = "Error";
            // 
            // Login_Form
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSeaGreen;
            ClientSize = new Size(1260, 670);
            Controls.Add(lblError);
            Controls.Add(pictureBox1);
            Controls.Add(btnLogin);
            Controls.Add(lblPasswordLoginForm);
            Controls.Add(lblUsernameLoginForm);
            Controls.Add(tbPasswordLoginForm);
            Controls.Add(tbUsernameLoginForm);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1278, 717);
            MinimumSize = new Size(1278, 717);
            Name = "Login_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUsernameLoginForm;
        private TextBox tbPasswordLoginForm;
        private Label lblUsernameLoginForm;
        private Label lblPasswordLoginForm;
        private Button btnLogin;
        private PictureBox pictureBox1;
        private Button btnTesting;
        private Label lblError;
    }
}
