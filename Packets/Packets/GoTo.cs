using System.Numerics;

namespace Packets
{
    public class GoTo : Packet
    {
        protected override PacketType packetType => PacketType.GoTo;

        public Vector3 Position { get; set; }

        protected internal override void Read(BinaryReader reader)
        {
            Position = reader.ReadVector3();
        }

        protected override void Write(BinaryWriter writer)
        {
            writer.Write(Position);
        }
    }
}