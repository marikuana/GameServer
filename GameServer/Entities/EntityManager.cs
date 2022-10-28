using System.Numerics;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class EntityManager : IUpdate
    {
        private List<Entity> _entities;
        private ILogger<EntityManager> _logger;
        private EntityFactory _entityFactory;

        public EntityManager(ILogger<EntityManager> logger, EntityFactory entityFactory)
        {
            _entities = new List<Entity>();
            _logger = logger;
            _entityFactory = entityFactory;
        }

        private List<Entity> entitiesToAdd = new List<Entity>();

        public Entity CreateEntity(Action<EntityBuilder> action)
        {
            Entity entity = _entityFactory.Create();

            EntityBuilder entityBuilder = new EntityBuilder(entity);
            action(entityBuilder);
            entity = entityBuilder.Build();

            entitiesToAdd.Add(entity);
            _logger.LogDebug($"CreateEntity: {entity.Position}");
            return entity;
        }

        public void Update(TimeSpan time)
        {
            foreach (var entity in _entities.ToList())
            {
                entity.Update(time);
            }
            _entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();
        }
    }
}