using Newtonsoft.Json;
using WebAppMVC.Models;

namespace WebAppMVC.Repositories
{
    public class DeviceRepository
    {
        public async Task<Device> GetDevice(int id)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7058/api/Devices/GetById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Device>(responseString);
            }

            throw new Exception("Failed to retrieve device.");
        }

        public async Task<Device> PutDevice(Device device)
        {
            using var client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:7058/api/Devices/{device.DeviceID}",
                device
            );

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Device>(responseString);
            }

            throw new Exception("Failed to retrieve device.");
        }
    }
}
