namespace Packets
{
    public abstract class Packet
    {
        protected abstract PacketType packetType { get; }

        protected internal virtual void Read(BinaryReader reader) { }
        protected virtual void Write(BinaryWriter writer) { }

        public byte[] GetBytes()
        {
            var memoryStream = new MemoryStream();
            var writer = new BinaryWriter(memoryStream);

            writer.Write((ushort)packetType);
            Write(writer);

            return memoryStream.ToArray();
        }
    }
}