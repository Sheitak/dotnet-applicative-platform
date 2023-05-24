namespace TerminalApp.Forms
{
    partial class ScannerForm
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
            scannerPanel = new Panel();
            scanPictureBox = new PictureBox();
            scanBtn = new Button();
            exampleTextBox = new TextBox();
            scannerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scanPictureBox).BeginInit();
            SuspendLayout();
            // 
            // scannerPanel
            // 
            scannerPanel.Controls.Add(exampleTextBox);
            scannerPanel.Controls.Add(scanBtn);
            scannerPanel.Controls.Add(scanPictureBox);
            scannerPanel.Dock = DockStyle.Fill;
            scannerPanel.Location = new Point(0, 0);
            scannerPanel.Name = "scannerPanel";
            scannerPanel.Size = new Size(800, 450);
            scannerPanel.TabIndex = 0;
            // 
            // scanPictureBox
            // 
            scanPictureBox.Location = new Point(227, 28);
            scanPictureBox.Name = "scanPictureBox";
            scanPictureBox.Size = new Size(313, 308);
            scanPictureBox.TabIndex = 0;
            scanPictureBox.TabStop = false;
            // 
            // scanBtn
            // 
            scanBtn.Location = new Point(326, 415);
            scanBtn.Name = "scanBtn";
            scanBtn.Size = new Size(99, 23);
            scanBtn.TabIndex = 1;
            scanBtn.Text = "OK";
            scanBtn.UseVisualStyleBackColor = true;
            scanBtn.Click += scanBtn_Click;
            // 
            // exampleTextBox
            // 
            exampleTextBox.Location = new Point(227, 374);
            exampleTextBox.Name = "exampleTextBox";
            exampleTextBox.Size = new Size(313, 23);
            exampleTextBox.TabIndex = 2;
            // 
            // ScannerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(scannerPanel);
            Name = "ScannerForm";
            Text = "Scanner";
            scannerPanel.ResumeLayout(false);
            scannerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scanPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel scannerPanel;
        private Button scanBtn;
        private PictureBox scanPictureBox;
        private TextBox exampleTextBox;
    }
}