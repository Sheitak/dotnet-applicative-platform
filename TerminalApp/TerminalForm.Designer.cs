namespace TerminalApp
{
    partial class TerminalForm
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
            welcomePanel = new Panel();
            dateLabel = new Label();
            welcomeLabel = new Label();
            signaturePanel = new Panel();
            scanBtn = new Button();
            welcomePanel.SuspendLayout();
            signaturePanel.SuspendLayout();
            SuspendLayout();
            // 
            // welcomePanel
            // 
            welcomePanel.Controls.Add(dateLabel);
            welcomePanel.Controls.Add(welcomeLabel);
            welcomePanel.Location = new Point(74, 25);
            welcomePanel.Name = "welcomePanel";
            welcomePanel.Size = new Size(650, 152);
            welcomePanel.TabIndex = 0;
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dateLabel.Location = new Point(298, 86);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(49, 21);
            dateLabel.TabIndex = 1;
            dateLabel.Text = "Echec";
            // 
            // welcomeLabel
            // 
            welcomeLabel.AutoSize = true;
            welcomeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            welcomeLabel.Location = new Point(125, 19);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Size = new Size(384, 21);
            welcomeLabel.TabIndex = 0;
            welcomeLabel.Text = "Bienvenue ! Vous pouvez commencer à émarger :";
            // 
            // signaturePanel
            // 
            signaturePanel.Controls.Add(scanBtn);
            signaturePanel.Location = new Point(74, 248);
            signaturePanel.Name = "signaturePanel";
            signaturePanel.Size = new Size(650, 141);
            signaturePanel.TabIndex = 1;
            // 
            // scanBtn
            // 
            scanBtn.Location = new Point(255, 82);
            scanBtn.Name = "scanBtn";
            scanBtn.Size = new Size(135, 23);
            scanBtn.TabIndex = 1;
            scanBtn.Text = "Scanner";
            scanBtn.UseVisualStyleBackColor = true;
            scanBtn.Click += scanBtn_Click;
            // 
            // TerminalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(signaturePanel);
            Controls.Add(welcomePanel);
            Name = "TerminalForm";
            Text = "Terminal";
            welcomePanel.ResumeLayout(false);
            welcomePanel.PerformLayout();
            signaturePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel welcomePanel;
        private Label welcomeLabel;
        private Label dateLabel;
        private Panel signaturePanel;
        private Button scanBtn;
    }
}