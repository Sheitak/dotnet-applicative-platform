namespace MobileAppV2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = App.student;
        }

        private async Task<object> LoginAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri("https://exnet.3il.fr"),
                    new Uri("myapp://callback")
                );

                string accessToken = authResult?.AccessToken;
                var yu = authResult;

                //return accessToken;

                var student = new { StudentID = 1, Firstname = "Carson", Lastname = "Alexander" };

                return student;
            }
            catch (TaskCanceledException e)
            {
                return e.Message;
            }
        }
    }
}