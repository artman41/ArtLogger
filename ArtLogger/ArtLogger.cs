using ArtLogger.Logging;
using System;

namespace ArtLogger {
    /// <summary>
    /// Create an object of this to log data in an application.
    /// </summary>
    public class ArtLogger {

        public ArtLogger() {
            Logger.SetLogger();
        }

        public void Write(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            //Logger.Write(msg, level, c);
            Logger.Stream.Data.Add(new Tuple<String, Tuple<LogLevel, ConsoleColor>>(msg, new Tuple<LogLevel, ConsoleColor>(level, c)));
        }

        public void Write(object msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Write(msg.ToString(), level, c);
        }
    }
}
