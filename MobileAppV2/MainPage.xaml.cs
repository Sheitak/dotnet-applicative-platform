using MobileAppV2.Services;

namespace MobileAppV2
{
    public partial class MainPage : ContentPage
    {
        AuthenticationProvider authenticationProvider = AuthenticationProvider.GetInstance();

        public MainPage()
        {
            InitializeComponent();

            BindingContext = App.student;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await authenticationProvider.LoginAsync();
        }
    }
}