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

            while (true)
            {
                Console.ReadLine();
            }
        }

        public static void RegisterServices()
        {
            var build = new ServiceCollection();

            build.AddTransient<ILogger, ConsoleLogger>();
            build.AddTransient<EntityFactory>();

            build.AddSingleton<EntityManager>();
            build.AddSingleton<IUpdate, EntityManager>(service => service.GetRequiredService<EntityManager>());
            build.AddSingleton<IUpdate, FakeUpdate>();
            build.AddSingleton<SimulationService>();

            build.AddSingleton<Packets.PacketFactory>();
            build.AddSingleton<PacketHandler>();
            build.AddSingleton<SessionFactory>();
            build.AddSingleton(service =>
            {
                ILogger logger = service.GetRequiredService<ILogger>();
                SessionFactory sessionFactory = service.GetRequiredService<SessionFactory>();
                return new Server(logger, sessionFactory, IPAddress.Any, 4444);
            });

            container = build.BuildServiceProvider();
        }

        public static void Start()
        {
            var server = container.GetRequiredService<Server>();
            var simulation = container.GetRequiredService<SimulationService>();

            server.Start();
            simulation.StartAsync();

        }
    }
}