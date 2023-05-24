using MobileApp.Models;
using MobileApp.Services;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        private DataSourceService dataSourceService;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender, EventArgs e)
        {

            await LoginAsync();

            var network = GetMacAddress();
            //var studentResult = await dataSourceService.GetInformationsForGenerateQrCode(network.ToString());
            var studentResult = await GetInformationsForGenerateQrCode(network.ToString());

            MacAddress.Text = studentResult.MacAdress;
            // 82A70095380B
        }

        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
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

                //WebAuth.Text = yu.ToString();

                //return accessToken;
                //return yu.ToString();

                var student = new { StudentID = 1, Firstname = "Carson", Lastname = "Alexander" };

                return student;
            }
            catch (TaskCanceledException e)
            {
                return e.Message;
            }
        }

        private async Task<Student> GetInformationsForGenerateQrCode(string macAddress)
        {
            Student student = new Student();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://10.0.2.2:5283/api/Students/GetByMacAddress/" + macAddress);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    student = JsonConvert.DeserializeObject<Student>(responseString);

                    //return responseString;
                    //return student;
                }

                return student;
            }
        }
    }
}