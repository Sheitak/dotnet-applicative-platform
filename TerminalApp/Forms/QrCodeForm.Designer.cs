namespace TerminalApp.Forms
{
    partial class QrCodeForm
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
            QrCodePanel = new Panel();
            QrCodeBtn = new Button();
            SuspendLayout();
            // 
            // QrCodePanel
            // 
            QrCodePanel.Location = new Point(202, 21);
            QrCodePanel.Name = "QrCodePanel";
            QrCodePanel.Size = new Size(372, 341);
            QrCodePanel.TabIndex = 0;
            // 
            // QrCodeBtn
            // 
            QrCodeBtn.Location = new Point(320, 396);
            QrCodeBtn.Name = "QrCodeBtn";
            QrCodeBtn.Size = new Size(137, 23);
            QrCodeBtn.TabIndex = 1;
            QrCodeBtn.Text = "OK";
            QrCodeBtn.UseVisualStyleBackColor = true;
            QrCodeBtn.Click += QrCodeBtn_Click;
            // 
            // QrCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(QrCodeBtn);
            Controls.Add(QrCodePanel);
            Name = "QrCodeForm";
            Text = "Générateur QrCode";
            ResumeLayout(false);
        }

        #endregion

        private Panel QrCodePanel;
        private Button QrCodeBtn;
    }
}