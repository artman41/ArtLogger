# ArtLogger

Custom Logger for all my projects

**Use this in Forms Apps to use the log**

```C#
using System.Runtime.InteropServices;

private void Form1_Load(object sender, EventArgs e)
{
    AllocConsole();
}

[DllImport("kernel32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
static extern bool AllocConsole();
```
