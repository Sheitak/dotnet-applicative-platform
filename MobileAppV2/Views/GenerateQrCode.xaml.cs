using MobileAppV2.Services;
using Newtonsoft.Json;
using QRCoder;
using System.Net;
using System.Net.NetworkInformation;

namespace MobileAppV2.Views;

public partial class GenerateQrCode : ContentPage
{
    readonly DataSourceProvider dataSourceProvider = DataSourceProvider.GetInstance();

    public GenerateQrCode()
    {
        InitializeComponent();
        BindingContext = App.Student;

        SignatureLabel.Text = $"QrCode pour le : {DateTime.Now:dd/MM/yyyy} à {DateTime.Now:HH:mm:ss}";
    }

    private void GenerateQrCodeButton_Clicked(object sender, EventArgs e)
    {
        GenerateQrCodeWithMacAddress();
    }

    private async void GenerateQrCodeWithMacAddress()
    {
        // TODO : Test Variable, replace with real value
        var tempStudentId = App.Student.StudentID;
        var tempMacAddress = "82A70095380Z";

        // 82A70095380B
        var deviceMacAddress = GetMacAddress().ToString();

        var student = await dataSourceProvider.GetInformationsForGenerateQrCode(tempStudentId, tempMacAddress);
        
        if (student != null)
        {
            var jsonStudent = JsonConvert.SerializeObject(student);
            var encodedJson = WebUtility.UrlEncode(jsonStudent);

            var qrImageSource = CreateQrCode(encodedJson);

            MacAddress.Text = jsonStudent;
            QrCodeImage.Source = qrImageSource;
        }
    }

    private static ImageSource CreateQrCode(string data)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(WebUtility.UrlDecode(data), QRCodeGenerator.ECCLevel.L);

        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        ImageSource qrImageSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

        return qrImageSource;
    }

    private static PhysicalAddress GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
            nic.OperationalStatus == OperationalStatus.Up)
            {
                return nic.GetPhysicalAddress();
            }
        }
        return null;
    }
}