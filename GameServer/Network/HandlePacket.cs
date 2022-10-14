using Packets;

namespace GameServer
{
    public abstract class HandlePacket<T> where T : Packet
    {
        public abstract void Invoke(T packet);
    }
}