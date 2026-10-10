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
/// Reads and writes tools.json next to the .exe. The file is never part of the build output,
/// so publishing a new version never overwrites the tools you have set up.
/// </summary>
public static class SettingsStore
{
    public static string FilePath => Path.Combine(AppContext.BaseDirectory, "tools.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Loads tools.json; with no file yet, starts with an empty list (tools are added in Edit tools).
    /// Throws on unreadable or invalid JSON.
    /// </summary>
    /// <remarks>
    /// Keep real paths out of this source file: they belong only in tools.json, which .gitignore keeps out of the repository.
    /// </remarks>
    public static LauncherSettings Load()
    {
        if (!File.Exists(FilePath))
            return new LauncherSettings();
        return JsonSerializer.Deserialize<LauncherSettings>(File.ReadAllText(FilePath), Options) ?? new LauncherSettings();
    }

    public static void Save(LauncherSettings settings) =>
        File.WriteAllText(FilePath, JsonSerializer.Serialize(settings, Options));
}
