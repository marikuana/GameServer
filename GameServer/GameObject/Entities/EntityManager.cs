using System.Numerics;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class EntityManager
    {
        private ILogger<EntityManager> _logger;
        private EntityFactory _entityFactory;
        private GameObjectManager _gameObjectManager;

        public EntityManager(ILogger<EntityManager> logger, EntityFactory entityFactory, GameObjectManager gameObjectManager)
        {
            _logger = logger;
            _entityFactory = entityFactory;
            _gameObjectManager = gameObjectManager;
        }

        public Entity CreateEntity(Action<EntityBuilder> action)
        {
            Entity entity = _entityFactory.Create();

            EntityBuilder entityBuilder = new EntityBuilder(entity);
            action(entityBuilder);
            entity = entityBuilder.Build();

            _gameObjectManager.AddGameObject(entity);
            
            _logger.LogDebug($"CreateEntity: {entity.Position}");
            return entity;
        }
    }
}