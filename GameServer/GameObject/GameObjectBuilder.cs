using System.Numerics;

namespace GameServerCore
{

    public abstract class GameObjectBuilder<T> where T : GameObject
    {
        protected T GameObject;

        protected Guid _id;
        protected Vector3 _position;
        protected Vector3 _rotation;

        public GameObjectBuilder(T gameObject)
        {
            _id = Guid.NewGuid();
            GameObject = gameObject;

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
            GameObject.Id = _id;
            GameObject.Position = _position;
            GameObject.Rotation = _rotation;

            return GameObject;
        }
    }
}
