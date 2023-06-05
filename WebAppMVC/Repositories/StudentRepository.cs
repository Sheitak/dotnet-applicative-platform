using Newtonsoft.Json;
using WebAppMVC.Models;

namespace WebAppMVC.Repositories
{
    public class StudentRepository
    {
        public async Task<Student> GetStudent(int id)
        {
            using var client = new HttpClient();
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
