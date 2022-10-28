using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class EntityFactory : GameObjectFactory<Entity>
    {
        private ILogger _logger;

        public EntityFactory(ILogger<EntityFactory> logger, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = logger;
        }

        public override Entity Create() =>
            new(GetLogger());
    }
}