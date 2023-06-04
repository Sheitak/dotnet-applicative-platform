using DesktopApp.Models;
using DesktopApp.Services;
using static DesktopApp.FormInterface;

namespace DesktopApp.Forms
{
    public partial class CreateStudentForm : Form
    {
        private readonly LoadEntitiesDelegate loadEntitiesDelegate;
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Student student = new();

        public CreateStudentForm(LoadEntitiesDelegate loadEntitiesDelegate)
        {
            InitializeComponent();
            InitializeComboBox();
            CenterToScreen();
            this.loadEntitiesDelegate = loadEntitiesDelegate;
        }

        private async void InitializeComboBox()
        {
            ComboBoxGroup.DataSource = await dataSourceProvider.GetGroups();
            ComboBoxPromotion.DataSource = await dataSourceProvider.GetPromotions();
        }

        private async void CreateStudentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreateStudent(student);
                MessageBox.Show("L'étudiant " + student.Firstname + " " + student.Lastname + " a été créé avec succès !");
                loadEntitiesDelegate.Invoke();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FirstnameField_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                student.Firstname = textBox.Text;
            }
        }

        private void LastnameField_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                student.Lastname = textBox.Text;
            }
        }

        private void ComboBoxGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                student.GroupID = (int)comboBox.SelectedValue;
                student.Group = new Group
                {
                    GroupID = (int)comboBox.SelectedValue,
                    Name = comboBox.Text,
                };
            }
        }

        private void ComboBoxPromotion_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
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
