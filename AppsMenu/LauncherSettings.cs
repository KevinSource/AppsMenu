using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppsMenu;

/// <summary>Everything stored in tools.json.</summary>
public sealed class LauncherSettings
{
    public string Title { get; set; } = "Apps Menu";
    public List<ToolEntry> Tools { get; set; } = [];
}

/// <summary>One button in the launcher.</summary>
public sealed class ToolEntry
{
    public string Name { get; set; } = "";

    /// <summary>The .exe, .bat or .cmd to run. Environment variables like %USERPROFILE% are expanded.</summary>
    public string Path { get; set; } = "";

    /// <summary>Always passed to the program.</summary>
    public string? Arguments { get; set; }

    /// <summary>Shown in grey under the button.</summary>
    public string? Description { get; set; }

    /// <summary>When set, a checkbox with this text appears next to the button.</summary>
    public string? OptionLabel { get; set; }

    /// <summary>Added to the arguments when the checkbox is ticked.</summary>
    public string? OptionArguments { get; set; }

    public ToolEntry Clone() => (ToolEntry)MemberwiseClone();
}

/// <summary>
/// Reads and writes tools.json in %LOCALAPPDATA%\AppsMenu, so each computer keeps its own list even when
/// AppsMenu.exe itself is shared (e.g. on Google Drive). A tools.json next to the .exe is only a starting list:
/// it is copied to the local file the first time a computer runs the menu.
/// Neither file is part of the build output, so publishing a new version never overwrites the tools you have set up.
/// </summary>
public static class SettingsStore
{
    public static string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppsMenu", "tools.json");

    /// <summary>The shared starting list beside the .exe, copied to FilePath when that doesn't exist yet.</summary>
    public static string SeedPath => Path.Combine(AppContext.BaseDirectory, "tools.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Loads the local tools.json, first copying the starting list beside the .exe if there's no local file yet.
    /// With neither file, starts with an empty list (tools are added in Edit tools).
    /// Throws on unreadable or invalid JSON.
    /// </summary>
    /// <remarks>
    /// Keep real paths out of this source file: they belong only in tools.json, which .gitignore keeps out of the repository.
    /// </remarks>
    public static LauncherSettings Load()
    {
        if (!File.Exists(FilePath))
        {
            if (!File.Exists(SeedPath))
                return new LauncherSettings();
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.Copy(SeedPath, FilePath);
        }
        return JsonSerializer.Deserialize<LauncherSettings>(File.ReadAllText(FilePath), Options) ?? new LauncherSettings();
    }

    public static void Save(LauncherSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, Options));
    }
}
