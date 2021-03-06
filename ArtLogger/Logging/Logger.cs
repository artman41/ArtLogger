﻿using ArtLogger.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ArtLogger.Logging {
    /// <summary>
    ///  Logger to be used across projects.
    ///  Logger should be set to properly log.
    /// </summary>

    internal class Logger {

        static LogStream _Stream = new LogStream();

        static Thread DictWriter { get; set; }

        internal static LogStream Stream { get { return _Stream; } }
        internal delegate void WriteHandler(String msg, LogLevel level, ConsoleColor c);
        static internal event WriteHandler OnWrite;

        /// <summary>
        /// Sets the logger for the project.
        /// </summary>
        internal static void SetLogger() {
            //OnWrite += Write(msg, level, c);
            OnWrite += Write;
            DictWriter = new Thread(() => { WriteDict(); });
            DictWriter.Start();
            Application.ApplicationExit += Application_ApplicationExit;

            if (!Directory.Exists(Ref.path)) {
                DirectoryInfo di = Directory.CreateDirectory(Ref.path);
            }
            Ref._CurrentFile = DateTime.Now.ToString("yyyy-MM-dd - HH.mm.ss");
            Log($"Initializing ArtLogger @ {DateTime.Now} for {Assembly.GetEntryAssembly()} ...");
        }

        static void Application_ApplicationExit(object sender, EventArgs e) {
            DictWriter.Abort();
        }

        /// <summary>
        /// Logs everything that is contained in the Stream.Data
        /// </summary>
        internal static void WriteDict() {
            while (DictWriter.IsAlive) {
                while (Stream.DataAvailable) {
                    Tuple<String, Tuple<LogLevel, ConsoleColor>> item = Stream.Data.ElementAt(0);
                    OnWrite(item.Item1, item.Item2.Item1, item.Item2.Item2);
                    Stream.Data.Remove(item);
                }
            }
        }

        /// <summary>
        /// Use to log messages in the console
        /// </summary>
        /// <param name="msg">The message</param>
        /// <param name="level">The log type</param>
        /// <param name="c">The default log colour</param>
        internal static void Write(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Console.OutputEncoding = Encoding.Unicode;
            ConsoleColor prev = Console.ForegroundColor;
            if (msg == String.Empty) { return; }
            if (Settings.Default.WriteConsole) {
                switch (level) {
                    case LogLevel.Info:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (INFO) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (WARNING) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (ERROR) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (DEBUG) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.None:
                        Console.ForegroundColor = c;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.Received:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (RECIEVED) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    case LogLevel.Sent:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (SENT) {msg}");
                        Console.ForegroundColor = prev;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {msg}");
                        Console.ForegroundColor = prev;
                        break;
                }
            }
            Log(msg, level);
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

            using (var log = File.AppendText(Path.Combine(Ref.path, Ref._CurrentFile + ".log"))) {
                log.WriteLine(msg);
                log.Flush();
            }
        }

        /// <summary>
        /// Logs everything to a text file.
        /// </summary>
        /// <param name="msg">Text being logged</param>
        /// <param name="level">The Loglevel of the text</param>
        private static void Log(String msg = "4220dea7-1f3a-430d-86ca-fd34dfcfb0e5", LogLevel level = LogLevel.None) {
            //Sanity check to make sure things are actually being logged
            if (msg == "4220dea7-1f3a-430d-86ca-fd34dfcfb0e5") {
                throw new Exception("Line Logged incorrectly!!");
            }

            if (Settings.Default.WriteFile) {
                using (var log = File.AppendText(Path.Combine(Ref.path, Ref._CurrentFile + ".log"))) {
                    switch (level) {
                        case LogLevel.Info:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (INFO) {msg}");
                            log.Flush();
                            break;
                        case LogLevel.Warning:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (WARNING) {msg}");
                            log.Flush();
                            break;
                        case LogLevel.Error:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (ERROR) {msg}");
                            log.Flush();
                            break;
                        case LogLevel.Debug:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (DEBUG) {msg}");
                            log.Flush();
                            break;
                        case LogLevel.Received:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (RECIEVED) {msg}");
                            log.Flush();
                            break;
                        case LogLevel.Sent:
                            log.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] (SENT) {msg}");
                            log.Flush();
                            break;
                        default:
                            log.WriteLine(msg);
                            log.Flush();
                            break;
                    }
                }
            }
        }

    }
}
