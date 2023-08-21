namespace MobileAppV2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var student = App.Student;

            if (student != null)
            {
                BindingContext = student;
                StudentName.Text = student.Firstname + " " + student.Lastname;

            }
            else
            {
                FailedLogin();
            }
        }

        private async void FailedLogin()
        {
            await DisplayAlert("Aucune Connexion.", "L'authentification a échoué.", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}