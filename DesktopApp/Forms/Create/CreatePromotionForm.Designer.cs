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
            SubmitCreatePromotion = new Button();
            nameField = new TextBox();
            nameLBL = new Label();
            SuspendLayout();
            // 
            // SubmitCreatePromotion
            // 
            SubmitCreatePromotion.Location = new Point(310, 256);
            SubmitCreatePromotion.Name = "SubmitCreatePromotion";
            SubmitCreatePromotion.Size = new Size(170, 23);
            SubmitCreatePromotion.TabIndex = 5;
            SubmitCreatePromotion.Text = "Créer";
            SubmitCreatePromotion.UseVisualStyleBackColor = true;
            SubmitCreatePromotion.Click += SubmitCreatePromotion_Click;
            // 
            // nameField
            // 
            nameField.Location = new Point(310, 156);
            nameField.Name = "nameField";
            nameField.Size = new Size(170, 23);
            nameField.TabIndex = 4;
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
            Controls.Add(SubmitCreatePromotion);
            Controls.Add(nameField);
            Controls.Add(nameLBL);
            Name = "CreatePromotionForm";
            Text = "Création d'une promotion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SubmitCreatePromotion;
        private TextBox nameField;
        private Label nameLBL;
    }
}