﻿namespace GameServerCore
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:[HH:mm:ss]} {message}");
        }
    }
}