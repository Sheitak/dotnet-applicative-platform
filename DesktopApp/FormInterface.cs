using DesktopApp.Forms;
using DesktopApp.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Json;

namespace DesktopApp
{
    public partial class FormInterface : Form
    {
        static readonly string urlBase = "https://localhost:7058/api";
        Student student = new Student();
        Group group = new Group();
        Promotion promotion = new Promotion();

        public FormInterface()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void LoadStudents_Click(object sender, EventArgs e)
        {
            CentralFlowLayoutPanel.Controls.Clear();
            ListFlowLayoutPanel.Controls.Clear();

            var studentList = await GetStudents();

            // PANEL GAUCHE
            ListView studentListView = new ListView();
            studentListView.Name = "StudentListView";
            studentListView.Bounds = new Rectangle(new Point(10, 10), new Size(125, 400));

            studentListView.View = View.Details;
            studentListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            studentListView.Columns.Add("Elèves", -2, HorizontalAlignment.Left);
            studentListView.FullRowSelect = true;
            studentListView.GridLines = true;

            studentListView.Columns[0].Width = studentListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Student student in studentList)
            {
                ListViewItem item = new ListViewItem(student.Firstname + ' ' + student.Lastname);
                item.Tag = student.StudentID;
                studentListView.Items.Add(item);
            }

            studentListView.Focus();
            studentListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(studentListView);
        }

        private async void LoadGroups_Click(object sender, EventArgs e)
        {
            CentralFlowLayoutPanel.Controls.Clear();
            ListFlowLayoutPanel.Controls.Clear();

            var groupList = await GetGroups();

            // PANEL GAUCHE
            ListView groupListView = new ListView();
            groupListView.Name = "GroupListView";
            groupListView.Bounds = new Rectangle(new Point(10, 10), new Size(125, 400));

            groupListView.View = View.Details;
            groupListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            groupListView.Columns.Add("Groupes", -2, HorizontalAlignment.Left);
            groupListView.FullRowSelect = true;
            groupListView.GridLines = true;

            groupListView.Columns[0].Width = groupListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Group group in groupList)
            {
                ListViewItem item = new ListViewItem(group.Name);
                item.Tag = group.GroupID;
                groupListView.Items.Add(item);
            }

            groupListView.Focus();
            groupListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(groupListView);
        }

        private async void LoadPromotions_Click(object sender, EventArgs e)
        {
            CentralFlowLayoutPanel.Controls.Clear();
            ListFlowLayoutPanel.Controls.Clear();

            var promotionList = await GetPromotions();

            // PANEL GAUCHE
            ListView promotionListView = new ListView();
            promotionListView.Name = "PromotionListView";
            promotionListView.Bounds = new Rectangle(new Point(10, 10), new Size(125, 400));

            promotionListView.View = View.Details;
            promotionListView.SelectedIndexChanged += ListView_SelectedIndexChanged;

            promotionListView.Columns.Add("Promotions", -2, HorizontalAlignment.Left);
            promotionListView.FullRowSelect = true;
            promotionListView.GridLines = true;

            promotionListView.Columns[0].Width = promotionListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Promotion promotion in promotionList)
            {
                ListViewItem item = new ListViewItem(promotion.Name);
                item.Tag = promotion.PromotionID;
                promotionListView.Items.Add(item);
            }

            promotionListView.Focus();
            promotionListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(promotionListView);
        }

        private void TextField_Changed(object sender, EventArgs e)
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

        private void ComboBoxField_Changed(object sender, EventArgs e)
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

        private async void submitBtn_Click(object sender, EventArgs e)
        {
            Button btnSubmit = sender as Button;
            try
            {
                switch (btnSubmit.Name)
                {
                    case "submitEditStudentButton":
                        await UpdateStudent(student);
                        MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été modifié avec succès !");
                        break;
                    case "submitEditGroupButton":
                        await UpdateGroup(group);
                        MessageBox.Show("Le groupe " + group.Name + " a été modifié avec succès !");
                        break;
                    case "submitEditPromotionButton":
                        await UpdatePromotion(promotion);
                        MessageBox.Show("La promotion " + promotion.Name + " a été modifiée avec succès !");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
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
            CentralFlowLayoutPanel.Controls.Clear();

            await GetStudentById(studentId);

            // ID
            Label studentIDLabel = new Label();
            TextBox studentIDField = new TextBox();

            studentIDLabel.Text = "ID : ";
            studentIDLabel.Name = "IDField";
            studentIDField.Text = student.StudentID.ToString();

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(studentIDLabel);
            CentralFlowLayoutPanel.Controls.Add(studentIDField);

            // FIRSTNAME
            Label studentFirstnameLabel = new Label();
            TextBox studentFirstnameField = new TextBox();

            studentFirstnameLabel.Text = "Prénom : ";
            studentFirstnameField.Name = "FirstnameField";
            studentFirstnameField.Text = student.Firstname.ToString();
            studentFirstnameField.TextChanged += new EventHandler(TextField_Changed);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(studentFirstnameLabel);
            CentralFlowLayoutPanel.Controls.Add(studentFirstnameField);

            // LASTNAME
            Label studentLastnameLabel = new Label();
            TextBox studentLastnameField = new TextBox();

            studentLastnameLabel.Text = "Nom : ";
            studentLastnameField.Name = "LastnameField";
            studentLastnameField.Text = student.Lastname.ToString();
            studentLastnameField.TextChanged += new EventHandler(TextField_Changed);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(studentLastnameLabel);
            CentralFlowLayoutPanel.Controls.Add(studentLastnameField);

            // GROUP
            // https://stackoverflow.com/questions/27723668/how-to-create-a-drop-down-menu-in-winforms-and-c-sharp
            Label groupListLabel = new Label();
            groupListLabel.Text = "Groupe : ";

            ComboBox groupList = new ComboBox();
            groupList.Name = "GroupField";
            groupList.ValueMember = "GroupID";
            groupList.DisplayMember = "Name";
            groupList.DropDownStyle = ComboBoxStyle.DropDownList;

            var groups = await GetGroups();

            groups.Insert(0, new Group { GroupID = student.Group.GroupID, Name = student.Group.Name });
            groupList.DataSource = groups;

            // Détermine la valeur par défaut sélectionnée
            if (student.Group != null)
            {
                groupList.SelectedValue = student.Group.GroupID;
            }
            else
            {
                groupList.SelectedValue = null; // ComboBox vide
            }

            groupList.SelectedValueChanged += new EventHandler(ComboBoxField_Changed);

            CentralFlowLayoutPanel.Controls.Add(groupListLabel);
            CentralFlowLayoutPanel.Controls.Add(groupList);

            // PROMOTION
            Label promotionListLabel = new Label();
            promotionListLabel.Text = "Promotion : ";

            ComboBox promotionList = new ComboBox();
            promotionList.Name = "PromotionField";
            promotionList.ValueMember = "PromotionID";
            promotionList.DisplayMember = "Name";
            promotionList.DropDownStyle = ComboBoxStyle.DropDownList;

            var promotions = await GetPromotions();

            promotions.Insert(0, new Promotion { PromotionID = student.Promotion.PromotionID, Name = student.Promotion.Name });
            promotionList.DataSource = promotions;

            // Détermine la valeur par défaut sélectionnée
            if (student.Group != null)
            {
                promotionList.SelectedValue = student.Promotion.PromotionID;
            }
            else
            {
                promotionList.SelectedValue = null; // ComboBox vide
            }

            promotionList.SelectedValueChanged += new EventHandler(ComboBoxField_Changed);

            CentralFlowLayoutPanel.Controls.Add(promotionListLabel);
            CentralFlowLayoutPanel.Controls.Add(promotionList);

            // SUBMIT
            Button submitBtn = new Button();
            submitBtn.Name = "submitEditStudentButton";
            submitBtn.Text = "Modifier";
            submitBtn.Click += new EventHandler(submitBtn_Click);

            CentralFlowLayoutPanel.Controls.Add(submitBtn);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private async void LoadCentralGroupPanel(int groupId)
        {
            CentralFlowLayoutPanel.Controls.Clear();

            await GetGroupById(groupId);

            // ID
            Label groupIDLabel = new Label();
            TextBox groupIDField = new TextBox();

            groupIDLabel.Text = "ID : ";
            groupIDLabel.Name = "groupIDField";
            groupIDField.Text = group.GroupID.ToString();

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(groupIDLabel);
            CentralFlowLayoutPanel.Controls.Add(groupIDField);

            // NAME
            Label groupNameLabel = new Label();
            TextBox groupNameField = new TextBox();

            groupNameLabel.Text = "Nom : ";
            groupNameField.Name = "GroupNameField";
            groupNameField.Text = group.Name.ToString();
            groupNameField.TextChanged += new EventHandler(TextField_Changed);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(groupNameLabel);
            CentralFlowLayoutPanel.Controls.Add(groupNameField);

            // SUBMIT
            Button submitBtn = new Button();
            submitBtn.Name = "submitEditGroupButton";
            submitBtn.Text = "Modifier";
            submitBtn.Click += new EventHandler(submitBtn_Click);

            CentralFlowLayoutPanel.Controls.Add(submitBtn);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private async void LoadCentralPromotionPanel(int promotionId)
        {
            CentralFlowLayoutPanel.Controls.Clear();

            await GetPromotionById(promotionId);

            // ID
            Label promotionIDLabel = new Label();
            TextBox promotionIDField = new TextBox();

            promotionIDLabel.Text = "ID : ";
            promotionIDLabel.Name = "promotionIDField";
            promotionIDField.Text = promotion.PromotionID.ToString();

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(promotionIDLabel);
            CentralFlowLayoutPanel.Controls.Add(promotionIDField);

            // NAME
            Label promotionNameLabel = new Label();
            TextBox promotionNameField = new TextBox();

            promotionNameLabel.Text = "Nom : ";
            promotionNameField.Name = "PromotionNameField";
            promotionNameField.Text = promotion.Name.ToString();
            promotionNameField.TextChanged += new EventHandler(TextField_Changed);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(promotionNameLabel);
            CentralFlowLayoutPanel.Controls.Add(promotionNameField);

            // SUBMIT
            Button submitBtn = new Button();
            submitBtn.Name = "submitEditPromotionButton";
            submitBtn.Text = "Modifier";
            submitBtn.Click += new EventHandler(submitBtn_Click);

            CentralFlowLayoutPanel.Controls.Add(submitBtn);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        private async Task<List<Student>> GetStudents()
        {
            List<Student> studentList = new List<Student>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    studentList = JsonConvert.DeserializeObject<List<Student>>(responseString);
                }
                return studentList;
            }
        }

        private async Task<List<Group>> GetGroups()
        {
            List<Group> groupList = new List<Group>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Groups");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    groupList = JsonConvert.DeserializeObject<List<Group>>(responseString);
                }

                return groupList;
            }
        }

        private async Task<List<Promotion>> GetPromotions()
        {
            List<Promotion> promotionList = new List<Promotion>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Promotions");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    promotionList = JsonConvert.DeserializeObject<List<Promotion>>(responseString);
                }

                return promotionList;
            }
        }

        private async Task<Student> GetStudentById(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    student = JsonConvert.DeserializeObject<Student>(responseString);
                }

                return student;
            }
        }

        private async Task<Group> GetGroupById(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Groups/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    group = JsonConvert.DeserializeObject<Group>(responseString);
                }

                return group;
            }
        }

        private async Task<Promotion> GetPromotionById(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Promotions/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    promotion = JsonConvert.DeserializeObject<Promotion>(responseString);
                }

                return promotion;
            }
        }

        private async Task<Student> UpdateStudent(Student student)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                    "https://localhost:7058/api/Students/" + student.StudentID,
                    student
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Student>();
            }
        }

        private async Task<Group> UpdateGroup(Group group)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                    "https://localhost:7058/api/Groups/" + group.GroupID,
                    group
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Group>();
            }
        }

        private async Task<Promotion> UpdatePromotion(Promotion promotion)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                    "https://localhost:7058/api/Promotions/" + promotion.PromotionID,
                    promotion
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Promotion>();
            }
        }

        private void CreateStudent_Click(object sender, EventArgs e)
        {
            var createStudentForm = new CreateStudentForm();
            createStudentForm.Show();
        }
        private void CreateGroup_Click(object sender, EventArgs e)
        {
            var createGroupForm = new CreateGroupForm();
            createGroupForm.Show();
        }

        private void CreatePromotion_Click(object sender, EventArgs e)
        {
            var createPromotionForm = new CreatePromotionForm();
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
    }
}