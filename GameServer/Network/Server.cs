using NetCoreServer;
using System.Net;
using Packets;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Server : TcpServer
    {
        private ILogger _logger;
        private SessionFactory _sessionFactory;

        public event Action<Session>? Connecting;
        public event Action<Session>? Disconnecting;

        public Server(ILogger<Server> logger, SessionFactory sessionFactory, Configuration configuration)
            : base(configuration.ListenerIP, configuration.ListenerPort)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;
        }

        protected override TcpSession CreateSession()
        {
            return _sessionFactory.Create(this);
        }

        public void Multicast(Packet packet)
        {
            lock (this)
            {
                toSend.Enqueue(packet);
            }
        }

        private Queue<Packet> toSend = new Queue<Packet>();

        protected override void OnConnected(TcpSession session)
        {
            _logger.LogDebug("Connected");
            base.OnConnected(session);
        }

        protected override void OnConnecting(TcpSession session)
        {
            _logger.LogDebug("Connecting");
            Connecting?.Invoke((Session)session);
            base.OnConnecting(session);
        }

        protected override void OnDisconnected(TcpSession session)
        {
            _logger.LogDebug("Disconnected");
            base.OnDisconnected(session);
        }

        protected override void OnDisconnecting(TcpSession session)
        {
            _logger.LogDebug("Disconnecting");
            Disconnecting?.Invoke((Session)session);
            base.OnDisconnecting(session);
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("Started");
            base.OnStarted();
        }

        protected override void OnStarting()
        {
            _logger.LogInformation($"Starting on port: {Port}");
            base.OnStarting();
        }

        protected override void OnStopped()
        {
            _logger.LogDebug("Stopped");
            base.OnStopped();
        }

        protected override void OnStopping()
        {
            _logger.LogDebug("Stopping");
            base.OnStopping();
        }
    }
}