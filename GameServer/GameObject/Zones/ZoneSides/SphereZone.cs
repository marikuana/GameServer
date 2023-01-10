using System.Numerics;

namespace GameServerCore
{
    public class SphereZone : ZoneSize
    {
        private float _radius;

        public SphereZone(Zone zone, float radius) : base(zone)
        {
            _radius = radius;
        }

        public override bool InZone(Vector3 position)
        {
            return Vector3.Distance(_zone.Position, position) <= _radius;
        }
    }
}
