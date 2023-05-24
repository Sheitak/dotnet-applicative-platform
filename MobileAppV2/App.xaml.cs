using MobileAppV2.Models;

namespace MobileAppV2
{
    public partial class App : Application
    {
        internal static Student student;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            student = new Student()
            {
                StudentID = 1,
                Firstname = "Carson",
                Lastname = "Alexander"
            };
        }
    }
}