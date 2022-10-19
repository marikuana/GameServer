using System.Numerics;

namespace GameServerCore
{
    public abstract class GameObjectBuilder<T> where T : GameObject
    {
        protected Guid _id;
        protected Vector3 _position;
        protected Vector3 _rotation;

        public GameObjectBuilder()
        {
            _id = Guid.NewGuid();
        }

        public GameObjectBuilder<T> SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public GameObjectBuilder<T> SetRotation(Vector3 rotation)
        {
            _rotation = rotation;
            return this;
        }

        public abstract T Build();
    }
}
