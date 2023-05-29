using TerminalApp.Models;

namespace TerminalApp.Services
{
    internal class DataSourceProvider
    {
        private DataSourceProvider() {}

        private static DataSourceProvider? _instance;

        public static DataSourceProvider GetInstance()
        {
            _instance ??= new DataSourceProvider();
            return _instance;
        }

        internal async Task<Signature> CreateSignature(Signature signature)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "https://localhost:7058/api/Signatures",
                    signature
                );
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Signature>();
            }
        }
    }
}
