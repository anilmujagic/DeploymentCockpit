using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentCockpit.Common
{
    /// <summary>
    /// This is a temporary solution until a better logging system is chosen
    /// </summary>
    public static class Log
    {
        private enum LogType
        {
            Error,
            Warning,
            Success,
            Information
        }

        private static void Entry(LogType logType, string message, params object[] args)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor =
                logType == LogType.Error ? ConsoleColor.Red :
                logType == LogType.Warning ? ConsoleColor.Yellow :
                logType == LogType.Success ? ConsoleColor.Green :
                logType == LogType.Information ? ConsoleColor.White :
                originalColor;

            Console.WriteLine(message, args);

            Console.ForegroundColor = originalColor;
        }

        public static void Exception(Exception ex)
        {
            Entry(LogType.Error, ex.GetAllMessagesWithStackTraces());
        }

        public static void Error(string message, params object[] args)
        {
            Entry(LogType.Error, message, args);
        }

        public static void Warning(string message, params object[] args)
        {
            Entry(LogType.Warning, message, args);
        }

        public static void Success(string message, params object[] args)
        {
            Entry(LogType.Success, message, args);
        }

        public static void Info(string message, params object[] args)
        {
            Entry(LogType.Information, message, args);
        }
    }
}
