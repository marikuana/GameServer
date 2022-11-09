using System.Numerics;

namespace GameServerCore
{
    public class EntityBuilder : GameObjectBuilder<Entity>
    {
        private IPosition? _targetPosition;

        public EntityBuilder(EntityFactory entityFactory) : base(entityFactory)
        {
        }

        public void SetTargerPosition(IPosition position)
        {
            _targetPosition = position;
        }

        public override Entity Build()
        {
            Entity entity = base.Build();

            entity.SetTargetPosition(_targetPosition);

            return entity;
        }
    }
}