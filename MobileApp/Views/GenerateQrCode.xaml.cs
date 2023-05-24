using QRCoder;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using MobileApp.Models;

namespace MobileApp.Views;

public partial class GenerateQrCode : ContentPage
{
	public GenerateQrCode()
	{
		InitializeComponent();

        testing();
    }

    public async void testing()
    {
        var network = GetMacAddress();
        var studentResult = await GetInformationsForGenerateQrCode(network.ToString());
        var create = GenerateQrCode2(studentResult.MacAdress);

        QrCodeImage.Source = create;
    }

    public static ImageSource GenerateQrCode2(string data)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);

        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        ImageSource qrImageSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

        return qrImageSource;
    }

    private async Task<Student> GetInformationsForGenerateQrCode(string macAddress)
    {
        Student student = new Student();

        using (var client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("http://10.0.2.2:5283/api/Students/GetByMacAddress/" + macAddress);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                student = JsonConvert.DeserializeObject<Student>(responseString);

                //return responseString;
                //return student;
            }

            return student;
        }
    }

    public static PhysicalAddress GetMacAddress()
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