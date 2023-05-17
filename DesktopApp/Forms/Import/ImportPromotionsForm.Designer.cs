namespace DesktopApp.Forms
{
    partial class ImportPromotionsForm
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
            ImportLabel = new Label();
            csvImportButton = new Button();
            SuspendLayout();
            // 
            // ImportLabel
            // 
            ImportLabel.AutoSize = true;
            ImportLabel.Location = new Point(266, 113);
            ImportLabel.Name = "ImportLabel";
            ImportLabel.Size = new Size(246, 15);
            ImportLabel.TabIndex = 5;
            ImportLabel.Text = "Veuillez sélectionner un format pour importer";
            // 
            // csvImportButton
            // 
            csvImportButton.Location = new Point(328, 230);
            csvImportButton.Name = "csvImportButton";
            csvImportButton.Size = new Size(107, 23);
            csvImportButton.TabIndex = 4;
            csvImportButton.Text = "CSV";
            csvImportButton.UseVisualStyleBackColor = true;
            csvImportButton.Click += csvImportButton_Click;
            // 
            // ImportPromotionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ImportLabel);
            Controls.Add(csvImportButton);
            Name = "ImportPromotionsForm";
            Text = "Importer des promotions";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ImportLabel;
        private Button csvImportButton;
    }
}