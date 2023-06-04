namespace DesktopApp.Forms
{
    partial class CreatePromotionForm
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
            CreatePromotionBtn = new Button();
            NameField = new TextBox();
            nameLBL = new Label();
            SuspendLayout();
            // 
            // CreatePromotionBtn
            // 
            CreatePromotionBtn.Location = new Point(310, 256);
            CreatePromotionBtn.Name = "CreatePromotionBtn";
            CreatePromotionBtn.Size = new Size(170, 23);
            CreatePromotionBtn.TabIndex = 5;
            CreatePromotionBtn.Text = "Créer";
            CreatePromotionBtn.UseVisualStyleBackColor = true;
            CreatePromotionBtn.Click += CreatePromotionBtn_Click;
            // 
            // NameField
            // 
            NameField.Location = new Point(310, 156);
            NameField.Name = "NameField";
            NameField.Size = new Size(170, 23);
            NameField.TabIndex = 4;
            NameField.TextChanged += NameField_TextChanged;
            // 
            // nameLBL
            // 
            nameLBL.AutoSize = true;
            nameLBL.Location = new Point(310, 115);
            nameLBL.Name = "nameLBL";
            nameLBL.Size = new Size(170, 15);
            nameLBL.TabIndex = 3;
            nameLBL.Text = "Nom de la nouvelle promotion";
            // 
            // CreatePromotionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CreatePromotionBtn);
            Controls.Add(NameField);
            Controls.Add(nameLBL);
            Name = "CreatePromotionForm";
            Text = "Création d'une promotion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CreatePromotionBtn;
        private TextBox NameField;
        private Label nameLBL;
    }
}