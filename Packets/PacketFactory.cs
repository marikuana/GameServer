using System.Reflection;

namespace Packets
{
    public class PacketFactory
    {
        public Packet GetPacket(byte[] buffer)
        {
            var reader = new BinaryReader(new MemoryStream(buffer));
            
            PacketType packetType = (PacketType)reader.ReadUInt16();

            Packet packet = packetType switch
            {
                PacketType.Ping => new Ping(),
                PacketType.GoTo => new GoTo(),
                _ => throw new ArgumentException(),
            };
            packet.Read(reader);
            return packet;
        }
    }
}