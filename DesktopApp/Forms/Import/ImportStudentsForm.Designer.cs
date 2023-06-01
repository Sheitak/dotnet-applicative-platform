namespace DesktopApp.Forms
{
    partial class ImportStudentsForm
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
            CSVImportButton = new Button();
            ImportLabel = new Label();
            SuspendLayout();
            // 
            // CSVImportButton
            // 
            CSVImportButton.Location = new Point(335, 207);
            CSVImportButton.Name = "CSVImportButton";
            CSVImportButton.Size = new Size(107, 23);
            CSVImportButton.TabIndex = 0;
            CSVImportButton.Text = "CSV";
            CSVImportButton.UseVisualStyleBackColor = true;
            CSVImportButton.Click += CSVImportButton_Click;
            // 
            // ImportLabel
            // 
            ImportLabel.AutoSize = true;
            ImportLabel.Location = new Point(273, 90);
            ImportLabel.Name = "ImportLabel";
            ImportLabel.Size = new Size(246, 15);
            ImportLabel.TabIndex = 1;
            ImportLabel.Text = "Veuillez sélectionner un format pour importer";
            // 
            // ImportStudentsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ImportLabel);
            Controls.Add(CSVImportButton);
            Name = "ImportStudentsForm";
            Text = "Importer des étudiants";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CSVImportButton;
        private Label ImportLabel;
    }
}