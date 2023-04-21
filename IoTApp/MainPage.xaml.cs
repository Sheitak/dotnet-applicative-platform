using IoTApp.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using ZXing.Windows.Compatibility;
using Windows.UI.Xaml.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTApp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            GenerateQrCodeAsync();

            // https://www.codeproject.com/Articles/5357944/Create-Your-Own-QR-Codes-with-ZXing-NET
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            /*
            var barCode = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                
                Options = new QrCodeEncodingOptions
                {
                    Margin = 1,
                    Height = 350,
                    Width = 350,
                    ErrorCorrection = ErrorCorrectionLevel.Q,
                }
                
            };
            var writeBarCode = barCode.Write(jsonSign);

            QrCodeSign.Source = writeBarCode;
            */
        }

        private void GenerateQrCodeAsync() {
            var sign = new Signature
            {
                IsPresent = true,
                CreatedAt = DateTime.Now
            };
            string jsonSign = Newtonsoft.Json.JsonConvert.SerializeObject(sign);

            string imageFileName = "klc_qr_code.png";

            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Margin = 1,
                Width = 350,
                Height = 350,
                ErrorCorrection = ErrorCorrectionLevel.Q
            };
            var writer = new ZXing.Windows.Compatibility.BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            Bitmap qrCodeBitmap = writer.Write(jsonSign);
            qrCodeBitmap.Save(imageFileName);

            // Convert bitmap to image source and set as the source for the Image control
            using (var stream = new MemoryStream())
            {
                qrCodeBitmap.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);

                var imageSource = new BitmapImage();
                imageSource.SetSource(stream.AsRandomAccessStream());
                QrCodeSign.Source = imageSource;
            }

            //var result = writer.Write(jsonSign);
            //var wb = result.ToBitmap() as WriteableBitmap;

        }
    }
}
