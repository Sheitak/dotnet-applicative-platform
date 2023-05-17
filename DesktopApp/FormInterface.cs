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

        public FormInterface()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void LoadStudents_Click(object sender, EventArgs e)
        {
            List<Student> studentsList = new List<Student>();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    studentsList = JsonConvert.DeserializeObject<List<Student>>(responseString);
                }

            }
            ListFlowLayoutPanel.Controls.Clear();
            CentralFlowLayoutPanel.Controls.Clear();

            // PANEL GAUCHE
            ListView studentListView = new ListView();
            studentListView.Name = "StudentListView";
            studentListView.Bounds = new Rectangle(new Point(10, 10), new Size(125, 400));

            studentListView.View = View.Details;
            studentListView.SelectedIndexChanged += studentListView_SelectedIndexChanged;

            studentListView.Columns.Add("Elèves", -2, HorizontalAlignment.Left);
            studentListView.FullRowSelect = true;
            studentListView.GridLines = true;

            studentListView.Columns[0].Width = studentListView.Width - 4 - SystemInformation.VerticalScrollBarWidth;

            foreach (Student student in studentsList)
            {
                ListViewItem item = new ListViewItem(student.Firstname + ' ' + student.Lastname);
                item.Tag = student.StudentID;
                studentListView.Items.Add(item);
            }

            studentListView.Focus();
            studentListView.Items[0].Selected = true;

            ListFlowLayoutPanel.Controls.Add(studentListView);
        }

        private void StudentField_Changed(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            //MessageBox.Show(textBox.Name + " text changed. Value " + textBox.Text);
            switch (textBox.Name)
            {
                case "FirstnameField":
                    student.Firstname = textBox.Text;
                    break;
                case "LastnameField":
                    student.Lastname = textBox.Text;
                    break;
            }
        }

        private void StudentFieldCB_Changed(object sender, EventArgs e)
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

        async void submitEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                await UpdateStudent(student);
                MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été modifié avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void studentListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;

            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView.SelectedItems[0];
                //MessageBox.Show(selectedItem.Tag.ToString() + ' ' + selectedItem.Text);
                LoadCentralPanel((int)selectedItem.Tag);
                setDefaultComboBoxValue();
            }
        }

        private void setDefaultComboBoxValue()
        {

        }

        private async void LoadCentralPanel(int studentId)
        {
            CentralFlowLayoutPanel.Controls.Clear();

            //var student = 
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
            studentFirstnameField.TextChanged += new EventHandler(StudentField_Changed);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            CentralFlowLayoutPanel.Controls.Add(studentFirstnameLabel);
            CentralFlowLayoutPanel.Controls.Add(studentFirstnameField);

            // LASTNAME
            Label studentLastnameLabel = new Label();
            TextBox studentLastnameField = new TextBox();

            studentLastnameLabel.Text = "Nom : ";
            studentLastnameField.Name = "LastnameField";
            studentLastnameField.Text = student.Lastname.ToString();
            studentLastnameField.TextChanged += new EventHandler(StudentField_Changed);

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

            groupList.SelectedValueChanged += new EventHandler(StudentFieldCB_Changed);

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

            promotionList.SelectedValueChanged += new EventHandler(StudentFieldCB_Changed);

            CentralFlowLayoutPanel.Controls.Add(promotionListLabel);
            CentralFlowLayoutPanel.Controls.Add(promotionList);

            // SUBMIT
            Button studentEditButton = new Button();
            studentEditButton.Text = "Modifier";
            studentEditButton.Click += new EventHandler(submitEditButton_Click);

            CentralFlowLayoutPanel.Controls.Add(studentEditButton);

            CentralFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
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

        private void CreateStudent_Click(object sender, EventArgs e)
        {
            var createStudentForm = new CreateStudentForm();
            createStudentForm.Show();
        }

        private void ImportStudents_Click(object sender, EventArgs e)
        {
            var importStudentsForm = new ImportStudentsForm();
            importStudentsForm.Show();
        }
    }
}