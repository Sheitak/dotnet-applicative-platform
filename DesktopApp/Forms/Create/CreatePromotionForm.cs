using DesktopApp.Models;
using DesktopApp.Services;
using static DesktopApp.FormInterface;

namespace DesktopApp.Forms
{
    public partial class CreatePromotionForm : Form
    {
        private readonly LoadEntitiesDelegate loadEntitiesDelegate;
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Promotion promotion = new();

        public CreatePromotionForm(LoadEntitiesDelegate loadEntitiesDelegate)
        {
            InitializeComponent();
            CenterToScreen();
            this.loadEntitiesDelegate = loadEntitiesDelegate;
        }

        private async void CreatePromotionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreatePromotion(promotion);
                MessageBox.Show("La promotion " + promotion.Name + " a été créé avec succès !");
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
                promotion.Name = textBox.Text;
            }
        }
    }
}
