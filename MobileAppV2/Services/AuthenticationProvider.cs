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

        internal async Task<object> LoginAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri("https://exnet.3il.fr"),
                    new Uri("myapp://")
                );

                string accessToken = authResult?.AccessToken;
                var yu = authResult;

                //return accessToken;

                var student = new { StudentID = 1, Firstname = "Carson", Lastname = "Alexander" };

                return student;
            }
            catch (TaskCanceledException ex)
            {
                return ex;
            }
        }
    }
}
