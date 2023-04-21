using ZXing.Net.Maui;

namespace MobileApp.Views;

// https://www.youtube.com/watch?v=ostgj2xB_ok
// https://www.reddit.com/r/dotnetMAUI/comments/tm1s4p/qr_code_reader/

public partial class Signature : ContentPage
{
	public Signature()
	{
		InitializeComponent();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = false
        };
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        //foreach (var barcode in e.Results)
        //    Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        Dispatcher.Dispatch(() =>
        {
            lblBarcodeResult.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
        });
    }
}