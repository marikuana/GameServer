using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public abstract class GameObjectFactory<Object> where Object : GameObject
    {
        private ILoggerFactory _loggerFactory;

        public GameObjectFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public abstract Object Create();

        protected ILogger<Object> GetLogger() =>
            _loggerFactory.CreateLogger<Object>();
    }
}
