namespace GameServerCore
{
    public abstract class GameObjectFactory<Builder, Object> where Builder : GameObjectBuilder<Object> where Object : GameObject
    {
        public abstract Object Create(Action<Builder> action);
    }
}
