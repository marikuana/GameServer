using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Spawner : GameObject
    {
        private EntityFactory? _entityFactory;
        public SpawnParameters SpawnParameters { get; private set; }

        private float _timeToNextSpawn;

        private GameObjectManager _gameObjectManager;

        public Spawner(ILogger<Spawner> logger, GameObjectManager gameObjectManager) : base(logger)
        {
            _gameObjectManager = gameObjectManager;
        }

        public void SetSpawnPamameters(SpawnParameters spawnParameters)
        {
            SpawnParameters = spawnParameters;
        }

        public void SetEntityFactory(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        protected internal override void Update(TimeSpan time)
        {
            base.Update(time);

            if (_timeToNextSpawn > SpawnParameters.SpawnInterval)
            {
                _timeToNextSpawn -= SpawnParameters.SpawnInterval;
                Spawn();
            }
            _timeToNextSpawn += (float)time.TotalMilliseconds;
        }

        private void Spawn()
        {
            if (_entityFactory == null)
                return;

            EntityBuilder entityBuilder = new EntityBuilder(_entityFactory);
            entityBuilder.SetPosition(Position);
            entityBuilder.SetTargerPosition(new Position(new RandomPosition(100).Pos));

            for (int i = 0; i < SpawnParameters.SpawnCount; i++)
            {
                Entity entity = entityBuilder.Build();
                _gameObjectManager.AddGameObject(entity);
            }
        }
    }
}
