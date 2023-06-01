using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Forms
{
    public partial class ImportGroupsForm : Form
    {
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        Group group = new();

        public ImportGroupsForm()
        {
            InitializeComponent();
            CenterToScreen();
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
                foreach (string[] row in csvData)
                {
                    group.Name = row[0];
                    await dataSourceProvider.CreateGroup(group);
                }
                MessageBox.Show("Les groupes ont été importés avec succès !");
            }
        }

        public List<string[]> ImportCsv(string filePath, char delimiter)
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
