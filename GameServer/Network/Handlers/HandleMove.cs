using Packets;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class HandleMove : HandlePacket<GoTo>
    {
        private ILogger _logger;

        public HandleMove(ILogger<HandleMove> logger)
        {
            _logger = logger;
        }

        public override Packet? Invoke(Session session, GoTo packet)
        {
            _logger.LogDebug($"HandleMove: {packet.Position}");
            return null;
        }
    }
}