using GameServerCore;
using Microsoft.Extensions.Hosting;
using GameServerCore.Api;

namespace GameServerConsole
{
    public class HostedService : IHostedService
    {
        private PacketHandler _packetHandler;
        private Server _server;
        private SimulationService _simulationService;

        private Api _api;

        public HostedService(PacketHandler packetHandler, Server server, SimulationService simulationService, OthersCode othersCode, Api api)
        {
            _packetHandler = packetHandler;
            _server = server;
            _simulationService = simulationService;

            _api = api;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _packetHandler.Init();
            _server.Start();
            _simulationService.StartAsync();

            _api.StartSend();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _server.Stop();

            return Task.CompletedTask;
        }
    }
}