using System.Numerics;

namespace GameServerCore
{
    public class EntityManager : IUpdate
    {
        private IList<Entity> _entities;
        private ILogger _logger;
        private EntityFactory _entityFactory;

        public EntityManager(ILogger logger, EntityFactory entityFactory)
        {
            _entities = new List<Entity>();
            _logger = logger;
            _entityFactory = entityFactory;
        }

        public Entity CreateEntity()
        {
            return CreateEntity(new Vector3());
        }

        public Entity CreateEntity(Vector3 pos)
        {
            Entity entity = _entityFactory.GetEntity();
            entity.Position = pos;

            _entities.Add(entity);
            _logger.Log($"CreateEntity: {pos}");
            return entity;
        }

        public void Update(TimeSpan time)
        {
            foreach (var entity in _entities.ToList())
            {
                entity.Update(time);
            }
        }
    }
}