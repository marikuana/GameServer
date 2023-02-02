using Packets;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class HandleMove : HandlePacket<GoTo>
    {
        private ILogger _logger;
        private Api.Api _api;

        public HandleMove(ILogger<HandleMove> logger, Api.Api api)
        {
            _logger = logger;
            _api = api;
        }

        public override Packet? Invoke(Session session, GoTo packet)
        {
            _logger.LogDebug($"HandleMove: {packet.Position}");
            _api.Move(session, packet.Position);
            return null;
        }
    }
}