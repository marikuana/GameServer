using GameServerCore;

namespace GameServerCore.Api
{
    public class Api
    {
        private GameObjectManager _gameObjectManager;
        private Server _server;

        public Api(GameObjectManager gameObjectManager, Server server)
        {
            _gameObjectManager = gameObjectManager;
            _server = server;
        }

        public void StartSend()
        {
            Task.Run(Sending);
            foreach (var gameObject in _gameObjectManager.GetObjects().ToList())
            {
                gameObject.OnDestroy += SendDestroy;
            }
            _gameObjectManager.OnAddGameObject += (gameObject) =>
            {
                gameObject.OnDestroy += SendDestroy;
            };
        }

        public void Sending()
        {
            while (true)
            {
                foreach (var gameObject in _gameObjectManager.GetObjects().ToList())
                {
                    _server.Multicast(new Packets.GoTo() { EntityId = gameObject.Id, Position = gameObject.Position });
                }
                Task.Delay(1000 / 60).Wait();
            }
        }

        public void SendDestroy(GameObject gameObject)
        {
            _server.Multicast(new Packets.Destroy() { EntityId = gameObject.Id });
        }
    }
}