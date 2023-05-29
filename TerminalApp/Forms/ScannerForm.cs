using AForge.Video;
using AForge.Video.DirectShow;
using Newtonsoft.Json;
using TerminalApp.Models;
using TerminalApp.Services;
using ZXing;
using ZXing.Windows.Compatibility;

namespace TerminalApp.Forms
{
    public partial class ScannerForm : Form
    {
        // https://www.youtube.com/watch?v=N2ioyWYt0AM

        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private DataSourceProvider DataSource = DataSourceProvider.GetInstance();
        private Student student;
        private Signature signature;

        public ScannerForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void ScannerForm_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);

            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();

            timerScan.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            scanPictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void ScannerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FinalFrame != null && FinalFrame.IsRunning)
            {
                FinalFrame.SignalToStop();
                FinalFrame.WaitForStop();
                FinalFrame = null;
            }
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result? result;

            try
            {
                result = barcodeReader.Decode((Bitmap)scanPictureBox.Image);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                return;
            }

            if (result != null)
            {
                string decoded = result.ToString().Trim();
                if (!string.IsNullOrEmpty(decoded))
                {
                    timerScan.Stop();
                    MessageBox.Show("QR code détecté : " + decoded, "QR code détecté");

                    student = JsonConvert.DeserializeObject<Student>(decoded);

                    if (student != null)
                    {
                        BuildSignatureFromStudent(student);
                        MessageBox.Show($"Votre émargement pour le : {DateTime.Now:dd/MM/yyyy} à {DateTime.Now:HH:mm:ss} a bien été enregistré !");
                    }

                    Close();
                }
            }
        }

        private async void BuildSignatureFromStudent(Student student)
        {
            try
            {
                signature = new Signature
                {
                    CreatedAt = DateTime.Now,
                    IsPresent = true,
                    StudentID = student.StudentID
                };

                await DataSource.CreateSignature(signature);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
