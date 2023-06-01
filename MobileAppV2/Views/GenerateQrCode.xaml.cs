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
        BindingContext = App.student;

        SignatureLabel.Text = "Votre QrCode pour le : " + DateTime.Now.ToString("dd/MM/yyyy") + " à " + DateTime.Now.ToString("HH:mm:ss");
    }

    private void GenerateQrCodeButton_Clicked(object sender, EventArgs e)
    {
        GenerateQrCodeWithMacAddress();
    }

    private async void GenerateQrCodeWithMacAddress()
    {
        var tempStudentId = 1;
        var tempMacAddress = "82A70095380Z";
        // 82A70095380B
        var networkInformations = GetMacAddress();

        var student = await dataSourceProvider.GetInformationsForGenerateQrCode(tempStudentId, networkInformations.ToString());
        var jsonStudent = JsonConvert.SerializeObject(student);
        // "{\"StudentID\":1,\"Firstname\":\"Carson\",\"Lastname\":\"Alexander\",\"IsActive\":true,\"MacAdress\":\"82A70095380B\"}"
        var encodedJson = WebUtility.UrlEncode(jsonStudent);

        var qrImageSource = CreateQrCode(encodedJson);

        MacAddress.Text = jsonStudent;
        QrCodeImage.Source = qrImageSource;


        /* TODO: Mettre en place l'aspect visuel des retours pour UX
        } else if (student.MacAdress == networkInformations.ToString() && student.IsActive == false)
            {
                await DisplayAlert(
                    "Appareil Inactif", 
                    "Cet appareil n'est pas encore activé pour votre compte. Souhaitez-vous faire une demande d'activation ?", 
                    "Oui"
                );
            } else
            {
                await DisplayAlert(
                    "Appareil non-valide",
                    "Cet appareil ne correspond pas à votre appareil de référence. Veuillez vous rapprocher de la vie étudiante pour toutes modifications.",
                    "Ok"
                );
            }
        */
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