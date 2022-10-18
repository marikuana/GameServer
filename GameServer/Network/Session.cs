using NetCoreServer;
using Packets;
using System.Text;

namespace GameServerCore
{

    public class Session : TcpSession
    {
        private ILogger _logger;
        private PacketHandler _packetHandler;

        public Session(ILogger logger, TcpServer server, PacketHandler packetHandler) : base(server)
        {
            _logger = logger;
            _packetHandler = packetHandler;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            _packetHandler.Handle(buffer);
            base.OnReceived(buffer, offset, size);
        }

        public void Send(Packet packet)
        {
            Send(packet.GetBytes());
        }
    }
}