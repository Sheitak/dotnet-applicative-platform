using DesktopApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DesktopApp.Services
{
    internal class DataSourceProvider
    {
        private DataSourceProvider() { }

        private static DataSourceProvider? _instance;
        private Auth _auth;

        public static DataSourceProvider GetInstance()
        {
            _instance ??= new DataSourceProvider();
            return _instance;
        }

        // AUTH
        internal async Task<Auth> Authentication(User user)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7058/api/Users/Auth", user);
            response.EnsureSuccessStatusCode();

            _auth = await response.Content.ReadAsAsync<Auth>();

            return _auth;
        }

        internal async Task<User> GetUser(string id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(responseString) ?? throw new Exception("Failed to deserialize user.");
            }

            throw new Exception($"Failed to retrieve user. StatusCode: {response.StatusCode}");
        }

        // GET COLLECTION
        internal async Task<List<Student>> GetStudents()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Students");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Student>>(responseString);
            }

            return new List<Student>();
        }

        internal async Task<List<Group>> GetGroups()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Groups");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Group>>(responseString);
            }

            return new List<Group>();
        }

        internal async Task<List<Promotion>> GetPromotions()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync("https://localhost:7058/api/Promotions");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Promotion>>(responseString);
            }

            return new List<Promotion>();
        }

        // GET ITEM BY ID
        internal async Task<Student> GetStudentById(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Students/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Student>(responseString) ?? throw new Exception("Failed to deserialize student.");
            }

            throw new Exception($"Failed to retrieve student with ID {id}. StatusCode: {response.StatusCode}");
        }

        internal async Task<Group> GetGroupById(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Groups/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Group>(responseString) ?? throw new Exception("Failed to deserialize group.");
            }

            throw new Exception($"Failed to retrieve group with ID {id}. StatusCode: {response.StatusCode}");
        }

        internal async Task<Promotion> GetPromotionById(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Promotions/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Promotion>(responseString) ?? throw new Exception("Failed to deserialize promotion.");
            }

            throw new Exception($"Failed to retrieve promotion with ID {id}. StatusCode: {response.StatusCode}");
        }

        // GET ITEM BY NAME
        internal async Task<Group> GetGroupByName(string name)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Groups/ByName/{name}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Group>(responseString) ?? throw new Exception("Failed to deserialize group.");
            }

            throw new Exception($"Failed to retrieve group with name '{name}'. StatusCode: {response.StatusCode}");
        }

        internal async Task<Promotion> GetPromotionByName(string name)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Promotions/ByName/{name}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Promotion>(responseString) ?? throw new Exception("Failed to deserialize promotion.");
            }

            throw new Exception($"Failed to retrieve promotion with name '{name}'. StatusCode: {response.StatusCode}");
        }

        // POST
        internal async Task<Student> CreateStudent(Student student)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7058/api/Students", student);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Student>();
        }

        internal async Task<Group> CreateGroup(Group group)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7058/api/Groups", group);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Group>();
        }

        internal async Task<Promotion> CreatePromotion(Promotion promotion)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7058/api/Promotions", promotion);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Promotion>();
        }

        // PUT
        internal async Task<Student> UpdateStudent(Student student)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7058/api/Students/{student.StudentID}",
                student
            );
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Student>();
        }

        internal async Task<Group> UpdateGroup(Group group)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7058/api/Groups/{group.GroupID}",
                group
            );
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Group>();
        }

        internal async Task<Promotion> UpdatePromotion(Promotion promotion)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7058/api/Promotions/{promotion.PromotionID}",
                promotion
            );
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Promotion>();
        }

        internal async Task<bool> DeleteStudent(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7058/api/Students/{id}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        internal async Task<bool> DeleteGroup(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7058/api/Groups/{id}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        internal async Task<bool> DeletePromotion(int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7058/api/Promotions/{id}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }
    }
}
