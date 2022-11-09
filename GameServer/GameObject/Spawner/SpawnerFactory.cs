using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class SpawnerFactory : GameObjectFactory<Spawner>
    {
        private GameObjectManager _gameObjectManager;

        public SpawnerFactory(ILoggerFactory loggerFactory, GameObjectManager gameObjectManager) : base(loggerFactory)
        {
            _gameObjectManager = gameObjectManager;
        }

        public override Spawner Create()
        {
            return new Spawner(GetLogger(), _gameObjectManager);
        }
    }
}
