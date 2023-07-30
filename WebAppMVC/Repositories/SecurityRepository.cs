using Newtonsoft.Json;
using WebAppMVC.Models;

namespace WebAppMVC.Repositories
{
    public class SecurityRepository
    {
        private Auth _auth;

        internal async Task<Auth> Authentication(User user)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7058/api/Users/Auth", user);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            _auth = JsonConvert.DeserializeObject<Auth>(responseString);

            return _auth;
        }
    }
}
