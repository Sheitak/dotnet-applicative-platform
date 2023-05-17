using DesktopApp.Models;

namespace DesktopApp.Forms
{
    public partial class CreatePromotionForm : Form
    {
        Promotion promotion = new Promotion();

        public CreatePromotionForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void SubmitCreatePromotion_Click(object sender, EventArgs e)
        {
            try
            {
                await CreatePromotion(promotion);
                MessageBox.Show("La promotion " + promotion.Name + " a été créé avec succès !");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<Promotion> CreatePromotion(Promotion promotion)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "https://localhost:7058/api/Promotions",
                    promotion
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Promotion>();
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
