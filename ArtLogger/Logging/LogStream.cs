using System;
using System.Collections.Generic;

namespace ArtLogger.Logging {
    internal class LogStream {
        bool _DataAvailable = false;
        public bool DataAvailable { get { if (0 < _Data.Count) { _DataAvailable = true; } else { _DataAvailable = false; } return _DataAvailable; } }

        List<Tuple<String, Tuple<LogLevel, ConsoleColor>>> _Data = new List<Tuple<String, Tuple<LogLevel, ConsoleColor>>>();
        public List<Tuple<String, Tuple<LogLevel, ConsoleColor>>> Data { get { return _Data; } }
        
    }
}
