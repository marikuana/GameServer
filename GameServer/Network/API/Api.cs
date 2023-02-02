using GameServerCore;
using System.Numerics;
using GameServerCore.Map;

namespace GameServerCore.Api
{
    public class Api
    {
        private GameObjectManager _gameObjectManager;
        private EntityManager _entityManager;
        private NetworkManager _networkManager;

        public Api(GameObjectManager gameObjectManager, NetworkManager networkManager, EntityManager entityManager)
        {
            _gameObjectManager = gameObjectManager;
            _networkManager = networkManager;
            _entityManager = entityManager;

            _networkManager.OnConnecting += Connect;
            _networkManager.OnDisconnecting += Disconnect;
        }

        public void StartSend()
        {
            Task.Run(Sending);
            foreach (var gameObject in _gameObjectManager.GetObjects().ToList())
            {
                RegisterGameObject(gameObject);
            }
            _gameObjectManager.OnAddGameObject += gameObject => RegisterGameObject(gameObject);
        }

        private void RegisterGameObject(GameObject gameObject)
        {
            gameObject.OnDestroy += SendDestroy;
            gameObject.OnChangePosition += SendPosition;
        }

        public void Sending()
        {
            while (true)
            {
                var list = new List<GameObject>();
                lock (this)
                {
                    list = sendNewPos.ToList();
                    sendNewPos.Clear();
                }

                foreach (var gameObject in list)
                {
                    _networkManager.Send(new Packets.GoTo() { EntityId = gameObject.Id, Position = gameObject.Position });
                }
                Task.Delay(millisecondsDealy).Wait();
            }
        }

        private List<GameObject> sendNewPos = new List<GameObject>();
        private int millisecondsDealy = 1000 / 60;

        public void SendPosition(GameObject gameObject)
        {
            lock (this)
            {
                if (!sendNewPos.Contains(gameObject))
                    sendNewPos.Add(gameObject);
            }
        }

        public void SendDestroy(GameObject gameObject)
        {
            _networkManager.Send(new Packets.Destroy() { EntityId = gameObject.Id });
        }

        public void SendAllGameObjects(Session session)
        {
            foreach (var gameObject in _gameObjectManager.GetObjects().ToList())
            {
                _networkManager.Send(session, new Packets.GoTo() { EntityId = gameObject.Id, Position = gameObject.Position });
            }
        }

        private Dictionary<Session, Entity> characters = new Dictionary<Session, Entity>();

        private void Connect(Session session)
        {
            SendAllGameObjects(session);
            Entity entity = _entityManager.CreateEntity();
            characters.Add(session, entity);
        }

        private void Disconnect(Session session)
        {
            if (characters.ContainsKey(session))
            {
                characters[session].Destroy();
                characters.Remove(session);
            }
        }

        public void Move(Session session, Vector3 pos)
        {
            if (characters.ContainsKey(session))
                characters[session].SetTargetPosition(new Position(pos));
        }
    }
}