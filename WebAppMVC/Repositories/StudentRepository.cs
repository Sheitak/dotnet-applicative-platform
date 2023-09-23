using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebAppMVC.Models;

namespace WebAppMVC.Repositories
{
    public class StudentRepository
    {
        public async Task<List<Student>> GetStudents(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Students");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Student>>(responseString);
            }

            return new List<Student>();
        }

        public async Task<Student> GetStudent(int id, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Students/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Student>(responseString);
            }

            throw new Exception("Failed to retrieve student.");
        }
    }
}
