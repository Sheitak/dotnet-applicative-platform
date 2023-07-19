using DesktopApp.Forms;
using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp
{
    public partial class FormInterface : Form
    {
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        Student student = new();
        Group group = new();
        Promotion promotion = new();

        public FormInterface(User user)
        {
            InitializeComponent();
            CenterToScreen();
            Text = $"3iL Signatures Administration - Bienvenue {user.UserName}";
        }

        public delegate void LoadEntitiesDelegate();

        private async void LoadStudents_Click(object sender, EventArgs e)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);
            ClearFlowLayoutPanel(ListFlowLayoutPanel);

            var studentList = await dataSourceProvider.GetStudents();

            // PANEL GAUCHE
            ListView studentListView = new ListView()
            {
                Name = "StudentListView",
                Bounds = new Rectangle(new Point(10, 10), new Size(125, 400)),

                View = View.Details
            };
            studentListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            studentListView.Columns.Add("Elèves", -2, HorizontalAlignment.Left);
            studentListView.FullRowSelect = true;
            studentListView.GridLines = true;

            studentListView.Columns[0].Width = studentListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Student student in studentList)
            {
                ListViewItem item = new ListViewItem(student.Firstname + ' ' + student.Lastname)
                {
                    Tag = student.StudentID
                };
                studentListView.Items.Add(item);
            }

            studentListView.Focus();
            studentListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(studentListView);
        }

        private async void LoadGroups_Click(object sender, EventArgs e)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);
            ClearFlowLayoutPanel(ListFlowLayoutPanel);

            var groupList = await dataSourceProvider.GetGroups();

            // PANEL GAUCHE
            ListView groupListView = new ListView
            {
                Name = "GroupListView",
                Bounds = new Rectangle(new Point(10, 10), new Size(125, 400)),

                View = View.Details
            };
            groupListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            groupListView.Columns.Add("Groupes", -2, HorizontalAlignment.Left);
            groupListView.FullRowSelect = true;
            groupListView.GridLines = true;

            groupListView.Columns[0].Width = groupListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Group group in groupList)
            {
                ListViewItem item = new ListViewItem(group.Name)
                {
                    Tag = group.GroupID
                };
                groupListView.Items.Add(item);
            }

            groupListView.Focus();
            groupListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(groupListView);
        }

        private async void LoadPromotions_Click(object sender, EventArgs e)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);
            ClearFlowLayoutPanel(ListFlowLayoutPanel);

            var promotionList = await dataSourceProvider.GetPromotions();

            // PANEL GAUCHE
            ListView promotionListView = new ListView
            {
                Name = "PromotionListView",
                Bounds = new Rectangle(new Point(10, 10), new Size(125, 400)),

                View = View.Details
            };
            promotionListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            promotionListView.Columns.Add("Promotions", -2, HorizontalAlignment.Left);
            promotionListView.FullRowSelect = true;
            promotionListView.GridLines = true;

            promotionListView.Columns[0].Width = promotionListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Promotion promotion in promotionList)
            {
                ListViewItem item = new ListViewItem(promotion.Name)
                {
                    Tag = promotion.PromotionID
                };
                promotionListView.Items.Add(item);
            }

            promotionListView.Focus();
            promotionListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(promotionListView);
        }

        private void TextField_Changed(object? sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            switch (textBox.Name)
            {
                case "FirstnameField":
                    student.Firstname = textBox.Text;
                    break;
                case "LastnameField":
                    student.Lastname = textBox.Text;
                    break;
                case "GroupNameField":
                    group.Name = textBox.Text;
                    break;
                case "PromotionNameField":
                    promotion.Name = textBox.Text;
                    break;
            }
        }

        private void ComboBoxField_Changed(object? sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            switch (comboBox.Name)
            {
                case "GroupField":
                    student.GroupID = (int)comboBox.SelectedValue;
                    student.Group = new Group
                    {
                        GroupID = (int)comboBox.SelectedValue,
                        Name = comboBox.Text
                    };
                    break;
                case "PromotionField":
                    student.PromotionID = (int)comboBox.SelectedValue;
                    student.Promotion = new Promotion
                    {
                        PromotionID = (int)comboBox.SelectedValue,
                        Name = comboBox.Text
                    };
                    break;
            }
        }

        private async void EditBtn_Click(object? sender, EventArgs e)
        {
            Button btnEdit = sender as Button;
            try
            {
                switch (btnEdit.Name)
                {
                    case "editStudentButton":
                        await dataSourceProvider.UpdateStudent(student);
                        MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été modifié avec succès !");
                        LoadStudents_Click(sender, e);
                        break;
                    case "editGroupButton":
                        await dataSourceProvider.UpdateGroup(group);
                        MessageBox.Show("Le groupe " + group.Name + " a été modifié avec succès !");
                        LoadGroups_Click(sender, e);
                        break;
                    case "editPromotionButton":
                        await dataSourceProvider.UpdatePromotion(promotion);
                        MessageBox.Show("La promotion " + promotion.Name + " a été modifiée avec succès !");
                        LoadPromotions_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteBtn_Click(object? sender, EventArgs e)
        {
            Button btnDelete = sender as Button;
            try
            {
                switch (btnDelete.Name)
                {
                    case "deleteStudentButton":
                        await dataSourceProvider.DeleteStudent(student.StudentID);
                        MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été supprimé avec succès !");
                        LoadStudents_Click(sender, e);
                        break;
                    case "deleteGroupButton":
                        await dataSourceProvider.DeleteGroup(group.GroupID);
                        MessageBox.Show("Le groupe " + group.Name + " a été supprimé avec succès !");
                        LoadGroups_Click(sender, e);
                        break;
                    case "deletePromotionButton":
                        await dataSourceProvider.DeletePromotion(promotion.PromotionID);
                        MessageBox.Show("La promotion " + promotion.Name + " a été supprimé avec succès !");
                        LoadPromotions_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListView_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView.SelectedItems[0];
                switch (listView.Name)
                {
                    case "StudentListView":
                        LoadCentralStudentPanel((int)selectedItem.Tag);
                        break;
                    case "GroupListView":
                        LoadCentralGroupPanel((int)selectedItem.Tag);
                        break;
                    case "PromotionListView":
                        LoadCentralPromotionPanel((int)selectedItem.Tag);
                        break;
                }
            }
        }

        private async void LoadCentralStudentPanel(int studentId)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);

            student = await dataSourceProvider.GetStudentById(studentId);

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;

            // ID
            /*
            Label studentIDLabel = new Label();
            TextBox studentIDField = new TextBox();

            studentIDLabel.Text = "ID : ";
            studentIDLabel.Name = "IDField";
            studentIDField.Text = student.StudentID.ToString();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(studentIDLabel, 0, 0);
            tableLayoutPanel.Controls.Add(studentIDField, 1, 0);
            */

            // FIRSTNAME
            Label studentFirstnameLabel = new Label();
            TextBox studentFirstnameField = new TextBox();

            studentFirstnameLabel.Text = "Prénom : ";
            studentFirstnameField.Name = "FirstnameField";
            studentFirstnameField.Text = student.Firstname.ToString();
            studentFirstnameField.TextChanged += new EventHandler(TextField_Changed);

            studentFirstnameLabel.Margin = new Padding(0, 15, 0, 0);
            studentFirstnameField.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(studentFirstnameLabel, 0, 0);
            tableLayoutPanel.Controls.Add(studentFirstnameField, 1, 0);

            // LASTNAME
            Label studentLastnameLabel = new Label();
            TextBox studentLastnameField = new TextBox();

            studentLastnameLabel.Text = "Nom : ";
            studentLastnameField.Name = "LastnameField";
            studentLastnameField.Text = student.Lastname.ToString();
            studentLastnameField.TextChanged += new EventHandler(TextField_Changed);

            studentLastnameLabel.Margin = new Padding(0, 15, 0, 0);
            studentLastnameField.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(studentLastnameLabel, 0, 1);
            tableLayoutPanel.Controls.Add(studentLastnameField, 1, 1);

            // GROUP
            Label groupListLabel = new Label();
            groupListLabel.Text = "Groupe : ";

            ComboBox groupList = new ComboBox();
            groupList.Name = "GroupField";
            groupList.ValueMember = "GroupID";
            groupList.DisplayMember = "Name";
            groupList.DropDownStyle = ComboBoxStyle.DropDownList;

            var groups = await dataSourceProvider.GetGroups();

            groups.Insert(0, new Group { GroupID = student.Group.GroupID, Name = student.Group.Name });
            groupList.DataSource = groups;

            if (student.Group != null)
            {
                groupList.SelectedValue = student.Group.GroupID;
            }
            else
            {
                groupList.SelectedValue = null;
            }

            groupList.SelectedValueChanged += new EventHandler(ComboBoxField_Changed);

            groupListLabel.Margin = new Padding(0, 15, 0, 0);
            groupList.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(groupListLabel, 0, 2);
            tableLayoutPanel.Controls.Add(groupList, 1, 2);

            // PROMOTION
            Label promotionListLabel = new Label();
            promotionListLabel.Text = "Promotion : ";

            ComboBox promotionList = new ComboBox();
            promotionList.Name = "PromotionField";
            promotionList.ValueMember = "PromotionID";
            promotionList.DisplayMember = "Name";
            promotionList.DropDownStyle = ComboBoxStyle.DropDownList;

            var promotions = await dataSourceProvider.GetPromotions();

            promotions.Insert(0, new Promotion { PromotionID = student.Promotion.PromotionID, Name = student.Promotion.Name });
            promotionList.DataSource = promotions;

            if (student.Group != null)
            {
                promotionList.SelectedValue = student.Promotion.PromotionID;
            }
            else
            {
                promotionList.SelectedValue = null;
            }

            promotionList.SelectedValueChanged += new EventHandler(ComboBoxField_Changed);

            promotionListLabel.Margin = new Padding(0, 15, 0, 0);
            promotionList.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(promotionListLabel, 0, 3);
            tableLayoutPanel.Controls.Add(promotionList, 1, 3);

            // EDIT
            Button editBtn = new Button();
            editBtn.Name = "editStudentButton";
            editBtn.Text = "Modifier";
            editBtn.BackColor = Color.DarkSeaGreen;
            editBtn.Click += new EventHandler(EditBtn_Click);

            // DELETE
            Button deleteBtn = new Button();
            deleteBtn.Name = "deleteStudentButton";
            deleteBtn.Text = "Supprimer";
            deleteBtn.BackColor = Color.IndianRed;
            deleteBtn.Click += new EventHandler(DeleteBtn_Click);

            editBtn.Margin = new Padding(0, 15, 0, 0);
            deleteBtn.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(editBtn, 0, 4);
            tableLayoutPanel.Controls.Add(deleteBtn, 1, 4);

            CentralFlowLayoutPanel.Controls.Add(tableLayoutPanel);

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private async void LoadCentralGroupPanel(int groupId)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);

            group = await dataSourceProvider.GetGroupById(groupId);

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;

            // ID
            /*
            Label groupIDLabel = new Label();
            TextBox groupIDField = new TextBox();

            groupIDLabel.Text = "ID : ";
            groupIDLabel.Name = "groupIDField";
            groupIDField.Text = group.GroupID.ToString();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(groupIDLabel, 0, 0);
            tableLayoutPanel.Controls.Add(groupIDField, 1, 0);
            */

            // NAME
            Label groupNameLabel = new Label();
            TextBox groupNameField = new TextBox();

            groupNameLabel.Text = "Nom : ";
            groupNameField.Name = "GroupNameField";
            groupNameField.Text = group.Name.ToString();
            groupNameField.TextChanged += new EventHandler(TextField_Changed);

            groupNameLabel.Margin = new Padding(0, 15, 0, 0);
            groupNameField.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(groupNameLabel, 0, 0);
            tableLayoutPanel.Controls.Add(groupNameField, 1, 0);

            // EDIT
            Button editBtn = new Button();
            editBtn.Name = "editGroupButton";
            editBtn.Text = "Modifier";
            editBtn.BackColor = Color.DarkSeaGreen;
            editBtn.Click += new EventHandler(EditBtn_Click);

            // DELETE
            Button deleteBtn = new Button();
            deleteBtn.Name = "deleteGroupButton";
            deleteBtn.Text = "Supprimer";
            deleteBtn.BackColor = Color.IndianRed;
            deleteBtn.Click += new EventHandler(DeleteBtn_Click);

            editBtn.Margin = new Padding(0, 15, 0, 0);
            deleteBtn.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(editBtn, 0, 1);
            tableLayoutPanel.Controls.Add(deleteBtn, 1, 1);

            CentralFlowLayoutPanel.Controls.Add(tableLayoutPanel);

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private async void LoadCentralPromotionPanel(int promotionId)
        {
            ClearFlowLayoutPanel(CentralFlowLayoutPanel);

            promotion = await dataSourceProvider.GetPromotionById(promotionId);

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;

            // ID
            /*
            Label promotionIDLabel = new Label();
            TextBox promotionIDField = new TextBox();

            promotionIDLabel.Text = "ID : ";
            promotionIDLabel.Name = "promotionIDField";
            promotionIDField.Text = promotion.PromotionID.ToString();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(promotionIDLabel, 0, 0);
            tableLayoutPanel.Controls.Add(promotionIDField, 1, 0);
            */

            // NAME
            Label promotionNameLabel = new Label();
            TextBox promotionNameField = new TextBox();

            promotionNameLabel.Text = "Nom : ";
            promotionNameField.Name = "PromotionNameField";
            promotionNameField.Text = promotion.Name.ToString();
            promotionNameField.TextChanged += new EventHandler(TextField_Changed);

            promotionNameLabel.Margin = new Padding(0, 15, 0, 0);
            promotionNameField.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(promotionNameLabel, 0, 0);
            tableLayoutPanel.Controls.Add(promotionNameField, 1, 0);

            // EDIT
            Button editBtn = new Button();
            editBtn.Name = "editPromotionButton";
            editBtn.Text = "Modifier";
            editBtn.BackColor = Color.DarkSeaGreen;
            editBtn.Click += new EventHandler(EditBtn_Click);

            // DELETE
            Button deleteBtn = new Button();
            deleteBtn.Name = "deletePromotionButton";
            deleteBtn.Text = "Supprimer";
            deleteBtn.BackColor = Color.IndianRed;
            deleteBtn.Click += new EventHandler(DeleteBtn_Click);

            editBtn.Margin = new Padding(0, 15, 0, 0);
            deleteBtn.Margin = new Padding(0, 15, 0, 0);

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Controls.Add(editBtn, 0, 1);
            tableLayoutPanel.Controls.Add(deleteBtn, 1, 1);

            CentralFlowLayoutPanel.Controls.Add(tableLayoutPanel);

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private void ClearFlowLayoutPanel(FlowLayoutPanel panel)
        {
            foreach (Control control in panel.Controls)
            {
                control.Dispose();
            }
            panel.Controls.Clear();
        }

        private void CreateStudent_Click(object sender, EventArgs e)
        {
            var createStudentForm = new CreateStudentForm(ReloadStudents);
            createStudentForm.Show();
        }

        private void CreateGroup_Click(object sender, EventArgs e)
        {
            var createGroupForm = new CreateGroupForm(ReloadGroups);
            createGroupForm.Show();
        }

        private void CreatePromotion_Click(object sender, EventArgs e)
        {
            var createPromotionForm = new CreatePromotionForm(ReloadPromotions);
            createPromotionForm.Show();
        }

        private void ImportStudents_Click(object sender, EventArgs e)
        {
            var importStudentsForm = new ImportStudentsForm();
            importStudentsForm.Show();
        }

        private void ImportGroups_Click(object sender, EventArgs e)
        {
            var importGroupsForm = new ImportGroupsForm();
            importGroupsForm.Show();
        }

        private void ImportPromotions_Click(object sender, EventArgs e)
        {
            var importPromotionsForm = new ImportPromotionsForm();
            importPromotionsForm.Show();
        }

        private void ReloadStudents()
        {
            LoadStudents_Click(this, EventArgs.Empty);
        }

        private void ReloadGroups()
        {
            LoadGroups_Click(this, EventArgs.Empty);
        }

        private void ReloadPromotions()
        {
            LoadPromotions_Click(this, EventArgs.Empty);
        }
    }
}