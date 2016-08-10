# ArtLogger

Custom Logger for all my projects

**Use this in Forms Apps to use the log**

```C#
using System.Runtime.InteropServices;

private void Form1_Load(object sender, EventArgs e) {
    AllocConsole();
}

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool AllocConsole();
```

**Use this to close the log on App Close**
```C#
[STAThread]
static void Main() {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.ApplicationExit += Application_ApplicationExit;
    Application.Run(new Form1());
}

private static void Application_ApplicationExit(object sender, EventArgs e) {
    FreeConsole();
}

[DllImport("kernel32.dll", EntryPoint = "FreeConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
public static extern int FreeConsole();
```
