﻿using Newtonsoft.Json;
using WebAppMVC.Models;

namespace WebAppMVC.Repositories
{
    public class StudentRepository
    {
        // https://www.compilemode.com/2020/12/get-data-in-aspnet-mvc-using-web-api.html

        public async Task<List<Student>> GetAllStudents()
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Student>>(responseString);
            }

            return new List<Student>();
        }

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
