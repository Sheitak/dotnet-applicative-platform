using DesktopApp.Models;
using DesktopApp.Services;
using static DesktopApp.FormInterface;

namespace DesktopApp.Forms
{
    public partial class ImportStudentsForm : Form
    {
        private readonly LoadEntitiesDelegate loadEntitiesDelegate;
        readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        Student student = new();

        public ImportStudentsForm(LoadEntitiesDelegate loadEntitiesDelegate)
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
                if (csvData.Count == 0 || csvData.Count < 2)
                {
                    MessageBox.Show("Le fichier CSV est vide ou ne correspond pas à la donnée attendue.");
                    return;
                }
                foreach (string[] row in csvData)
                {
                    var group = await dataSourceProvider.GetGroupByName(row[2]);
                    var promotion = await dataSourceProvider.GetPromotionByName(row[3]);

                    student.Firstname = row[0];
                    student.Lastname = row[1];
                    student.GroupID = group.GroupID;
                    student.Group = new Group
                    {
                        GroupID = group.GroupID,
                        Name = group.Name
                    };
                    student.PromotionID = promotion.PromotionID;
                    student.Promotion = new Promotion
                    {
                        PromotionID = promotion.PromotionID,
                        Name = promotion.Name
                    };

                    await dataSourceProvider.CreateStudent(student);
                }

                MessageBox.Show("Les étudiants ont été importés avec succès !");
                loadEntitiesDelegate.Invoke();
                Close();
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
                Console.WriteLine($"An error occurred while importing the CSV file: {ex.Message}");
            }

            return csvData;
        }
    }
}
