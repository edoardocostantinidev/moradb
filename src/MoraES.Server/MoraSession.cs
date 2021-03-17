using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetCoreServer;

namespace MoraES.Network
{

    internal class MoraSession : TcpSession
    {
        public MoraSession(TcpServer server) : base(server)
        {
        }

        protected override void OnConnected()
        {
            MoraLogger.Info($"MoraSession initiated with id: {this.Id}");
        }

        protected override void OnDisconnected()
        {
            MoraLogger.Info($"MoraSession terminated with id: {this.Id}");
        }

        protected override void OnError(SocketError error)
        {
            base.OnError(error);
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            MoraLogger.Info($"Received: {message}");
            this.Send($"OK {this.Id}!");
        }

    }
    internal class MoraServer : TcpServer
    {
        public MoraServer(IPAddress address, int port) : base(address, port) { }

        protected override void OnStarted()
        {
            base.OnStarted();
            MoraLogger.Info("MoraES started! ⏰");
            Task.Run(async delegate
            {
                for (; ; )
                {
                    await Task.Delay(10000);
                    MoraLogger.Info("OK✅");
                }
            });
        }
        protected override TcpSession CreateSession()
        {
            return new MoraSession(this);
        }
        protected override void OnError(SocketError error)
        {
            MoraLogger.Error($"Socket Error: {error}");
        }
    }

}

