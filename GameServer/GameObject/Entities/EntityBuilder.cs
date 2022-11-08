using System.Numerics;

namespace GameServerCore
{
    public class EntityBuilder : GameObjectBuilder<Entity>
    {
        private IPosition? _targetPosition;

        public EntityBuilder(Entity gameObject) : base(gameObject)
        {
        }

        public void SetTargerPosition(IPosition position)
        {
            _targetPosition = position;
        }

        public override Entity Build()
        {
            base.Build();

            GameObject.SetTargetPosition(_targetPosition);

            return GameObject;
        }
    }
}