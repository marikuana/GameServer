using System.Numerics;

namespace GameServerCore
{

    public abstract class GameObjectBuilder<T> where T : GameObject
    {
        protected GameObjectFactory<T> _factory;

        protected Guid? _id;
        protected Vector3 _position;
        protected Vector3 _rotation;

        public GameObjectBuilder(GameObjectFactory<T> factory)
        {
            _factory = factory;
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

        public virtual T Build()
        {
            T gameObject = _factory.Create();

            gameObject.Id = Guid.NewGuid();
            gameObject.Position = _position;
            gameObject.Rotation = _rotation;

            return gameObject;
        }
    }
}
