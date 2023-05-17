using DesktopApp.Models;

namespace DesktopApp.Forms
{
    public partial class ImportPromotionsForm : Form
    {
        Promotion promotion = new Promotion();

        public ImportPromotionsForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void csvImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                List<string[]> csvData = ImportCsv(filePath, ';');
                csvData.RemoveAt(0);
                foreach (string[] row in csvData)
                {
                    promotion.Name = row[0];

                    await CreatePromotion(promotion);
                }

                MessageBox.Show("Les promotions ont été importées avec succès !");
            }
        }

        public List<string[]> ImportCsv(string filePath, char delimiter)
        {
            List<string[]> csvData = new List<string[]>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] fields = line.Split(delimiter);
                        csvData.Add(fields);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while importing the CSV file: " + ex.Message);
            }

            return csvData;
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
    }
}
