using System.Reflection;

namespace GameServerCore
{
    public class ZoneBuilder : GameObjectBuilder<Zone>
    {
        private ZoneSideFactory _sideFactory;
        private Type _zoneSideType;
        private object[] parameters;

        delegate Action ZoneSizeAction<T>(T zoneSize) where T : ZoneSize;

        public ZoneBuilder(GameObjectFactory<Zone> factory, ZoneSideFactory zoneSideFactory) : base(factory)
        {
            _sideFactory = zoneSideFactory;
        }

        public void SphereZone(float radius)
        {
            _zoneSideType = typeof(SphereZone);
            parameters = new object[] { radius };
        }

        private ZoneSize GetZoneSize(Zone zone)
        {
            ZoneSize zoneSize = _sideFactory.Create(_zoneSideType, zone, parameters);
            return zoneSize;
        }

        public override Zone Build()
        {
            Zone zone = base.Build();

            zone.ZoneSize = GetZoneSize(zone);
            
            return zone;
        }
    }

    public class ZoneSideFactory
    {
        public T Create<T>(Zone zone, Action<T> action) where T : ZoneSize
        {
            T zoneSize = (T)Create(typeof(T), zone);
            action?.Invoke(zoneSize);
            return zoneSize;
        }

        public ZoneSize Create(Type zoneSizeType, Zone zone, params object[] parameters)
        {
            ConstructorInfo constructor = zoneSizeType.GetConstructors().First();
            var list = new List<object>() { zone };
            list.AddRange(parameters);
            ZoneSize? zoneSize = (ZoneSize?)constructor.Invoke(list.ToArray());
            if (zoneSize == null)
                throw new ArgumentException();
            return zoneSize;
        }
    }
}
