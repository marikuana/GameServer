namespace GameServer
{
    public class SessionFactory
    {
        private ILogger _logger;
        private PacketHandler _packetHandler;

        public SessionFactory(ILogger logger, PacketHandler packetHandler)
        {
            _logger = logger;
            _packetHandler = packetHandler;
        }

        public Session Create(Server server)
        {
            return new Session(_logger, server, _packetHandler);
        }
    }
}