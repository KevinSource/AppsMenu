namespace AppsMenu;

/// <summary>
/// Edits a copy of the settings; nothing is written to tools.json until Save.
/// </summary>
public partial class SettingsForm : Form
{
    private readonly List<ToolEntry> _tools;

    // True while the form fills the fields itself, so Field_TextChanged doesn't copy them back
    private bool _loading;

    public SettingsForm(LauncherSettings settings)
    {
        InitializeComponent();
        txtTitle.Text = settings.Title;
        _tools = settings.Tools.Select(t => t.Clone()).ToList();
        RefreshList(0);
    }

    private ToolEntry? Selected => lstTools.SelectedIndex >= 0 ? _tools[lstTools.SelectedIndex] : null;

    private void RefreshList(int select)
    {
        _loading = true;
        lstTools.BeginUpdate();
        lstTools.Items.Clear();
        foreach (var t in _tools)
            lstTools.Items.Add(DisplayName(t));
        lstTools.EndUpdate();
        _loading = false;

        lstTools.SelectedIndex = _tools.Count == 0 ? -1 : Math.Clamp(select, 0, _tools.Count - 1);
        ShowSelected();
    }

    private void ShowSelected()
    {
        var tool = Selected;
        _loading = true;
        txtName.Text = tool?.Name ?? "";
        txtPath.Text = tool?.Path ?? "";
        txtArguments.Text = tool?.Arguments ?? "";
        txtDescription.Text = tool?.Description ?? "";
        txtOptionLabel.Text = tool?.OptionLabel ?? "";
        txtOptionArguments.Text = tool?.OptionArguments ?? "";
        _loading = false;

        int i = lstTools.SelectedIndex;
        grpTool.Enabled = tool != null;
        btnRemove.Enabled = tool != null;
        btnUp.Enabled = i > 0;
        btnDown.Enabled = i >= 0 && i < _tools.Count - 1;
    }

    private void LstTools_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_loading) ShowSelected();
    }

    private void Field_TextChanged(object? sender, EventArgs e)
    {
        var tool = Selected;
        if (_loading || tool == null) return;

        tool.Name = txtName.Text.Trim();
        tool.Path = txtPath.Text.Trim().Trim('"');
        tool.Arguments = NullIfEmpty(txtArguments.Text);
        tool.Description = NullIfEmpty(txtDescription.Text);
        tool.OptionLabel = NullIfEmpty(txtOptionLabel.Text);
        tool.OptionArguments = NullIfEmpty(txtOptionArguments.Text);

        if (sender == txtName)
        {
            _loading = true;
            lstTools.Items[lstTools.SelectedIndex] = DisplayName(tool);
            _loading = false;
        }
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        _tools.Add(new ToolEntry { Name = "New tool" });
        RefreshList(_tools.Count - 1);
        txtName.Focus();
        txtName.SelectAll();
    }

    private void BtnRemove_Click(object? sender, EventArgs e)
    {
        int i = lstTools.SelectedIndex;
        if (i < 0) return;
        _tools.RemoveAt(i);
        RefreshList(i);
    }

    private void BtnUp_Click(object? sender, EventArgs e) => MoveSelected(-1);

    private void BtnDown_Click(object? sender, EventArgs e) => MoveSelected(+1);

    private void MoveSelected(int offset)
    {
        int i = lstTools.SelectedIndex;
        int j = i + offset;
        if (i < 0 || j < 0 || j >= _tools.Count) return;
        (_tools[i], _tools[j]) = (_tools[j], _tools[i]);
        RefreshList(j);
    }

    private void BtnBrowse_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Choose the program to run",
            Filter = "Programs and batch files (*.exe;*.bat;*.cmd)|*.exe;*.bat;*.cmd|All files (*.*)|*.*"
        };
        string current = Environment.ExpandEnvironmentVariables(txtPath.Text.Trim().Trim('"'));
        if (current != "" && Directory.Exists(Path.GetDirectoryName(current)))
            dialog.InitialDirectory = Path.GetDirectoryName(current);

        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        txtPath.Text = dialog.FileName;
        if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text == "New tool")
            txtName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        for (int i = 0; i < _tools.Count; i++)
        {
            if (_tools[i].Name == "" || _tools[i].Path == "")
            {
                lstTools.SelectedIndex = i;
                MessageBox.Show(this, "Every tool needs a name and a program.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                (_tools[i].Name == "" ? txtName : txtPath).Focus();
                return;
            }
        }

        var missing = _tools.Where(t => !File.Exists(Environment.ExpandEnvironmentVariables(t.Path))).ToList();
        if (missing.Count > 0)
        {
            string list = string.Join("\n", missing.Select(t => $"  {t.Name}: {t.Path}"));
            if (MessageBox.Show(this, $"These programs were not found:\n\n{list}\n\nSave anyway?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
        }

        var settings = new LauncherSettings
        {
            Title = string.IsNullOrWhiteSpace(txtTitle.Text) ? "Apps Menu" : txtTitle.Text.Trim(),
            Tools = _tools
        };
        try
        {
            SettingsStore.Save(settings);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            MessageBox.Show(this, $"Could not save {SettingsStore.FilePath}\n\n{ex.Message}", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        DialogResult = DialogResult.OK;
    }

    private static string DisplayName(ToolEntry t) => string.IsNullOrWhiteSpace(t.Name) ? "(no name)" : t.Name;

    private static string? NullIfEmpty(string s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
