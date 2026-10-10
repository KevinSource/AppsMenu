namespace AppsMenu;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip = new MenuStrip();
        settingsMenu = new ToolStripMenuItem();
        editToolsMenuItem = new ToolStripMenuItem();
        openFolderMenuItem = new ToolStripMenuItem();
        reloadMenuItem = new ToolStripMenuItem();
        menuSeparator = new ToolStripSeparator();
        exitMenuItem = new ToolStripMenuItem();
        toolsPanel = new FlowLayoutPanel();
        menuStrip.SuspendLayout();
        SuspendLayout();
        //
        // menuStrip
        //
        menuStrip.Items.AddRange(new ToolStripItem[] { settingsMenu });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Size = new Size(434, 24);
        menuStrip.TabIndex = 0;
        //
        // settingsMenu
        //
        settingsMenu.DropDownItems.AddRange(new ToolStripItem[] { editToolsMenuItem, openFolderMenuItem, reloadMenuItem, menuSeparator, exitMenuItem });
        settingsMenu.Name = "settingsMenu";
        settingsMenu.Size = new Size(61, 20);
        settingsMenu.Text = "&Settings";
        //
        // editToolsMenuItem
        //
        editToolsMenuItem.Name = "editToolsMenuItem";
        editToolsMenuItem.Size = new Size(200, 22);
        editToolsMenuItem.Text = "&Edit tools...";
        editToolsMenuItem.Click += EditToolsMenuItem_Click;
        //
        // openFolderMenuItem
        //
        openFolderMenuItem.Name = "openFolderMenuItem";
        openFolderMenuItem.Size = new Size(200, 22);
        openFolderMenuItem.Text = "&Show tools.json in Explorer";
        openFolderMenuItem.Click += OpenFolderMenuItem_Click;
        //
        // reloadMenuItem
        //
        reloadMenuItem.Name = "reloadMenuItem";
        reloadMenuItem.Size = new Size(200, 22);
        reloadMenuItem.Text = "&Reload tools.json";
        reloadMenuItem.Click += ReloadMenuItem_Click;
        //
        // menuSeparator
        //
        menuSeparator.Name = "menuSeparator";
        menuSeparator.Size = new Size(197, 6);
        //
        // exitMenuItem
        //
        exitMenuItem.Name = "exitMenuItem";
        exitMenuItem.Size = new Size(200, 22);
        exitMenuItem.Text = "E&xit";
        exitMenuItem.Click += ExitMenuItem_Click;
        //
        // toolsPanel
        //
        toolsPanel.AutoScroll = true;
        toolsPanel.Dock = DockStyle.Fill;
        toolsPanel.FlowDirection = FlowDirection.TopDown;
        toolsPanel.Location = new Point(0, 24);
        toolsPanel.Name = "toolsPanel";
        toolsPanel.Padding = new Padding(12);
        toolsPanel.Size = new Size(434, 337);
        toolsPanel.TabIndex = 1;
        toolsPanel.WrapContents = false;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(434, 361);
        Controls.Add(toolsPanel);
        Controls.Add(menuStrip);
        MainMenuStrip = menuStrip;
        MinimumSize = new Size(300, 200);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Apps Menu";
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip;
    private ToolStripMenuItem settingsMenu;
    private ToolStripMenuItem editToolsMenuItem;
    private ToolStripMenuItem openFolderMenuItem;
    private ToolStripMenuItem reloadMenuItem;
    private ToolStripSeparator menuSeparator;
    private ToolStripMenuItem exitMenuItem;
    private FlowLayoutPanel toolsPanel;
}
