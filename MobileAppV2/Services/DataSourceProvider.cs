using MobileAppV2.Models;
using Newtonsoft.Json;

namespace MobileAppV2.Services
{
    internal class DataSourceProvider
    {
        private DataSourceProvider() { }

        private static DataSourceProvider _instance;

        public static DataSourceProvider GetInstance()
        {
            _instance ??= new DataSourceProvider();
            return _instance;
        }

        internal async Task<Student> GetInformationsForGenerateQrCode(int id, string macAddress)
        {
            Student student = new Student();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://10.0.2.2:5283/api/Students/GetByIdWithMacAddress/" + id + "/" + macAddress);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    student = JsonConvert.DeserializeObject<Student>(responseString);
                }
                return student;
            }
        }
    }
}
