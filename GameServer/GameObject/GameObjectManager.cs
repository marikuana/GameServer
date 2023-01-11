using System.Numerics;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class GameObjectManager : IUpdate
    {
        private List<GameObject> _gameObjects;
        private ILogger _logger;

        private object _lockAdd = new object();
        private List<GameObject> gameObjectsToAdd;
        private object _lockRemove = new object();
        private List<GameObject> gameObjectsToRemove;

        public event Action<GameObject>? OnAddGameObject;

        public GameObjectManager(ILogger<GameObjectManager> logger)
        {
            _gameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            _logger = logger;
        }

        public void AddGameObject(GameObject gameObject)
        {
            lock (_lockAdd)
            {
                gameObjectsToAdd.Add(gameObject);
            }
            OnAddGameObject?.Invoke(gameObject);
            gameObject.OnDestroy += RemoveGameObject;
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            lock (_lockRemove)
            {
                gameObjectsToRemove.Add(gameObject);
            }
        }

        public void Update(TimeSpan time)
        {
            foreach (var gameObject in _gameObjects.ToList())
            {
                gameObject.Update(time);
            }
            lock (_lockRemove)
            {
                foreach (var gameObject in gameObjectsToRemove)
                    _gameObjects.Remove(gameObject);
                gameObjectsToRemove.Clear();
            }
            lock (_lockAdd)
            {
                _gameObjects.AddRange(gameObjectsToAdd);
                gameObjectsToAdd.Clear();
            }
        }

        public IEnumerable<GameObject> GetObjects()
        {
            return GetObjects<GameObject>();
        }

        public IEnumerable<GameObject> GetObjects(Vector3 position, float radius)
        {
            return GetObjects()
                .Where(x => Vector3.Distance(position, x.Position) <= radius);
        }

        public IEnumerable<T> GetObjects<T>() where T : GameObject
        {
            return _gameObjects
                .Where(x => x is T)
                .Select(x => (T)x);
        }

        public IEnumerable<T> GetObjects<T>(Vector3 position, float radius) where T : GameObject
        {
            return GetObjects<T>()
                .Where(x => Vector3.Distance(position, x.Position) <= radius);
        }
    }
}