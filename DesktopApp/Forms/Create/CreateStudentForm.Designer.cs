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
            FirstnameField = new TextBox();
            LastnameField = new TextBox();
            ComboBoxGroup = new ComboBox();
            ComboBoxPromotion = new ComboBox();
            firstnameLBL = new Label();
            lastnameLBL = new Label();
            promotionLBL = new Label();
            groupLBL = new Label();
            CreateStudentBtn = new Button();
            SuspendLayout();
            // 
            // FirstnameField
            // 
            FirstnameField.Location = new Point(195, 125);
            FirstnameField.Name = "FirstnameField";
            FirstnameField.Size = new Size(156, 23);
            FirstnameField.TabIndex = 1;
            FirstnameField.TextChanged += FirstnameField_TextChanged;
            // 
            // LastnameField
            // 
            LastnameField.Location = new Point(396, 125);
            LastnameField.Name = "LastnameField";
            LastnameField.Size = new Size(156, 23);
            LastnameField.TabIndex = 2;
            LastnameField.TextChanged += LastnameField_TextChanged;
            // 
            // ComboBoxGroup
            // 
            ComboBoxGroup.DisplayMember = "Name";
            ComboBoxGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxGroup.FormattingEnabled = true;
            ComboBoxGroup.Location = new Point(195, 210);
            ComboBoxGroup.Name = "ComboBoxGroup";
            ComboBoxGroup.Size = new Size(156, 23);
            ComboBoxGroup.TabIndex = 4;
            ComboBoxGroup.ValueMember = "GroupID";
            ComboBoxGroup.SelectedValueChanged += ComboBoxGroup_SelectedValueChanged;
            // 
            // ComboBoxPromotion
            // 
            ComboBoxPromotion.DisplayMember = "Name";
            ComboBoxPromotion.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxPromotion.FormattingEnabled = true;
            ComboBoxPromotion.Location = new Point(396, 210);
            ComboBoxPromotion.Name = "ComboBoxPromotion";
            ComboBoxPromotion.Size = new Size(156, 23);
            ComboBoxPromotion.TabIndex = 5;
            ComboBoxPromotion.ValueMember = "PromotionID";
            ComboBoxPromotion.SelectedValueChanged += ComboBoxPromotion_SelectedValueChanged;
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
            // CreateStudentBtn
            // 
            CreateStudentBtn.Location = new Point(304, 283);
            CreateStudentBtn.Name = "CreateStudentBtn";
            CreateStudentBtn.Size = new Size(156, 23);
            CreateStudentBtn.TabIndex = 10;
            CreateStudentBtn.Text = "Créer";
            CreateStudentBtn.UseVisualStyleBackColor = true;
            CreateStudentBtn.Click += CreateStudentBtn_Click;
            // 
            // CreateStudentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CreateStudentBtn);
            Controls.Add(groupLBL);
            Controls.Add(promotionLBL);
            Controls.Add(lastnameLBL);
            Controls.Add(firstnameLBL);
            Controls.Add(ComboBoxPromotion);
            Controls.Add(ComboBoxGroup);
            Controls.Add(LastnameField);
            Controls.Add(FirstnameField);
            Name = "CreateStudentForm";
            Text = "Création d'un Etudiant";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox FirstnameField;
        private TextBox LastnameField;
        private ComboBox ComboBoxGroup;
        private ComboBox ComboBoxPromotion;
        private Label firstnameLBL;
        private Label lastnameLBL;
        private Label promotionLBL;
        private Label groupLBL;
        private Button CreateStudentBtn;
    }
}