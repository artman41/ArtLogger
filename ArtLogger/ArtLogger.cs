using ArtLogger.Gui;
using ArtLogger.Logging;
using ArtLogger.Lib;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ArtLogger {
    /// <summary>
    /// Create an object of this to log data in an application.
    /// </summary>
    public class ArtLogger {

        public bool WriteConsole { get { return Settings.Default.WriteConsole; } set { Settings.Default.WriteConsole = value; } }
        public bool WriteFile { get { return Settings.Default.WriteFile; } set { Settings.Default.WriteFile = value; } }
        static Thread GuiThread;
        static GuiForm LoggerGui { get; set; }

        public ArtLogger() {
            if (Settings.Default.UseGui) {
                GuiThread = new Thread(() => this.InitGui(this));
                GuiThread.Start();
            }
            Logger.SetLogger();
        }

        public void Write(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            //Logger.Write(msg, level, c);
            Logger.Stream.Data.Add(new Tuple<String, Tuple<LogLevel, ConsoleColor>>(msg, new Tuple<LogLevel, ConsoleColor>(level, c)));
        }

        public void Write(object msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.White) {
            Write(msg.ToString(), level, c);
        }

        void InitGui(object sender) {
            Logger.OnWrite += Logger_OnWrite;
            LoggerGui = new GuiForm();
            LoggerGui.FormClosed += CloseGuiThread;
            RunGui();
        }

        [STAThread]
        static void RunGui() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += CloseGuiThread;
            Application.Run(LoggerGui);
        }

        private static void CloseGuiThread(object sender, EventArgs e) {
            try {
                GuiThread.Abort();
            } catch (Exception) {

            }
       }

        private void Logger_OnWrite(String msg, LogLevel level = LogLevel.None, ConsoleColor c = ConsoleColor.Black) {
            LoggerGui.Invoke(new Action(delegate () {
                LoggerGui.dataGridView1.Rows.Add(msg, level.ToString("F"));
            }));
        }
    }
}
