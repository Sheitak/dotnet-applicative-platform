using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace TerminalApp.Forms
{
    public partial class QrCodeForm : Form
    {
        public QrCodeForm()
        {
            InitializeComponent();
            CenterToScreen();

            GenerateQRCodeWithDate(QrCodePanel);
        }

        /*
        public void GenerateQRCodeWithDate(Panel panel)
        {
            // Obtiens la date du jour
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            // Crée un générateur QRCode
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(currentDate, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Convertit le QRCode en une image bitmap
            Bitmap qrCodeImage = qrCode.GetGraphic(10);

            // Crée un contrôle PictureBox pour afficher l'image du QRCode
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = qrCodeImage;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            // Efface tous les contrôles existants dans le panel
            panel.Controls.Clear();

            // Ajoute le PictureBox contenant le QRCode au panel
            panel.Controls.Add(pictureBox);
        }
        */

        public void GenerateQRCodeWithDate(Panel panel)
        {
            // https://stackoverflow.com/questions/52620795/zxing-qrcode-renderer-exception-with-net-core-2-1
            string currentDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            // Crée le contenu du QRCode
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 350,
                    Width = 350
                }
            };

            Bitmap qrCodeImage = writer.Write(currentDate);

            // Crée un contrôle PictureBox pour afficher l'image du QRCode
            PictureBox pictureBox = new PictureBox
            {
                Image = qrCodeImage,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            // Efface tous les contrôles existants dans le panel
            panel.Controls.Clear();

            // Ajoute le PictureBox contenant le QRCode au panel
            panel.Controls.Add(pictureBox);
        }

        private void QrCodeBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
