using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    
    internal class DataSourceService
    {
        string studentInformations = "";
        //Student student = new Student();

        public async Task<string> GetInformationsForGenerateQrCode(string macAddress)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students/GetByMacAddress/"+macAddress);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    //student = JsonConvert.DeserializeObject<Student>(responseString);
                    if (responseString != null)
                    {
                        studentInformations = responseString;
                    }
                }

                return studentInformations;
            }
        }
    }
}
