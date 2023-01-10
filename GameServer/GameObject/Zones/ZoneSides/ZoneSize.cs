using System.Numerics;

namespace GameServerCore
{
    public abstract class ZoneSize
    {
        protected Zone _zone;

        public ZoneSize(Zone zone)
        {
            _zone = zone;
        }

        public bool InZone(IPosition position)
        {
            return InZone(position.Pos);
        }

        public abstract bool InZone(Vector3 position);
    }
}
