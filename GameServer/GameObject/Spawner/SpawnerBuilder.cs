namespace GameServerCore
{
    public class SpawnerBuilder : GameObjectBuilder<Spawner>
    {
        private EntityFactory? _entityFactory;
        private SpawnParameters _spawnParameters;

        public SpawnerBuilder(SpawnerFactory factory) : base(factory)
        {
            _spawnParameters = new SpawnParameters();
        }

        public void SetSpawnInvervalMilliseconds(float value)
        {
            _spawnParameters.SpawnInterval = value;
        }

        public void SetSpawnInvervalSeconds(float value)
        {
            SetSpawnInvervalMilliseconds(value * 1000);
        }

        public void SetSpawnInvervalMinutes(float value)
        {
            SetSpawnInvervalSeconds(value * 1000);
        }

        public void SetSpawnCount(int value)
        {
            _spawnParameters.SpawnCount = value;
        }

        public void SetEntityFactory(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public override Spawner Build()
        {
            Spawner spawner = base.Build();

            spawner.SetSpawnPamameters(_spawnParameters);
            if (_entityFactory != null)
                spawner.SetEntityFactory(_entityFactory);

            return spawner;
        }
    }
}
