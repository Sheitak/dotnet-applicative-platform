namespace DesktopApp
{
    partial class FormInterface
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
            CentralFlowLayoutPanel = new FlowLayoutPanel();
            menuStrip1 = new MenuStrip();
            StudentsTSMI = new ToolStripMenuItem();
            CreateStudent = new ToolStripMenuItem();
            LoadStudents = new ToolStripMenuItem();
            ImportStudents = new ToolStripMenuItem();
            GroupsTSMI = new ToolStripMenuItem();
            CreateGroup = new ToolStripMenuItem();
            LoadGroups = new ToolStripMenuItem();
            ImportGroups = new ToolStripMenuItem();
            PromotionsTSMI = new ToolStripMenuItem();
            CreatePromotion = new ToolStripMenuItem();
            LoadPromotions = new ToolStripMenuItem();
            ImportPromotions = new ToolStripMenuItem();
            ListFlowLayoutPanel = new FlowLayoutPanel();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // CentralFlowLayoutPanel
            // 
            CentralFlowLayoutPanel.Location = new Point(161, 27);
            CentralFlowLayoutPanel.Name = "CentralFlowLayoutPanel";
            CentralFlowLayoutPanel.Size = new Size(627, 411);
            CentralFlowLayoutPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { StudentsTSMI, GroupsTSMI, PromotionsTSMI });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // StudentsTSMI
            // 
            StudentsTSMI.DropDownItems.AddRange(new ToolStripItem[] { CreateStudent, LoadStudents, ImportStudents });
            StudentsTSMI.Name = "StudentsTSMI";
            StudentsTSMI.Size = new Size(68, 20);
            StudentsTSMI.Text = "Etudiants";
            // 
            // CreateStudent
            // 
            CreateStudent.Name = "CreateStudent";
            CreateStudent.Size = new Size(180, 22);
            CreateStudent.Text = "Créer";
            CreateStudent.Click += CreateStudent_Click;
            // 
            // LoadStudents
            // 
            LoadStudents.Name = "LoadStudents";
            LoadStudents.Size = new Size(180, 22);
            LoadStudents.Text = "Charger";
            LoadStudents.Click += LoadStudents_Click;
            // 
            // ImportStudents
            // 
            ImportStudents.Name = "ImportStudents";
            ImportStudents.Size = new Size(180, 22);
            ImportStudents.Text = "Importer";
            ImportStudents.Click += ImportStudents_Click;
            // 
            // GroupsTSMI
            // 
            GroupsTSMI.DropDownItems.AddRange(new ToolStripItem[] { CreateGroup, LoadGroups, ImportGroups });
            GroupsTSMI.Name = "GroupsTSMI";
            GroupsTSMI.Size = new Size(63, 20);
            GroupsTSMI.Text = "Groupes";
            // 
            // CreateGroup
            // 
            CreateGroup.Name = "CreateGroup";
            CreateGroup.Size = new Size(120, 22);
            CreateGroup.Text = "Créer";
            // 
            // LoadGroups
            // 
            LoadGroups.Name = "LoadGroups";
            LoadGroups.Size = new Size(120, 22);
            LoadGroups.Text = "Charger";
            // 
            // ImportGroups
            // 
            ImportGroups.Name = "ImportGroups";
            ImportGroups.Size = new Size(120, 22);
            ImportGroups.Text = "Importer";
            // 
            // PromotionsTSMI
            // 
            PromotionsTSMI.DropDownItems.AddRange(new ToolStripItem[] { CreatePromotion, LoadPromotions, ImportPromotions });
            PromotionsTSMI.Name = "PromotionsTSMI";
            PromotionsTSMI.Size = new Size(81, 20);
            PromotionsTSMI.Text = "Promotions";
            // 
            // CreatePromotion
            // 
            CreatePromotion.Name = "CreatePromotion";
            CreatePromotion.Size = new Size(120, 22);
            CreatePromotion.Text = "Créer";
            // 
            // LoadPromotions
            // 
            LoadPromotions.Name = "LoadPromotions";
            LoadPromotions.Size = new Size(120, 22);
            LoadPromotions.Text = "Charger";
            // 
            // ImportPromotions
            // 
            ImportPromotions.Name = "ImportPromotions";
            ImportPromotions.Size = new Size(120, 22);
            ImportPromotions.Text = "Importer";
            // 
            // ListFlowLayoutPanel
            // 
            ListFlowLayoutPanel.Location = new Point(12, 27);
            ListFlowLayoutPanel.Name = "ListFlowLayoutPanel";
            ListFlowLayoutPanel.Size = new Size(143, 411);
            ListFlowLayoutPanel.TabIndex = 2;
            // 
            // FormInterface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ListFlowLayoutPanel);
            Controls.Add(CentralFlowLayoutPanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormInterface";
            Text = "3iL Signatures Administration";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel CentralFlowLayoutPanel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem StudentsTSMI;
        private ToolStripMenuItem CreateStudent;
        private ToolStripMenuItem LoadStudents;
        private ToolStripMenuItem ImportStudents;
        private ToolStripMenuItem GroupsTSMI;
        private ToolStripMenuItem CreateGroup;
        private ToolStripMenuItem LoadGroups;
        private ToolStripMenuItem PromotionsTSMI;
        private ToolStripMenuItem CreatePromotion;
        private ToolStripMenuItem LoadPromotions;
        private FlowLayoutPanel ListFlowLayoutPanel;
        private ToolStripMenuItem ImportGroups;
        private ToolStripMenuItem ImportPromotions;
    }
}