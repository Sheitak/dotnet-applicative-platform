using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Forms
{
    public partial class CreateStudentForm : Form
    {
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Student student = new();

        public CreateStudentForm()
        {
            InitializeComponent();
            InitializeComboBox();
            CenterToScreen();
        }

        private async void InitializeComboBox()
        {
            comboBoxGroup.DataSource = await dataSourceProvider.GetGroups();
            comboBoxPromotion.DataSource = await dataSourceProvider.GetPromotions();
        }

        private async void submitCreateStudent_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreateStudent(student);
                MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été créé avec succès !");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
