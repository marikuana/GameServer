using GameServerCore;
using Microsoft.Extensions.Hosting;

namespace GameServerConsole
{
    public class HostedService : IHostedService
    {
        private PacketHandler _packetHandler;
        private Server _server;
        private SimulationService _simulationService;

        public HostedService(PacketHandler packetHandler, Server server, SimulationService simulationService)
        {
            _packetHandler = packetHandler;
            _server = server;
            _simulationService = simulationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _packetHandler.Init();
            _server.Start();
            _simulationService.StartAsync();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _server.Stop();

            return Task.CompletedTask;
        }
    }
}