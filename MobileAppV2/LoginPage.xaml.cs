using MobileAppV2.Models;
using MobileAppV2.Services;

namespace MobileAppV2
{
    public partial class LoginPage : ContentPage
    {
        public Student _student;

        AuthenticationProvider authenticationProvider = AuthenticationProvider.GetInstance();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            _student = await authenticationProvider.LoginAsync();

            if (_student != null)
            {
                App.Student = _student;
                await Shell.Current.GoToAsync("//AppStudent");
            }
            else
            {
                await DisplayAlert("Erreur d'authentification", "L'authentification a échoué.", "OK");
            }
        }
    }
}
