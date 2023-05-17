using DesktopApp.Forms;
using DesktopApp.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Json;

namespace DesktopApp.Forms
{
    public partial class CreateStudentForm : Form
    {
        static readonly string urlBase = "https://localhost:7058/api";
        Student student = new Student();

        public CreateStudentForm()
        {
            InitializeComponent();
            InitializeComboBox();
            CenterToScreen();
        }

        private async void InitializeComboBox()
        {
            comboBoxGroup.DataSource = await GetGroups();
            comboBoxPromotion.DataSource = await GetPromotions();
        }

        private async void submitCreateStudent_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(student.Firstname + student.Lastname + ' ' + student.GroupID + student.Group.Name);
                await CreateStudent(student);
                MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été créé avec succès !");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<Student> CreateStudent(Student student)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "https://localhost:7058/api/Students",
                    student
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Student>();
            }
        }

        private void firstnameField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                student.Firstname = textBox.Text;
            }
        }

        private void lastnameField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                student.Lastname = textBox.Text;
            }
        }

        private void comboBoxGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                student.GroupID = (int)comboBox.SelectedValue;
                student.Group = new Group
                {
                    GroupID = (int)comboBox.SelectedValue,
                    Name = comboBox.Text,
                };
            }
        }

        private void comboBoxPromotion_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                student.PromotionID = (int)comboBox.SelectedValue;
                student.Promotion = new Promotion
                {
                    PromotionID = (int)comboBox.SelectedValue,
                    Name = comboBox.Text,
                };
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
    }
}
