using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace TerminalApp.Models
{
    public class QRCodeScanner
    {
        //private PictureBox pictureBox;
        private System.Windows.Forms.Timer timer;
        private BarcodeReader barcodeReader;

        public event EventHandler<string> QRCodeScanned;

        public QRCodeScanner(Panel panel, PictureBox pictureBox)
        {
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            panel.Controls.Add(pictureBox);

            barcodeReader = new BarcodeReader();
            barcodeReader.Options = new DecodingOptions
            {
                PossibleFormats = new[] { BarcodeFormat.QR_CODE }
            };

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            Bitmap frame = GetVideoFrame();
            if (frame != null)
            {
                Result result = barcodeReader.Decode(frame);
                if (result != null)
                {
                    QRCodeScanned?.Invoke(this, result.Text);
                }
            }
        }

        public void StartScanning()
        {
            timer.Start();
            timer.Enabled = true;
        }

        public void StopScanning()
        {
            timer.Stop();
            timer.Enabled = false;
        }

        private Bitmap GetVideoFrame()
        {
            // Capture de la frame vidéo ici
            // Remplace cette ligne avec la logique de capture de la frame vidéo depuis ta source vidéo (par exemple, la caméra)
            // Assure-toi de retourner une instance de Bitmap représentant la frame vidéo capturée

            // Exemple de code factice pour générer une image de test
            int width = 640;
            int height = 480;
            Bitmap testImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(testImage))
            {
                g.FillRectangle(Brushes.White, 0, 0, width, height);
                g.DrawString("QRCode test", new Font("Arial", 16), Brushes.Black, new PointF(10, 10));
            }
            return testImage;
        }
    }
}
