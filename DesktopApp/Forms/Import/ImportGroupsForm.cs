using DesktopApp.Models;

namespace DesktopApp.Forms
{
    public partial class ImportGroupsForm : Form
    {
        Group group = new Group();

        public ImportGroupsForm()
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
                    group.Name = row[0];

                    await CreateGroup(group);
                }

                MessageBox.Show("Les groupes ont été importés avec succès !");
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

        private async Task<Group> CreateGroup(Group group)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "https://localhost:7058/api/Groups",
                    group
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Group>();
            }
        }
    }
}
