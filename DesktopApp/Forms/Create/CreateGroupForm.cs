using DesktopApp.Models;

namespace DesktopApp.Forms
{
    public partial class CreateGroupForm : Form
    {
        Group group = new Group();

        public CreateGroupForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void SubmitCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                await CreateGroup(group);
                MessageBox.Show("Le groupe " + group.Name + " a été créé avec succès !");
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void nameField_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                group.Name = textBox.Text;
            }
        }
    }
}
