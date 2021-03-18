
using System;
using System.Net;
using MoraES.Model;

namespace MoraES.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = Environment.GetEnvironmentVariable("SERVER_ADDRESS") ?? "moraes";
            var port = int.Parse(Environment.GetEnvironmentVariable("SERVER_PORT") ?? "2626");
            var dnsEndpoint = new DnsEndPoint(address, port);
            MoraESClient moraEsClient = new MoraESClient(, 2626);
            MoraLogger.Info("Connecting to MoraES Server...");
            moraEsClient.Connect();
            MoraLogger.Info("Connected to MoraES Server ✅");
            try
            {
                SendMessage(moraEsClient);
            }
            catch (System.Exception ex)
            {
                MoraLogger.Error(ex.Message);
            }
            while (true) { }
        }

        private static void SendMessage(MoraESClient moraEsClient)
        {
            MoraLogger.Info("Sending message...📧");
            if (moraEsClient.IsConnected)
            {
                var timestamp = (ulong)(DateTimeOffset.UtcNow).Add(new TimeSpan(0, 0, 10)).ToUnixTimeMilliseconds();
                MoraEvent moraEvent = new MoraEvent(timestamp, ContentTypeEnum.BINARY, new byte[4]);
                var bytes = moraEvent.ToByteArray();
                moraEsClient.Send(bytes, 0, bytes.Length);
                MoraLogger.Info("Sent! 📨");
                var response = moraEsClient.Receive(64);
                MoraLogger.Info(response);
            }
        }
    }
}
