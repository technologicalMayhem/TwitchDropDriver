using System;

namespace TwitchDropDriverLib
{
    public class Logger : ILogger
    {
        public Logger(bool debug = false)
        {
            _debug = debug;
        }

        private readonly bool _debug;

        public void LogInformation(string message)
        {
            WriteMessage(message);
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteMessage(message);
            Console.ResetColor();
        }

        public void LogCritical(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            WriteMessage(message);
            Console.ResetColor();
        }

        public void LogDebug(string message)
        {
            if (!_debug) return;
            WriteMessage(message);
            Console.ResetColor();
        }

        private static void WriteMessage(string message)
        {
            Console.WriteLine($"[{DateTime.Now:g}] {message}");
        }
    }
}