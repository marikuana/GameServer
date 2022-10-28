using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class SimulationService
    {
        private bool _running;
        private DateTime lastUpdate;
        private IEnumerable<IUpdate> updates;

        private ILogger logger;

        public SimulationService(IEnumerable<IUpdate> updates, ILogger<SimulationService> logger)
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
                logger.LogInformation($"Update: {time.TotalMilliseconds} ms");
            }
        }
    }
}