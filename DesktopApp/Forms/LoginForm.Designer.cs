namespace DesktopApp.Forms
{
    partial class LoginForm
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
            EmailLabel = new Label();
            EmailField = new TextBox();
            LoginBtn = new Button();
            PasswordLabel = new Label();
            PasswordField = new TextBox();
            logoPB = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)logoPB).BeginInit();
            SuspendLayout();
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Location = new Point(303, 175);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(36, 15);
            EmailLabel.TabIndex = 0;
            EmailLabel.Text = "Email";
            // 
            // EmailField
            // 
            EmailField.Location = new Point(303, 193);
            EmailField.Name = "EmailField";
            EmailField.Size = new Size(151, 23);
            EmailField.TabIndex = 1;
            // 
            // LoginBtn
            // 
            LoginBtn.Location = new Point(327, 346);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(100, 23);
            LoginBtn.TabIndex = 2;
            LoginBtn.Text = "Connexion";
            LoginBtn.UseVisualStyleBackColor = true;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(303, 260);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(57, 15);
            PasswordLabel.TabIndex = 3;
            PasswordLabel.Text = "Password";
            // 
            // PasswordField
            // 
            PasswordField.Location = new Point(303, 289);
            PasswordField.Name = "PasswordField";
            PasswordField.Size = new Size(151, 23);
            PasswordField.TabIndex = 4;
            // 
            // logoPB
            // 
            logoPB.Image = Properties.Resources._3il_group;
            logoPB.Location = new Point(256, 68);
            logoPB.Name = "logoPB";
            logoPB.Size = new Size(249, 66);
            logoPB.TabIndex = 5;
            logoPB.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(logoPB);
            Controls.Add(PasswordField);
            Controls.Add(PasswordLabel);
            Controls.Add(LoginBtn);
            Controls.Add(EmailField);
            Controls.Add(EmailLabel);
            Name = "LoginForm";
            Text = "Connexion à l'administration";
            ((System.ComponentModel.ISupportInitialize)logoPB).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label EmailLabel;
        private TextBox EmailField;
        private Button LoginBtn;
        private Label PasswordLabel;
        private TextBox PasswordField;
        private PictureBox logoPB;
    }
}