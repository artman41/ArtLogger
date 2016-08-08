using ArtLogger.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArtLogger.Logging {
    /// <summary>
    ///  Logger to be used across projects.
    ///  Logger should be set to properly log.
    /// </summary>

    internal class Logger {

        /// <summary>
        /// Sets the logger for the project.
        /// </summary>
        internal static void SetLogger() {
            if (!Directory.Exists(Ref.path)) {
                DirectoryInfo di = Directory.CreateDirectory(Ref.path);
            }
            Ref._CurrentFile = DateTime.Now.ToString("yyyy-MM-dd - HH.mm.ss");
            Log($"Initializing ArtLogger @ {DateTime.Now} for Project {Assembly.GetCallingAssembly()} ...");
        }

        /// <summary>
        /// Use to log messages in the console
        /// </summary>
        /// <param name="msg">The message</param>
        /// <param name="level">The log type</param>
        /// <param name="c">The default log colour</param>
        internal static void Write(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Console.OutputEncoding = Encoding.Unicode;

            switch (level) {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (INFO) {msg}");
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (WARNING) {msg}");
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (ERROR) {msg}");
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (DEBUG) {msg}");
                    break;
                case LogLevel.None:
                    Console.ForegroundColor = c;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {msg}");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {msg}");
                    break;
            }
            Log(string.Concat($"[{DateTime.Now.ToString("HH:mm:ss")}] ", msg));
        }

        /// <summary>
        /// Logs everything to a text file.
        /// </summary>
        /// <param name="msg">Text being logged</param>
        private static void Log(String msg = "4220dea7-1f3a-430d-86ca-fd34dfcfb0e5") {
            //Sanity check to make sure things are actually being logged
            if (msg == "4220dea7-1f3a-430d-86ca-fd34dfcfb0e5") {
                throw new Exception("Line Logged incorrectly!!");
            }

            using(var log = File.CreateText(Path.Combine(Ref.path, Ref._CurrentFile + ".log"))) {
                log.WriteLine(msg);
                log.Flush();
            }
        }

    }
}
