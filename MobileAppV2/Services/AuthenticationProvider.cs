using MobileAppV2.Models;

namespace MobileAppV2.Services
{
    internal class AuthenticationProvider
    {
        private AuthenticationProvider() { }

        private static AuthenticationProvider _instance;

        public static AuthenticationProvider GetInstance()
        {
            _instance ??= new AuthenticationProvider();
            return _instance;
        }

        internal async Task<Student> LoginAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri("https://exnet.3il.fr"),
                    new Uri("myapp://")
                );

                /*
                string email = null;

                if (authResult?.Properties != null && authResult.Properties.ContainsKey("email"))
                {
                    email = authResult.Properties["email"];
                }
                */

                // TODO : Envoyer l'email après l'authentification au serveur pour récupérer les informations de l'élève.

                var student = new Student { StudentID = 1, Firstname = "Carson", Lastname = "Alexander" };

                return student;
            }
            catch (TaskCanceledException ex)
            {
                //throw new Exception("L'authentification a été annulée.", ex);
                var student = new Student { StudentID = 1, Firstname = "Carson", Lastname = "Alexander" };

                return student;
            }
        }
    }
}
