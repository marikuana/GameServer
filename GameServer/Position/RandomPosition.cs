using System.Numerics;

namespace GameServer
{
    public class RandomPosition : IPosition
    {
        public Vector3 Pos
        {
            get
            {
                Random random = new Random();
                return new Vector3(random.NextInt64(10), random.NextInt64(10), random.NextInt64(10));
            }
        }
    }
}