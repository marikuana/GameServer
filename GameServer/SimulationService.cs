using Microsoft.Extensions.DependencyInjection;

namespace GameServerCore
{
    public class SimulationService
    {
        private bool _running;
        private DateTime lastUpdate;
        private IEnumerable<IUpdate> updates;

        private ILogger logger;

        public SimulationService(IEnumerable<IUpdate> updates, ILogger logger)
        {
            this.updates = updates;
            this.logger = logger;
        }

        public void Start()
        {
            if (_running)
                return;
            _running = true;

            lastUpdate = DateTime.UtcNow;
            Update();
        }

        public void StartAsync()
        {
            if (_running)
                return;
            _running = true;

            lastUpdate = DateTime.UtcNow;
            Task.Run(Update);
        }

        public void Stop()
        {
            if (!_running)
                return;

            _running = false;
        }

        public void Update()
        {
            while (_running)
            {
                TimeSpan time = DateTime.UtcNow - lastUpdate;
                lastUpdate = DateTime.UtcNow;
                foreach (var update in updates)
                {
                    update.Update(time);
                }
                logger.Log($"Update: {time.TotalMilliseconds} ms");
            }
        }
    }
}