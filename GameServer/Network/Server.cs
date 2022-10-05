using NetCoreServer;
using System.Net;
using Packets;

namespace GameServer
{
    public class Server : TcpServer
    {
        private ILogger _logger;

        public Server(ILogger logger, IPAddress address, int port) : base(address, port)
        {
            _logger = logger;
        }

        protected override TcpSession CreateSession()
        {
            return new Session(_logger, this);
        }

        public void Multicast(Packet packet)
        {
            Multicast(packet.GetBytes());
        }

        protected override void OnConnected(TcpSession session)
        {
            _logger.Log("Connected");
            base.OnConnected(session);
        }

        protected override void OnConnecting(TcpSession session)
        {
            _logger.Log("Connecting");
            base.OnConnecting(session);
        }

        protected override void OnDisconnected(TcpSession session)
        {
            _logger.Log("Disconnected");
            base.OnDisconnected(session);
        }

        protected override void OnDisconnecting(TcpSession session)
        {
            _logger.Log("Disconnecting");
            base.OnDisconnecting(session);
        }

        protected override void OnStarted()
        {
            _logger.Log("Started");
            base.OnStarted();
        }

        protected override void OnStarting()
        {
            _logger.Log("Starting");
            base.OnStarting();
        }

        protected override void OnStopped()
        {
            _logger.Log("Stopped");
            base.OnStopped();
        }

        protected override void OnStopping()
        {
            _logger.Log("Stopping");
            base.OnStopping();
        }
    }
}