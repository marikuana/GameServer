using Packets;

namespace GameServer
{
    public class HandlePing : HandlePacket<Ping>
    {
        public HandlePing()
        {

        }
        public override void Invoke(Ping packet)
        {

        }
    }
}