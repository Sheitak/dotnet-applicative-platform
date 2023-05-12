using Newtonsoft.Json;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender, EventArgs e)
        {

            await LoginAsync();


            /*
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://exnet.3il.fr");

                if (response.IsSuccessStatusCode)
                {
                    

                }

            }
            */
        }

        
        private async Task<string> LoginAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri("https://exnet.3il.fr"),
                    new Uri("myapp://callback")
                );

                string accessToken = authResult?.AccessToken;
                var yu = authResult;

                WebAuth.Text = yu.ToString();

                //return accessToken;
                return yu.ToString();

            }
            catch (TaskCanceledException e)
            {
                return e.Message;
            }
        }
    }
}