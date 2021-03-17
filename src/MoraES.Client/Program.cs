
using System.Net;
using System.Threading.Tasks;

namespace MoraES.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MoraESClient moraEsClient = new MoraESClient(IPAddress.Loopback, 2626);
            MoraLogger.Info("Connecting to MoraES Server...");
            moraEsClient.Connect();
            MoraLogger.Info("Connected to MoraES Server ✅");
            Task.Run(async delegate
            {
                for (; ; )
                {
                    await Task.Delay(5000);
                    MoraLogger.Info("Sending message...📧");
                    if (moraEsClient.IsConnected)
                    {
                        moraEsClient.Send("Test");
                        MoraLogger.Info("Sent! 📨");
                        var response = moraEsClient.Receive(64);
                        MoraLogger.Info(response);
                    }

                }
            });
            MoraLogger.Info("Press a key to exit");
            System.Console.ReadLine();

        }
    }
}
