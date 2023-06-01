using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Forms
{
    public partial class CreatePromotionForm : Form
    {
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        readonly Promotion promotion = new();

        public CreatePromotionForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void SubmitCreatePromotion_Click(object sender, EventArgs e)
        {
            try
            {
                await dataSourceProvider.CreatePromotion(promotion);
                MessageBox.Show("La promotion " + promotion.Name + " a été créé avec succès !");
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
                promotion.Name = textBox.Text;
            }
        }
    }
}
