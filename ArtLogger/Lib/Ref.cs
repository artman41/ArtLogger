using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtLogger.Lib {
    public class Ref {

        public static String _CurrentFile = string.Empty;
        public static String path = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

    }
}
