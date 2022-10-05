using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace GameServer
{
    public class Program
    {
        public static IServiceProvider container;

        public static void Main(string[] args)
        {
            RegisterServices();

            Start();
        }

        public static void RegisterServices()
        {
            var build = new ServiceCollection();

            build.AddTransient<ILogger, Logger>();
            build.AddTransient<EntityFactory>();

            build.AddSingleton<EntityManager>();
            build.AddSingleton<IUpdate, EntityManager>(service => service.GetRequiredService<EntityManager>());
            build.AddSingleton<IUpdate, FakeUpdate>();
            build.AddSingleton<SimulationService>();

            build.AddSingleton(service =>
            {
                ILogger logger = service.GetRequiredService<ILogger>();
                return new Server(logger, IPAddress.Any, 4444);
            });

            container = build.BuildServiceProvider();
        }

        public static void Start()
        {
            var server = container.GetRequiredService<Server>();
            server.Start();

            var simulation = container.GetRequiredService<SimulationService>();
            simulation.Start();
        }
    }
}