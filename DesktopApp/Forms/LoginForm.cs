using DesktopApp.Models;
using DesktopApp.Services;

namespace DesktopApp.Forms
{
    public partial class LoginForm : Form
    {
        DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();
        public bool AuthenticationSuccessful { get; private set; } = false;
        public User AuthenticatedUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            var user = new User
            {
                Email = EmailField.Text,
                Password = PasswordField.Text
            };

            try
            {
                Auth auth = await dataSourceProvider.Authentication(user);

                if (auth.User != null)
                {
                    AuthenticationSuccessful = true;
                    AuthenticatedUser = auth.User;
                    Close();
                }
                else
                {
                    MessageBox.Show("Authentification échouée. Veuillez vérifier vos informations d'identification.", "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'authentification : {ex.Message}", "Erreur d'authentification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
