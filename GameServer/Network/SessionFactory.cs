using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class SessionFactory
    {
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        private PacketHandler _packetHandler;

        public SessionFactory(ILogger<SessionFactory> logger, PacketHandler packetHandler, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _packetHandler = packetHandler;
            _loggerFactory = loggerFactory;
        }

        public Session Create(Server server)
        {
            return new Session(_loggerFactory.CreateLogger<Session>(), server, _packetHandler);
        }
    }
}