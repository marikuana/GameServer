using Packets;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class HandleRequest : HandlePacket<Request>
    {
        private ILogger _logger;
        private PacketHandler _packetHandler;
        private ResponcePacketFactory _responcePacketFactory;
        private NetworkManager _networkManager;

        public HandleRequest(ILogger<HandleRequest> logger, PacketHandler packetHandler, ResponcePacketFactory responcePacketFactory, NetworkManager networkManager)
        {
            _logger = logger;
            _packetHandler = packetHandler;
            _responcePacketFactory = responcePacketFactory;
            _networkManager = networkManager;

        }

        public override Packet? Invoke(Session session, Request request)
        {
            Packet packet = _packetHandler.Handle(session, request.Packet) ?? throw new Exception($"Request({request.Packet.GetType()}) not return answer packet");

            Response response = _responcePacketFactory.CreateFromRequest(request);
            response.Packet = packet;
            _networkManager.Send(session, response);
            return null;
        }
    }
}