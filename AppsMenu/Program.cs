using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace AppsMenu;

static class Program
{
    // Held for the life of the app; a second copy that can't take it switches to this one instead
    private static Mutex? _singleInstance;

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    private const int SW_RESTORE = 9;

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        _singleInstance = new Mutex(true, MutexName(), out bool createdNew);
        if (!createdNew)
        {
            BringExistingInstanceToFront();
            return;
        }

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
        GC.KeepAlive(_singleInstance);
    }

    /// <summary>
    /// One instance per copy of the .exe (per user): the deployed menu and a Debug build in
    /// Visual Studio each run on their own instead of switching to each other.
    /// </summary>
    private static string MutexName()
    {
        string exe = Environment.ProcessPath ?? AppContext.BaseDirectory;
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(exe.ToUpperInvariant()));
        return $@"Local\AppsMenu-{Convert.ToHexString(hash, 0, 8)}";
    }

    private static void BringExistingInstanceToFront()
    {
        using var current = Process.GetCurrentProcess();
        string? exe = Environment.ProcessPath;
        foreach (var process in Process.GetProcessesByName(current.ProcessName))
        {
            using (process)
            {
                if (process.Id == current.Id || !SamePath(process, exe))
                    continue;

                IntPtr handle = process.MainWindowHandle;
                if (handle == IntPtr.Zero)
                    continue;

                // Only un-minimize; restoring a maximized window would shrink it
                if (IsIconic(handle))
                    ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(handle);
                return;
            }
        }
    }

    private static bool SamePath(Process process, string? exe)
    {
        try
        {
            return exe == null || string.Equals(process.MainModule?.FileName, exe, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return false;   // exited meanwhile, or not ours to inspect
        }
    }
}
