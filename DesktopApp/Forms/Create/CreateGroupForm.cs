using DesktopApp.Models;
using DesktopApp.Services;
using static DesktopApp.FormInterface;

namespace DesktopApp.Forms
{
    public partial class CreateGroupForm : Form
    {
        private readonly LoadEntitiesDelegate loadEntitiesDelegate;
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Group group = new();

        public CreateGroupForm(LoadEntitiesDelegate loadEntitiesDelegate)
        {
            InitializeComponent();
            CenterToScreen();
            this.loadEntitiesDelegate = loadEntitiesDelegate;
        }

        private async void CreateGroupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreateGroup(group);
                MessageBox.Show("Le groupe " + group.Name + " a été créé avec succès !");
                loadEntitiesDelegate.Invoke();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NameField_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                group.Name = textBox.Text;
            }
        }
    }
}
