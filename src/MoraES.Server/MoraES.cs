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
            MoraEventHandler moraEventHandler = new MoraEventHandler();
            MoraServer moraServer = new MoraServer(moraEventHandler, IPAddress.Any, port);
            moraEventHandler.Start();
            moraServer.Start();
            while(true){}
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