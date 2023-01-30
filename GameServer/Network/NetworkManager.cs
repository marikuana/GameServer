using Microsoft.Extensions.Logging;
using Packets;

namespace GameServerCore
{
    public class NetworkManager
    {
        private Server _server;
        private ILogger<NetworkManager> _logger;

        private Dictionary<Session, Queue<Packet>> listToSend;

        public Action<Session>? OnConnecting;
        public Action<Session>? OnDisconnecting;

        public NetworkManager(Server server, ILogger<NetworkManager> logger)
        {
            _server = server;
            _logger = logger;

            listToSend = new Dictionary<Session, Queue<Packet>>();
            _server.Connecting += Connect;
            _server.Disconnecting += Disconnect;

            Task.Run(Sending);
        }

        public void Connect(Session session)
        {
            lock (this)
            {
                if (!listToSend.ContainsKey(session))
                    listToSend.Add(session, new Queue<Packet>());
            }
            OnConnecting?.Invoke(session);
        }

        public void Disconnect(Session session)
        {
            lock (this)
            {
                if (listToSend.ContainsKey(session))
                    listToSend.Remove(session);
            }
            OnDisconnecting?.Invoke(session);
        }

        public void Send(Session session, Packet packet)
        {
            lock (this)
            {
                if (listToSend.ContainsKey(session))
                {
                    listToSend[session].Enqueue(packet);
                }
            }
        }

        public void Send(Packet packet)
        {
            foreach (var session in listToSend.Keys)
            {
                Send(session, packet);
            }
        }

        public void SendOther(Session notSendSession, Packet packet)
        {
            foreach (var session in listToSend.Keys)
            {
                if (notSendSession.Equals(session))
                    continue;
                Send(session, packet);
            }
        }

        private void Sending()
        {
            while (true)
            {
                lock (this)
                {
                    foreach (var session in listToSend)
                    {
                        List<byte> data = new List<byte>();

                        while (session.Value.TryDequeue(out Packet? packet))
                        {
                            
                            data.AddRange(packet.GetBytes());
                        }

                        if (data.Count > 0)
                        {
                            session.Key.Send(data.ToArray());
                        }
                    }
                }

                Task.Delay(10).Wait();
            }
        }
    }
}