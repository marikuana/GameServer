using NetCoreServer;
using Packets;
using System.Text;

namespace GameServer
{

    public class Session : TcpSession
    {
        private ILogger _logger;

        public Session(ILogger logger, TcpServer server) : base(server)
        {
            _logger = logger;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            base.OnReceived(buffer, offset, size);
        }

        public void Send(Packet packet)
        {
            Send(packet.GetBytes());
        }
    }
}