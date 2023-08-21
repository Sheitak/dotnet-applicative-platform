using MobileAppV2.Models;

namespace MobileAppV2
{
    public partial class App : Application
    {
        public static Student Student { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}