using Microsoft.Extensions.Logging;
using Packets;

namespace GameServerCore
{
    public class HandlePing : HandlePacket<Ping>
    {
        private ILogger _logger;

        public HandlePing(ILogger<HandlePing> logger)
        {
            _logger = logger;
        }

        public override Packet? Invoke(Session session, Ping packet)
        {
            _logger.LogDebug("Ping");
            return packet;
        }
    }
}