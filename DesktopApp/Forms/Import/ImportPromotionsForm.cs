using DesktopApp.Models;
using DesktopApp.Services;
using static DesktopApp.FormInterface;

namespace DesktopApp.Forms
{
    public partial class ImportPromotionsForm : Form
    {
        private readonly LoadEntitiesDelegate loadEntitiesDelegate;
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        Promotion promotion = new();

        public ImportPromotionsForm(LoadEntitiesDelegate loadEntitiesDelegate)
        {
            InitializeComponent();
            CenterToScreen();
            this.loadEntitiesDelegate = loadEntitiesDelegate;
        }

        private async void CSVImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "CSV files (*.csv)|*.csv",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                List<string[]> csvData = ImportCsv(filePath, ';');
                csvData.RemoveAt(0);
                if (csvData.Count == 0)
                {
                    MessageBox.Show("Le fichier CSV est vide ou ne correspond pas à la donnée attendue.");
                    return;
                }
                foreach (string[] row in csvData)
                {
                    promotion.Name = row[0];
                    await dataSourceProvider.CreatePromotion(promotion);
                }
                MessageBox.Show("Les promotions ont été importées avec succès !");
                loadEntitiesDelegate.Invoke();
                Close();
            }
        }

        private List<string[]> ImportCsv(string filePath, char delimiter)
        {
            List<string[]> csvData = new();

            try
            {
                using StreamReader reader = new(filePath);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(delimiter);
                    csvData.Add(fields);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while importing the CSV file: " + ex.Message);
            }

            return csvData;
        }
    }
}
