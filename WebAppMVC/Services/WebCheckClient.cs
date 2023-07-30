using System.Net.Sockets;
using System.Text;

namespace WebAppMVC.Services
{
    public class WebCheckClient
    {
        private WebCheckClient() { }

        private static WebCheckClient? _instance;

        public static WebCheckClient GetInstance()
        {
            _instance ??= new WebCheckClient();
            return _instance;
        }

        public bool CheckTerminalStatus()
        {
            try
            {
                using TcpClient client = new TcpClient("ROG-STRIX-SCAR-II-T8QPGR2B", 8787); // Remplacez ici l'adresse IP ou le nom d'hôte de la machine où s'exécute l'application.
                byte[] data = Encoding.ASCII.GetBytes("ping");
                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}
