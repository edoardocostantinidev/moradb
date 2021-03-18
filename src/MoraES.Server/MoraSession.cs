using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MoraES.Model;
using NetCoreServer;

namespace MoraES.Network
{

    public class MoraSession : TcpSession
    {
        private readonly MoraEventHandler _moraEventHandler;
        public MoraSession(TcpServer server, MoraEventHandler moraEventHandler) : base(server)
        {
            _moraEventHandler = moraEventHandler;
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
            var moraEvent = ConvertBytesToMoraEvent(buffer, size);
            MoraLogger.Info($"Received event: timestamp {moraEvent.timestamp}, contentType {moraEvent.contentType}");
            _moraEventHandler.Schedule(moraEvent);
            this.Send($"OK {this.Id}!");
        }
        private MoraEvent ConvertBytesToMoraEvent(byte[] buffer, long size)
        {
            var timestamp = BitConverter.ToUInt64(buffer, 0);
            var contentType = (ContentTypeEnum)BitConverter.ToUInt16(buffer, sizeof(ulong));
            var headerLength = sizeof(ulong) - sizeof(ushort);
            var payloadLength = size - headerLength;
            var payload = new byte[payloadLength];
            Array.Copy(buffer, headerLength, payload, 0, payloadLength);
            return new MoraEvent(timestamp, contentType, payload);

        }
    }
    internal class MoraServer : TcpServer
    {
        private readonly MoraEventHandler _moraEventHandler;
        public MoraServer(MoraEventHandler moraEventHandler, IPAddress address, int port) : base(address, port)
        {
            _moraEventHandler = moraEventHandler;
        }

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
            return new MoraSession(this, this._moraEventHandler);
        }
        protected override void OnError(SocketError error)
        {
            MoraLogger.Error($"Socket Error: {error}");
        }
    }

}

