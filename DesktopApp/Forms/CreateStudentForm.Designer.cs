namespace DesktopApp.Forms
{
    partial class CreateStudentForm
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
            firstnameField = new TextBox();
            lastnameField = new TextBox();
            comboBoxGroup = new ComboBox();
            comboBoxPromotion = new ComboBox();
            firstnameLBL = new Label();
            lastnameLBL = new Label();
            promotionLBL = new Label();
            groupLBL = new Label();
            submitCreateStudent = new Button();
            SuspendLayout();
            // 
            // firstnameField
            // 
            firstnameField.Location = new Point(195, 125);
            firstnameField.Name = "firstnameField";
            firstnameField.Size = new Size(156, 23);
            firstnameField.TabIndex = 1;
            firstnameField.TextChanged += firstnameField_TextChanged;
            // 
            // lastnameField
            // 
            lastnameField.Location = new Point(396, 125);
            lastnameField.Name = "lastnameField";
            lastnameField.Size = new Size(156, 23);
            lastnameField.TabIndex = 2;
            lastnameField.TextChanged += lastnameField_TextChanged;
            // 
            // comboBoxGroup
            // 
            comboBoxGroup.DisplayMember = "Name";
            comboBoxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGroup.FormattingEnabled = true;
            comboBoxGroup.Location = new Point(195, 210);
            comboBoxGroup.Name = "comboBoxGroup";
            comboBoxGroup.Size = new Size(156, 23);
            comboBoxGroup.TabIndex = 4;
            comboBoxGroup.ValueMember = "GroupID";
            comboBoxGroup.SelectedValueChanged += comboBoxGroup_SelectedValueChanged;
            // 
            // comboBoxPromotion
            // 
            comboBoxPromotion.DisplayMember = "Name";
            comboBoxPromotion.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPromotion.FormattingEnabled = true;
            comboBoxPromotion.Location = new Point(396, 210);
            comboBoxPromotion.Name = "comboBoxPromotion";
            comboBoxPromotion.Size = new Size(156, 23);
            comboBoxPromotion.TabIndex = 5;
            comboBoxPromotion.ValueMember = "PromotionID";
            comboBoxPromotion.SelectedValueChanged += comboBoxPromotion_SelectedValueChanged;
            // 
            // firstnameLBL
            // 
            firstnameLBL.AutoSize = true;
            firstnameLBL.Location = new Point(195, 107);
            firstnameLBL.Name = "firstnameLBL";
            firstnameLBL.Size = new Size(49, 15);
            firstnameLBL.TabIndex = 6;
            firstnameLBL.Text = "Prénom";
            // 
            // lastnameLBL
            // 
            lastnameLBL.AutoSize = true;
            lastnameLBL.Location = new Point(396, 107);
            lastnameLBL.Name = "lastnameLBL";
            lastnameLBL.Size = new Size(34, 15);
            lastnameLBL.TabIndex = 7;
            lastnameLBL.Text = "Nom";
            // 
            // promotionLBL
            // 
            promotionLBL.AutoSize = true;
            promotionLBL.Location = new Point(396, 182);
            promotionLBL.Name = "promotionLBL";
            promotionLBL.Size = new Size(64, 15);
            promotionLBL.TabIndex = 8;
            promotionLBL.Text = "Promotion";
            // 
            // groupLBL
            // 
            groupLBL.AutoSize = true;
            groupLBL.Location = new Point(195, 182);
            groupLBL.Name = "groupLBL";
            groupLBL.Size = new Size(46, 15);
            groupLBL.TabIndex = 9;
            groupLBL.Text = "Groupe";
            // 
            // submitCreateStudent
            // 
            submitCreateStudent.Location = new Point(304, 283);
            submitCreateStudent.Name = "submitCreateStudent";
            submitCreateStudent.Size = new Size(156, 23);
            submitCreateStudent.TabIndex = 10;
            submitCreateStudent.Text = "Créer";
            submitCreateStudent.UseVisualStyleBackColor = true;
            submitCreateStudent.Click += submitCreateStudent_Click;
            // 
            // CreateStudentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(submitCreateStudent);
            Controls.Add(groupLBL);
            Controls.Add(promotionLBL);
            Controls.Add(lastnameLBL);
            Controls.Add(firstnameLBL);
            Controls.Add(comboBoxPromotion);
            Controls.Add(comboBoxGroup);
            Controls.Add(lastnameField);
            Controls.Add(firstnameField);
            Name = "CreateStudentForm";
            Text = "Création d'un Etudiant";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox firstnameField;
        private TextBox lastnameField;
        private ComboBox comboBoxGroup;
        private ComboBox comboBoxPromotion;
        private Label firstnameLBL;
        private Label lastnameLBL;
        private Label promotionLBL;
        private Label groupLBL;
        private Button submitCreateStudent;
    }
}