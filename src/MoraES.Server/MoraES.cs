using System;
using System.Net;
using MoraES.Network;

namespace MoraES
{

    internal class MoraES
    {
        private int port;

        internal void Start()
        {
            MoraLogger.Info("MoraES starting up...🚀");
            this.Configure();
            MoraServer moraServer = new MoraServer(IPAddress.Any, port);
            moraServer.Start();
            
            MoraLogger.Info("Press a key to exit");
            System.Console.ReadLine();
        }

        private void Configure()
        {
            try
            {
                port = int.Parse(Environment.GetEnvironmentVariable("PORT"));
            }
            catch
            {
                MoraLogger.Warning($"PORT environment variable was not set, defaulting to {Constants.DEFAULT_PORT}...");
                port = Constants.DEFAULT_PORT;
            }
            MoraLogger.Info($"MoraES configured, will listend to port {port}...🤞");
        }
    }
}