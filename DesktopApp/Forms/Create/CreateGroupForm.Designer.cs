namespace DesktopApp.Forms
{
    partial class CreateGroupForm
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
            nameLBL = new Label();
            nameField = new TextBox();
            SubmitCreateGroup = new Button();
            SuspendLayout();
            // 
            // nameLBL
            // 
            nameLBL.AutoSize = true;
            nameLBL.Location = new Point(316, 105);
            nameLBL.Name = "nameLBL";
            nameLBL.Size = new Size(141, 15);
            nameLBL.TabIndex = 0;
            nameLBL.Text = "Nom du nouveau groupe";
            // 
            // nameField
            // 
            nameField.Location = new Point(316, 146);
            nameField.Name = "nameField";
            nameField.Size = new Size(141, 23);
            nameField.TabIndex = 1;
            nameField.TextChanged += nameField_TextChanged;
            // 
            // SubmitCreateGroup
            // 
            SubmitCreateGroup.Location = new Point(316, 246);
            SubmitCreateGroup.Name = "SubmitCreateGroup";
            SubmitCreateGroup.Size = new Size(141, 23);
            SubmitCreateGroup.TabIndex = 2;
            SubmitCreateGroup.Text = "Créer";
            SubmitCreateGroup.UseVisualStyleBackColor = true;
            SubmitCreateGroup.Click += SubmitCreateGroup_Click;
            // 
            // CreateGroupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SubmitCreateGroup);
            Controls.Add(nameField);
            Controls.Add(nameLBL);
            Name = "CreateGroupForm";
            Text = "Création d'un Groupe";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nameLBL;
        private TextBox nameField;
        private Button SubmitCreateGroup;
    }
}