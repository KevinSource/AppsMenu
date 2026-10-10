namespace AppsMenu;

partial class SettingsForm
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
        lblTitle = new Label();
        txtTitle = new TextBox();
        lstTools = new ListBox();
        btnAdd = new Button();
        btnRemove = new Button();
        btnUp = new Button();
        btnDown = new Button();
        grpTool = new GroupBox();
        lblName = new Label();
        txtName = new TextBox();
        lblPath = new Label();
        txtPath = new TextBox();
        btnBrowse = new Button();
        lblArguments = new Label();
        txtArguments = new TextBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblOptionLabel = new Label();
        txtOptionLabel = new TextBox();
        lblOptionArguments = new Label();
        txtOptionArguments = new TextBox();
        lblHint = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        grpTool.SuspendLayout();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(12, 15);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(80, 15);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Window title:";
        //
        // txtTitle
        //
        txtTitle.Location = new Point(110, 12);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(300, 23);
        txtTitle.TabIndex = 1;
        //
        // lstTools
        //
        lstTools.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        lstTools.FormattingEnabled = true;
        lstTools.IntegralHeight = false;
        lstTools.ItemHeight = 15;
        lstTools.Location = new Point(12, 48);
        lstTools.Name = "lstTools";
        lstTools.Size = new Size(200, 322);
        lstTools.TabIndex = 2;
        lstTools.SelectedIndexChanged += LstTools_SelectedIndexChanged;
        //
        // btnAdd
        //
        btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAdd.Location = new Point(12, 376);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(60, 27);
        btnAdd.TabIndex = 3;
        btnAdd.Text = "&Add";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += BtnAdd_Click;
        //
        // btnRemove
        //
        btnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRemove.Location = new Point(76, 376);
        btnRemove.Name = "btnRemove";
        btnRemove.Size = new Size(64, 27);
        btnRemove.TabIndex = 4;
        btnRemove.Text = "&Remove";
        btnRemove.UseVisualStyleBackColor = true;
        btnRemove.Click += BtnRemove_Click;
        //
        // btnUp
        //
        btnUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnUp.Location = new Point(144, 376);
        btnUp.Name = "btnUp";
        btnUp.Size = new Size(32, 27);
        btnUp.TabIndex = 5;
        btnUp.Text = "▲";
        btnUp.UseVisualStyleBackColor = true;
        btnUp.Click += BtnUp_Click;
        //
        // btnDown
        //
        btnDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDown.Location = new Point(180, 376);
        btnDown.Name = "btnDown";
        btnDown.Size = new Size(32, 27);
        btnDown.TabIndex = 6;
        btnDown.Text = "▼";
        btnDown.UseVisualStyleBackColor = true;
        btnDown.Click += BtnDown_Click;
        //
        // grpTool
        //
        grpTool.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpTool.Controls.Add(lblName);
        grpTool.Controls.Add(txtName);
        grpTool.Controls.Add(lblPath);
        grpTool.Controls.Add(txtPath);
        grpTool.Controls.Add(btnBrowse);
        grpTool.Controls.Add(lblArguments);
        grpTool.Controls.Add(txtArguments);
        grpTool.Controls.Add(lblDescription);
        grpTool.Controls.Add(txtDescription);
        grpTool.Controls.Add(lblOptionLabel);
        grpTool.Controls.Add(txtOptionLabel);
        grpTool.Controls.Add(lblOptionArguments);
        grpTool.Controls.Add(txtOptionArguments);
        grpTool.Controls.Add(lblHint);
        grpTool.Location = new Point(224, 42);
        grpTool.Name = "grpTool";
        grpTool.Size = new Size(448, 328);
        grpTool.TabIndex = 7;
        grpTool.TabStop = false;
        grpTool.Text = "Selected tool";
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(12, 28);
        lblName.Name = "lblName";
        lblName.Size = new Size(42, 15);
        lblName.TabIndex = 0;
        lblName.Text = "Name:";
        //
        // txtName
        //
        txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtName.Location = new Point(124, 25);
        txtName.Name = "txtName";
        txtName.Size = new Size(310, 23);
        txtName.TabIndex = 1;
        txtName.TextChanged += Field_TextChanged;
        //
        // lblPath
        //
        lblPath.AutoSize = true;
        lblPath.Location = new Point(12, 62);
        lblPath.Name = "lblPath";
        lblPath.Size = new Size(95, 15);
        lblPath.TabIndex = 2;
        lblPath.Text = "Program or .bat:";
        //
        // txtPath
        //
        txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPath.Location = new Point(124, 59);
        txtPath.Name = "txtPath";
        txtPath.Size = new Size(230, 23);
        txtPath.TabIndex = 3;
        txtPath.TextChanged += Field_TextChanged;
        //
        // btnBrowse
        //
        btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBrowse.Location = new Point(360, 58);
        btnBrowse.Name = "btnBrowse";
        btnBrowse.Size = new Size(74, 25);
        btnBrowse.TabIndex = 4;
        btnBrowse.Text = "&Browse...";
        btnBrowse.UseVisualStyleBackColor = true;
        btnBrowse.Click += BtnBrowse_Click;
        //
        // lblArguments
        //
        lblArguments.AutoSize = true;
        lblArguments.Location = new Point(12, 96);
        lblArguments.Name = "lblArguments";
        lblArguments.Size = new Size(69, 15);
        lblArguments.TabIndex = 5;
        lblArguments.Text = "Arguments:";
        //
        // txtArguments
        //
        txtArguments.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtArguments.Location = new Point(124, 93);
        txtArguments.Name = "txtArguments";
        txtArguments.Size = new Size(310, 23);
        txtArguments.TabIndex = 6;
        txtArguments.TextChanged += Field_TextChanged;
        //
        // lblDescription
        //
        lblDescription.AutoSize = true;
        lblDescription.Location = new Point(12, 130);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(70, 15);
        lblDescription.TabIndex = 7;
        lblDescription.Text = "Description:";
        //
        // txtDescription
        //
        txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDescription.Location = new Point(124, 127);
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(310, 23);
        txtDescription.TabIndex = 8;
        txtDescription.TextChanged += Field_TextChanged;
        //
        // lblOptionLabel
        //
        lblOptionLabel.AutoSize = true;
        lblOptionLabel.Location = new Point(12, 176);
        lblOptionLabel.Name = "lblOptionLabel";
        lblOptionLabel.Size = new Size(93, 15);
        lblOptionLabel.TabIndex = 9;
        lblOptionLabel.Text = "Checkbox label:";
        //
        // txtOptionLabel
        //
        txtOptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtOptionLabel.Location = new Point(124, 173);
        txtOptionLabel.Name = "txtOptionLabel";
        txtOptionLabel.Size = new Size(310, 23);
        txtOptionLabel.TabIndex = 10;
        txtOptionLabel.TextChanged += Field_TextChanged;
        //
        // lblOptionArguments
        //
        lblOptionArguments.AutoSize = true;
        lblOptionArguments.Location = new Point(12, 210);
        lblOptionArguments.Name = "lblOptionArguments";
        lblOptionArguments.Size = new Size(106, 15);
        lblOptionArguments.TabIndex = 11;
        lblOptionArguments.Text = "Checkbox adds:";
        //
        // txtOptionArguments
        //
        txtOptionArguments.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtOptionArguments.Location = new Point(124, 207);
        txtOptionArguments.Name = "txtOptionArguments";
        txtOptionArguments.Size = new Size(310, 23);
        txtOptionArguments.TabIndex = 12;
        txtOptionArguments.TextChanged += Field_TextChanged;
        //
        // lblHint
        //
        lblHint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHint.ForeColor = SystemColors.GrayText;
        lblHint.Location = new Point(12, 248);
        lblHint.Name = "lblHint";
        lblHint.Size = new Size(422, 66);
        lblHint.TabIndex = 13;
        lblHint.Text = "Leave both checkbox fields empty for no checkbox. When the checkbox is ticked, \"Checkbox adds\" is passed after the arguments.\r\n\r\nThe path may use variables such as %USERPROFILE%.";
        //
        // btnSave
        //
        btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSave.Location = new Point(516, 376);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(75, 27);
        btnSave.TabIndex = 8;
        btnSave.Text = "&Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(597, 376);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 27);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        //
        // SettingsForm
        //
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(684, 415);
        Controls.Add(lblTitle);
        Controls.Add(txtTitle);
        Controls.Add(lstTools);
        Controls.Add(btnAdd);
        Controls.Add(btnRemove);
        Controls.Add(btnUp);
        Controls.Add(btnDown);
        Controls.Add(grpTool);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        MinimizeBox = false;
        MinimumSize = new Size(700, 454);
        Name = "SettingsForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Edit tools";
        grpTool.ResumeLayout(false);
        grpTool.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTitle;
    private TextBox txtTitle;
    private ListBox lstTools;
    private Button btnAdd;
    private Button btnRemove;
    private Button btnUp;
    private Button btnDown;
    private GroupBox grpTool;
    private Label lblName;
    private TextBox txtName;
    private Label lblPath;
    private TextBox txtPath;
    private Button btnBrowse;
    private Label lblArguments;
    private TextBox txtArguments;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblOptionLabel;
    private TextBox txtOptionLabel;
    private Label lblOptionArguments;
    private TextBox txtOptionArguments;
    private Label lblHint;
    private Button btnSave;
    private Button btnCancel;
}
