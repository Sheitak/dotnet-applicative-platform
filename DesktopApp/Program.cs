using DesktopApp.Forms;
using DesktopApp.Models;

namespace DesktopApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();

            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            // Si LoginForm se ferme avec succès et que l'authentification réussit, affichez le formulaire FormInterface
            if (loginForm.AuthenticationSuccessful)
            {
                User authenticatedUser = loginForm.AuthenticatedUser;
                FormInterface formInterface = new FormInterface(authenticatedUser);
                Application.Run(formInterface);
            }
        }
    }
}