namespace Packets
{
    public class Ping : Packet
    {
        protected override PacketType packetType => PacketType.Ping;
    }
}