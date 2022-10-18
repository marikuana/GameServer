using Packets;

namespace GameServerCore
{
    public abstract class HandlePacket<T> where T : Packet
    {
        public abstract void Invoke(T packet);
    }
}