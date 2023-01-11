using Packets;

namespace GameServerCore
{
    public delegate Packet? HandleInvoke<T>(Session session, T packet) where T : Packet;

    public abstract class HandlePacket<T> where T : Packet
    {
        public abstract Packet? Invoke(Session session, T packet);
    }
}