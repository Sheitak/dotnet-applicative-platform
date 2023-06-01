using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Forms
{
    public partial class CreateGroupForm : Form
    {
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Group group = new();

        public CreateGroupForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void SubmitCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreateGroup(group);
                MessageBox.Show("Le groupe " + group.Name + " a été créé avec succès !");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nameField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                group.Name = textBox.Text;
            }
        }
    }
}
