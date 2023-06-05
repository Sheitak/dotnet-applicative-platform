using System.Net;
using System.Net.Sockets;

namespace TerminalApp.Services
{
    internal class TerminalServer
    {
        private TerminalServer() { }

        private static TerminalServer? _instance;
        private TcpListener listener;
        private bool isDesktopActive;

        public static TerminalServer GetInstance()
        {
            _instance ??= new TerminalServer();
            return _instance;
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, 8787);
            listener.Start();

            Console.WriteLine("Serveur démarré. En attente de connexions...");

            Task.Run(async () =>
            {
                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    ProcessClient(client);
                }
            });
        }

        private void ProcessClient(TcpClient client)
        {
            // Working Client
            isDesktopActive = true;
        }

        public bool IsDesktopActive()
        {
            return isDesktopActive;
        }
    }
}
