using MobileAppV2.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
            using var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.GetAsync($"http://10.0.2.2:5283/api/Students/GetByIdWithMacAddress/{id}/{macAddress}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Student>(responseString);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseString);

                // 403 FORBIDDEN
                if (problemDetails.Status == 403)
                {
                    bool sendRegisterDeviceRequest = await Application.Current.MainPage.DisplayAlert(
                        problemDetails.Title,
                        problemDetails.Detail,
                        "Oui", "Non"
                    );

                    if (sendRegisterDeviceRequest)
                    {
                        await CreateRegisterDeviceRequest(id, macAddress);
                    }
                }

                // 423 LOCKED
                if (problemDetails.Status == 423)
                {
                    bool sendEmail = await Application.Current.MainPage.DisplayAlert(
                        problemDetails.Title,
                        problemDetails.Detail,
                        "Oui", "Non"
                    );

                    if (sendEmail)
                    {
                        var message = new EmailMessage
                        {
                            To = new List<string>
                        {
                            "admin@3il.fr"
                        },
                            Subject = "Demande d'activation",
                            Body = $"Etudiant : John Doe. Relance d'une demande d'activation pour l'appareil : {macAddress}"
                        };

                        await Email.ComposeAsync(message);
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erreur",
                    "Une erreur s'est produite lors de la récupération des informations. Veuillez réessayer plus tard.",
                    "OK"
                );
            }
            return null;
        }

        internal async Task CreateRegisterDeviceRequest(int id, string macAddress)
        {
            var registerDevice = new Models.Device
            {
                MacAddress = macAddress,
                IsActive = false,
                RegisteredAt = DateTime.Now,
                StudentID = id
            };

            using var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.Token);

            HttpResponseMessage response = await client.PostAsJsonAsync("http://10.0.2.2:5283/api/Devices", registerDevice);
            response.EnsureSuccessStatusCode();

            string successMessage = "La nouvelle demande a été envoyée avec succès. " +
                "N'oubliez pas d'avertir la vie étudiante de vos changements d'appareils pour l'activation.";
            
            string failureMessage = "Une erreur s'est produite lors de l'envoi de la nouvelle demande. " +
                "Veuillez vous rapprocher de l'administration pour plus de détails et l'enregistrement de votre appareil.";

            await DisplayResultMessage(response.IsSuccessStatusCode, "Demande envoyée !", successMessage, failureMessage);
        }

        internal async Task DisplayResultMessage(bool isSuccess, string successTitle, string successMessage, string failureMessage)
        {
            string title = isSuccess ? successTitle : "Echec de la demande";
            string message = isSuccess ? successMessage : failureMessage;

            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
