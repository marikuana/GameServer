﻿using NetCoreServer;
using Packets;
using System.Text;
using Microsoft.Extensions.Logging;

namespace GameServerCore
{
    public class Session : TcpSession
    {
        private ILogger _logger;
        private PacketHandler _packetHandler;

        public Session(ILogger<Session> logger, TcpServer server, PacketHandler packetHandler) : base(server)
        {
            _logger = logger;
            _packetHandler = packetHandler;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            byte[] data = new byte[size];
            Array.Copy(buffer, offset, data, 0, size);
            _packetHandler.Handle(this, data);
            base.OnReceived(buffer, offset, size);
        }

        public void Send(Packet packet)
        {
            Send(packet.GetBytes());
        }
    }
}