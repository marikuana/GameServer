using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

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
            //build.AddSingleton<IUpdate, FakeUpdate>();
            build.AddSingleton<SimulationService>();

            container = build.BuildServiceProvider();
        }

        public static void Start()
        {
            var simulation = container.GetRequiredService<SimulationService>();
            simulation.Start();
        }
    }
}