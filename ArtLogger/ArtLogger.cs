using ArtLogger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtLogger {
    /// <summary>
    /// Create an object of this to log in an application.
    /// </summary>
    public class ArtLogger {

        public ArtLogger() {
            Logger.SetLogger();
        }

        public void Write(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Logger.Write(msg, level, c);
        }

        public void Write(object msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Write(msg.ToString(), level, c);
        }
    }
}
