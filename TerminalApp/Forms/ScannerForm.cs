using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Windows.Compatibility;

namespace TerminalApp.Forms
{
    public partial class ScannerForm : Form
    {
        public ScannerForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

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

        private void scanBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
