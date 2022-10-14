using Packets;

namespace GameServer
{
    public class HandleMove : HandlePacket<GoTo>
    {
        private ILogger _logger;

        public HandleMove(ILogger logger)
        {
            _logger = logger;
        }

        public override void Invoke(GoTo packet)
        {
            _logger.Log($"HandleMove: {packet.Position}");
        }
    }
}