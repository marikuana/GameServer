using Packets;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class HandleBatch : HandlePacket<Batch>
    {
        private ILogger _logger;
        private PacketHandler _packetHandler;

        public HandleBatch(ILogger<HandleBatch> logger, PacketHandler packetHandler)
        {
            _logger = logger;
            _packetHandler = packetHandler;
        }

        public override void Invoke(Batch batchPacket)
        {
            foreach (var packet in batchPacket.Packets)
            {
                _packetHandler.Handle(packet);
            }
        }
    }
}