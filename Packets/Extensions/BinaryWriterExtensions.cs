using System.Numerics;

namespace Packets
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, Vector3 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
            writer.Write(vector.Z);
        }
    }
}