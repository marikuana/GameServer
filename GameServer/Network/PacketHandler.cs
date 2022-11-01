using Microsoft.Extensions.DependencyInjection;
using Packets;
using System.Reflection;

namespace GameServerCore
{
    public class PacketHandler
    {
        private PacketFactory _packetFactory;
        private Dictionary<Type, Delegate> handleMethods;
        private IServiceProvider serviceProvider;
        
        public PacketHandler(PacketFactory packetFactory, IServiceProvider service)
        {
            _packetFactory = packetFactory;
            handleMethods = new Dictionary<Type, Delegate>();
            serviceProvider = service;
        }

        public void Init()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(HandlePacket<>))
                .ToList();

            foreach (var type in types)
            {
                Register(type);
            }
        }

        private void Register(Type handlerClass)
        {
            if (handlerClass.BaseType == null)
                return;

            Type packetType = handlerClass.BaseType.GetGenericArguments().First();
            ConstructorInfo constructor = handlerClass.GetConstructors().First();
            
            ParameterInfo[] parametersInfo = constructor.GetParameters();
            object[] parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                parameters[i] = serviceProvider.GetRequiredService(parametersInfo[i].ParameterType);
            }
            var handlerPacket = constructor.Invoke(parameters); 
            handleMethods.Add(packetType, Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(packetType), handlerPacket, "Invoke"));
        }

        private void Register<T, K>() where K : Packet where T : HandlePacket<K>
        {
            ConstructorInfo constructor = typeof(T).GetConstructors().First();

            ParameterInfo[] parametersInfo = constructor.GetParameters();
            object[] parameters = new object[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                parameters[i] = serviceProvider.GetRequiredService(parametersInfo[i].ParameterType);
            }
            T handlerPacket = (T)constructor.Invoke(parameters);
            handleMethods.Add(typeof(K), handlerPacket.Invoke);
        }

        public void Handle(byte[] data)
        {
            Packet packet = _packetFactory.GetPacket(data);

            Handle(packet);
        }

        public void Handle(Packet packet)
        {
            Type type = packet.GetType();
            if (handleMethods.ContainsKey(type))
            {
                handleMethods[type].DynamicInvoke(packet);
            }
        }
    }
}