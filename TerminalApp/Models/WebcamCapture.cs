using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing.Windows.Compatibility;
using ZXing.Common;
using System.Threading.Tasks.Dataflow;

namespace TerminalApp.Models
{
    internal class WebcamCapture
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private PictureBox pictureBox;
        private BarcodeReader barcodeReader;
        private bool isCapturing;
        private object lockObject = new object();

        public event Action<Result> QrCodeDetected;

        public WebcamCapture()
        {
            barcodeReader = new BarcodeReader();
            barcodeReader.Options = new DecodingOptions
            {
                TryHarder = true
            };

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
        }

        public void Start(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            isCapturing = true;
            videoSource.Start();
        }

        public void Stop()
        {
            isCapturing = false;

            lock (lockObject)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isCapturing)
            {
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                lock (lockObject)
                {
                    pictureBox.Invoke(new Action(() =>
                    {
                        pictureBox.Image = bitmap;
                    }));
                }

                var result = barcodeReader.Decode(bitmap);

                if (result != null)
                {
                    QrCodeDetected?.Invoke(result);
                    Stop();
                }


            }
        }
    }
}
