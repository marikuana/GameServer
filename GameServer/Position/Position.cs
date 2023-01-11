using System.Numerics;

namespace GameServerCore
{
    public class Position : IPosition
    {
        public Vector3 Pos { get; private set; }

        public Position(Vector3 pos)
        {
            Pos = pos;
        }

        public Position(float x, float y, float z)
            : this(new Vector3(x, y, z))
        {

        }
    }
}