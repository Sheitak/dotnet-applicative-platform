using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerminalApp.Models;
using ZXing;
using ZXing.Windows.Compatibility;

namespace TerminalApp.Forms
{
    public partial class ScannerForm : Form
    {
        private QRCodeScanner qrCodeScanner;

        public ScannerForm()
        {
            InitializeComponent();
            CenterToScreen();
            StartQRCodeScanning();
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public void StartQRCodeScanning()
        {
            qrCodeScanner = new QRCodeScanner(scannerPanel, scanPictureBox);
            qrCodeScanner.QRCodeScanned += QRCodeScanner_QRCodeScanned;
            qrCodeScanner.StartScanning();
        }

        public void StopQRCodeScanning()
        {
            qrCodeScanner.StopScanning();
            qrCodeScanner.QRCodeScanned -= QRCodeScanner_QRCodeScanned;
        }

        private void QRCodeScanner_QRCodeScanned(object sender, string qrCodeText)
        {
            MessageBox.Show("QRCode détecté : " + qrCodeText);
        }

        /*
        public static string ReadQrCode(byte[] qrCode)
        {
            BarcodeReader coreCompatReader = new BarcodeReader();

            using (Stream stream = new MemoryStream(qrCode))
            {
                using (var coreCompatImage = (Bitmap)Image.FromStream(stream))
                {
                    return coreCompatReader.Decode(coreCompatImage).Text;
                }
            }
        }
        */
    }
}
