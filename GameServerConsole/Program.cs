using Microsoft.Extensions.DependencyInjection;
using System.Net;
using GameServerCore;
using Microsoft.Extensions.Logging;
using GameServerData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GameServerConsole
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);          
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddHostedService<HostedService>();

            services.AddLogging(configure => {
                configure.SetMinimumLevel(LogLevel.Trace);
                configure.AddConsole();
            });

            Configuration configuration = new ConfigProvider().GetConfiguration();
            services.AddSingleton(configuration);

            services.AddDbContext<Db>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString);
            });

            services.AddSingleton<EntityFactory>();
            services.AddSingleton<EntityBuilder>();
            services.AddSingleton<SpawnerFactory>();
            services.AddSingleton<ZoneFactory>();
            services.AddSingleton<ZoneSideFactory>();

            services.AddSingleton<EntityManager>();
            services.AddSingleton<GameObjectManager>();
            services.AddSingleton<IUpdate, GameObjectManager>(service => service.GetRequiredService<GameObjectManager>());
            //services.AddSingleton<IUpdate, FakeUpdate>();
            services.AddSingleton<SimulationService>();

            services.AddSingleton<Packets.PacketFactory>();
            services.AddSingleton<PacketHandler>();
            services.AddSingleton<SessionFactory>();
            services.AddSingleton<Server>();

        }
    }
}