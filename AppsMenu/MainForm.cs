using System.Diagnostics;
using System.Text.Json;

namespace AppsMenu;

/// <summary>
/// One button per tool in tools.json. The buttons are built in code (BuildButtons), not in the Designer,
/// so adding a tool only means editing the settings.
/// </summary>
public partial class MainForm : Form
{
    private LauncherSettings _settings = new();

    public MainForm()
    {
        InitializeComponent();
        // Windows Forms doesn't reuse the .exe icon for the window; loading the .ico keeps every size sharp
        using (var icon = typeof(MainForm).Assembly.GetManifestResourceStream("AppsMenu.ico"))
            if (icon != null) Icon = new Icon(icon);
        LoadSettings();
    }

    private void LoadSettings()
    {
        try
        {
            _settings = SettingsStore.Load();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            MessageBox.Show($"Could not read the settings file:\n{SettingsStore.FilePath}\n\n{ex.Message}\n\n" +
                            "Fix it in a text editor and use Settings > Reload, or use Settings > Edit tools to start over.",
                            "Apps Menu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _settings = new LauncherSettings();
        }
        Text = string.IsNullOrWhiteSpace(_settings.Title) ? "Apps Menu" : _settings.Title;
        BuildButtons();
    }

    private void BuildButtons()
    {
        toolsPanel.SuspendLayout();
        foreach (var old in toolsPanel.Controls.Cast<Control>().ToList())
            old.Dispose();

        if (_settings.Tools.Count == 0)
            toolsPanel.Controls.Add(new Label { Text = "No tools yet. Use Settings > Edit tools... to add one.", AutoSize = true });

        foreach (var tool in _settings.Tools)
        {
            bool hasDescription = !string.IsNullOrWhiteSpace(tool.Description);
            var row = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, hasDescription ? 0 : 12)
            };

            var button = new Button { Text = tool.Name, Width = 220, Height = 34 };
            row.Controls.Add(button);

            CheckBox? option = null;
            if (!string.IsNullOrWhiteSpace(tool.OptionLabel))
            {
                option = new CheckBox { Text = tool.OptionLabel, AutoSize = true, Margin = new Padding(10, 9, 0, 0) };
                row.Controls.Add(option);
            }
            button.Click += (_, _) => Launch(tool, option?.Checked == true);
            toolsPanel.Controls.Add(row);

            if (hasDescription)
            {
                toolsPanel.Controls.Add(new Label
                {
                    Text = tool.Description,
                    AutoSize = true,
                    ForeColor = SystemColors.GrayText,
                    Margin = new Padding(3, 2, 3, 12)
                });
            }
        }
        toolsPanel.ResumeLayout();
    }

    private void Launch(ToolEntry tool, bool optionChecked)
    {
        string path = Environment.ExpandEnvironmentVariables(tool.Path);
        string args = string.Join(" ", new[] { tool.Arguments, optionChecked ? tool.OptionArguments : null }
            .Where(a => !string.IsNullOrWhiteSpace(a)));
        try
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found:\n{path}");

            // Shell execute runs .exe, .bat and .cmd the way a double-click would, in their own window
            Process.Start(new ProcessStartInfo(path, args)
            {
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(path) ?? ""
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Could not start '{tool.Name}'.\n\n{ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void EditToolsMenuItem_Click(object? sender, EventArgs e)
    {
        // Re-read first so edits made by hand in tools.json are not overwritten
        LoadSettings();
        using var form = new SettingsForm(_settings);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadSettings();
    }

    private void OpenFolderMenuItem_Click(object? sender, EventArgs e)
    {
        try
        {
            Process.Start("explorer.exe", $"/select,\"{SettingsStore.FilePath}\"");
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ReloadMenuItem_Click(object? sender, EventArgs e) => LoadSettings();

    private void ExitMenuItem_Click(object? sender, EventArgs e) => Close();
}
