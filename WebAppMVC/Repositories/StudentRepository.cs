using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAppMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAppMVC.Repositories
{
    public class StudentRepository
    {
        // https://www.compilemode.com/2020/12/get-data-in-aspnet-mvc-using-web-api.html

        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> studentsList =  new List<Student>();
            
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students");

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    studentsList = JsonConvert.DeserializeObject<List<Student>>(responseString);

                }

            }
            return studentsList;
            
        }

        public async Task<Student> GetStudent()
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students/1");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Student>(responseString);
            }

            throw new Exception();
        }
    }
}
