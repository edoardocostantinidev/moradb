

using System.Net;
using MoraES;
using NetCoreServer;

class MoraESClient : TcpClient
{
    public MoraESClient(IPAddress address, int port) : base(address, port) { }
    protected override void OnConnected()
    {
        MoraLogger.Info($"Connected to MoraES Server with Id: {Id}");
        base.OnConnected();
    }
}