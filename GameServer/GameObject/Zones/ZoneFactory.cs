using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class ZoneFactory : GameObjectFactory<Zone>
    {
        private ILogger _logger;
        private EntityManager _entityManager;

        public ZoneFactory(ILogger<ZoneFactory> logger, ILoggerFactory loggerFactory, EntityManager entity) : base(loggerFactory)
        {
            _logger = logger;
            _entityManager = entity;
        }

        public override Zone Create()
        {
            return new Zone(GetLogger(), _entityManager);
        }
    }
}
