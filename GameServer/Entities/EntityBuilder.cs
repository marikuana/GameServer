using System.Numerics;

namespace GameServerCore
{
    public class EntityBuilder : GameObjectBuilder<Entity>
    {
        public EntityBuilder(Entity gameObject) : base(gameObject)
        {
        }

        public override Entity Build()
        {
            GameObject = base.Build();
            
            return GameObject;
        }
    }
}