using Microsoft.Win32;
using System.Diagnostics;

namespace AppsMenu;

/// <summary>
/// Finds the program a tool entry points to: a file on disk, a name on PATH, or an App Paths name.
/// Shared by MainForm (launch) and SettingsForm (save check) so both agree on what "found" means.
/// </summary>
public static class ToolResolver
{
    /// <summary>Returns the full path of the program, or null if it can't be found.</summary>
    public static string? Resolve(string toolPath)
    {
        string path = Environment.ExpandEnvironmentVariables(toolPath ?? "").Trim().Trim('"');
        if (path.Length == 0) return null;

        if (File.Exists(path)) return Path.GetFullPath(path);
        return FindOnPath(path) ?? FindInAppPaths(path);
    }

    // A bare name like "notepad"; anything with a directory or drive part is a path, not a PATH lookup
    private static bool IsBareName(string name) =>
        name.Length > 0 && name.IndexOfAny(new[] { '\\', '/', ':' }) < 0;

    // Lazy PATH lookup: ask where.exe. Returns the first match's full path, or null.
    private static string? FindOnPath(string name)
    {
        if (!IsBareName(name)) return null;

        var psi = new ProcessStartInfo("where.exe", $"\"{name}\"")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            using var p = Process.Start(psi);
            if (p == null) return null;

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit(3000);

            return p.HasExited && p.ExitCode == 0
                ? output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
                : null;
        }
        catch (Exception)
        {
            return null; // where.exe missing or blocked; treat as not found
        }
    }

    // App Paths lookup (what Win+R and shell execute use for names like "chrome" or "excel").
    // Returns the registered full path if it exists on disk, or null.
    private static string? FindInAppPaths(string name)
    {
        if (!IsBareName(name)) return null;

        // Keys are named with the extension, e.g. "chrome.exe"
        string keyName = Path.HasExtension(name) ? name : name + ".exe";
        string subKey = $@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{keyName}";

        var roots = new[]
        {
            (RegistryHive.CurrentUser,  RegistryView.Default),
            (RegistryHive.LocalMachine, RegistryView.Registry64),
            (RegistryHive.LocalMachine, RegistryView.Registry32)
        };

        foreach (var (hive, view) in roots)
        {
            try
            {
                using var baseKey = RegistryKey.OpenBaseKey(hive, view);
                using var key = baseKey.OpenSubKey(subKey);
                if (key?.GetValue(null) is string value && value.Length > 0)
                {
                    string exe = Environment.ExpandEnvironmentVariables(value).Trim().Trim('"');
                    if (File.Exists(exe)) return exe;
                }
            }
            catch (Exception) { /* access denied or bad entry, try next */ }
        }
        return null;
    }
}
