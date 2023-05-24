using AForge.Video;
using AForge.Video.DirectShow;
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
        // https://www.youtube.com/watch?v=N2ioyWYt0AM
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        public ScannerForm()
        {
            InitializeComponent();
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
            try
            {
                Result result = barcodeReader.Decode((Bitmap)scanPictureBox.Image);
                string decoded = "";

                if (result != null)
                {
                    decoded = result.ToString().Trim();
                }
                if (decoded != "")
                {
                    timerScan.Stop();
                    MessageBox.Show("QR code détecté : " + decoded, "QR code détecté");
                    Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
