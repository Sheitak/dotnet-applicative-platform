using DesktopApp.Models;
using Newtonsoft.Json;

namespace DesktopApp.Forms
{
    public partial class ImportStudentsForm : Form
    {
        Student student = new Student();

        public ImportStudentsForm()
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
                    var group = await GetGroupByName(row[2]);
                    var promotion = await GetPromotionByName(row[3]);
                    
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

                    await CreateStudent(student);
                }

                MessageBox.Show("Les étudiants ont été importés avec succès !");
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

        private async Task<Student> CreateStudent(Student student)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "https://localhost:7058/api/Students",
                    student
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Student>();
            }
        }

        private async Task<Group> GetGroupByName(string name)
        {
            Group group = new Group();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Groups/ByName/"+name);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    group = JsonConvert.DeserializeObject<Group>(responseString);
                }

                return group;
            }
        }

        private async Task<Promotion> GetPromotionByName(string name)
        {
            Promotion promotion = new Promotion();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Promotions/ByName/"+name);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    promotion = JsonConvert.DeserializeObject<Promotion>(responseString);
                }

                return promotion;
            }
        }
    }
}
