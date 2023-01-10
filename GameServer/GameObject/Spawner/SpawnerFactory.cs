using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class SpawnerFactory : GameObjectFactory<Spawner>
    {
        private GameObjectManager _gameObjectManager;
        private EntityBuilder _entityBuilder;

        public SpawnerFactory(ILoggerFactory loggerFactory, GameObjectManager gameObjectManager, EntityBuilder entityBuilder) : base(loggerFactory)
        {
            _gameObjectManager = gameObjectManager;
            _entityBuilder = entityBuilder;
        }

        public override Spawner Create()
        {
            return new Spawner(GetLogger(), _gameObjectManager, _entityBuilder);
        }
    }
}
