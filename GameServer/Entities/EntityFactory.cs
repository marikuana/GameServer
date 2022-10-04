namespace GameServer
{
    public class EntityFactory
    {
        private ILogger _logger;

        public EntityFactory(ILogger logger)
        {
            _logger = logger;
        }

        public Entity GetEntity()
        {
            return new Entity(_logger);
        }
    }
}