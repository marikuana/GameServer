using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore
{
    public class Zone : GameObject
    {
        public ZoneSize ZoneSize;
        private EntityManager _entityManager;

        private IList<Entity> _entities;

        public event Action<Entity>? OnEntityEnter;
        public event Action<Entity>? OnEntityLeave;

        public Zone(ILogger logger, EntityManager entityManager) : base(logger)
        {
            _entityManager = entityManager;
            _entities = new List<Entity>();
        }

        protected internal override void Update(TimeSpan time)
        {
            base.Update(time);

            foreach (var entity in _entityManager.GetEntities())
            {
                if (ZoneSize.InZone(entity))
                {
                    if (!_entities.Contains(entity))
                    {
                        EntityEnter(entity);
                    }
                }
                else
                {
                    if (_entities.Contains(entity))
                    {
                        EntityLeave(entity);
                    }
                }
            }
        }

        private void EntityEnter(Entity entity)
        {
            _entities.Add(entity);
            OnEntityEnter?.Invoke(entity);
            _logger.LogDebug($"Entity enter: {entity.Id}");
        }

        private void EntityLeave(Entity entity)
        {
            _entities.Remove(entity);
            OnEntityLeave?.Invoke(entity);
            _logger.LogDebug($"Entity leave: {entity.Id}");
        }

        public override void Destroy()
        {
            base.Destroy();

            foreach (var entity in _entities)
            {
                EntityLeave(entity);
            }
        }
    }
}
