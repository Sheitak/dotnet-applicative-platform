namespace MobileApp.Views;

public partial class About : ContentPage
{
	public About()
	{
		InitializeComponent();
	}

    private async void About_Clicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.3il-ingenieurs.fr/");
    }
}