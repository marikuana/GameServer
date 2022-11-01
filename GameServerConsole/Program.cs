using Microsoft.Extensions.DependencyInjection;
using System.Net;
using GameServerCore;
using Microsoft.Extensions.Logging;

namespace GameServerConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            IServiceCollection services = ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            Start(serviceProvider);

            while (true)
            {
                Console.ReadLine();
            }
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => {
                configure.SetMinimumLevel(LogLevel.Trace);
                configure.AddConsole();
            });

            services.AddSingleton(new ConfigProvider().GetConfiguration());

            services.AddTransient<EntityFactory>();

            services.AddSingleton<EntityManager>();
            services.AddSingleton<GameObjectManager>();
            services.AddSingleton<IUpdate, GameObjectManager>(service => service.GetRequiredService<GameObjectManager>());
            services.AddSingleton<IUpdate, FakeUpdate>();
            services.AddSingleton<SimulationService>();

            services.AddSingleton<Packets.PacketFactory>();
            services.AddSingleton<PacketHandler>();
            services.AddSingleton<SessionFactory>();
            services.AddSingleton<Server>();

            return services;
        }

        public static void Start(IServiceProvider service)
        {
            var packetHandler = service.GetRequiredService<PacketHandler>();
            var server = service.GetRequiredService<Server>();
            var simulation = service.GetRequiredService<SimulationService>();

            packetHandler.Init();
            server.Start();
            simulation.StartAsync();
        }
    }
}