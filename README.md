# AppsMenu

A small Windows menu for launching your own programs and scripts. Each tool gets a button; an optional checkbox per tool adds an extra argument, such as a dry-run flag. Tools are added and edited inside the app, so there's nothing to rebuild when the list changes.

## Features

- One button per tool, with an optional description underneath
- **Settings > Edit tools** to add, edit, remove and reorder tools, with a file browser for the program
- Runs `.exe`, `.bat` and `.cmd` files the same way a double-click does, each in its own window
- Paths may use environment variables such as `%USERPROFILE%`
- Opening AppsMenu again brings the running window to the front instead of starting a second copy
- Clear error messages when a program is missing or won't start; the menu stays open

## Requirements

- Windows 10 or 11
- [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) to run it, or the .NET 8 SDK (or later) to build it

## Build and run

Open `AppsMenu.slnx` in Visual Studio (2022 version 17.14 or later opens `.slnx` solutions) and press F5, or from the repository folder:

```
dotnet run --project AppsMenu
```

## Install

Publish it as a single `.exe` into any folder you like:

```
dotnet publish AppsMenu\AppsMenu.csproj -c Release -r win-x64 -o "C:\Tools\AppsMenu"
```

Then add it to the Start menu by creating a shortcut to `AppsMenu.exe` in `%APPDATA%\Microsoft\Windows\Start Menu\Programs`. Right-click it in **All apps** to pin it to Start or the taskbar.

Publishing again later replaces only `AppsMenu.exe`; your tool list is kept.

## Configuring tools

Use **Settings > Edit tools**. The list is saved as `tools.json` next to `AppsMenu.exe`, so you can also edit it by hand and use **Settings > Reload tools.json**:

```json
{
  "title": "Apps Menu",
  "tools": [
    {
      "name": "Notepad",
      "path": "%WINDIR%\\System32\\notepad.exe"
    },
    {
      "name": "Nightly backup",
      "path": "C:\\Scripts\\Backup.bat",
      "arguments": "--all",
      "description": "Copies Documents to the backup drive",
      "optionLabel": "Dry run",
      "optionArguments": "--dry-run"
    }
  ]
}
```

| Field | Required | Meaning |
|---|---|---|
| `title` | | Window title |
| `name` | Yes | Button text |
| `path` | Yes | The `.exe`, `.bat` or `.cmd` to run |
| `arguments` | | Always passed to the program |
| `description` | | Grey text under the button |
| `optionLabel` | | Shows a checkbox with this text next to the button |
| `optionArguments` | | Added after `arguments` when the checkbox is ticked |

`tools.json` holds paths from your own PC, so `.gitignore` keeps it out of the repository.

## Icon

The icon is drawn by `Tools\MakeIcon.ps1`, which draws each size separately so the small taskbar sizes stay sharp. To change the colors, edit them in the script, then run it and rebuild:

```
powershell -ExecutionPolicy Bypass -File Tools\MakeIcon.ps1
```

## License

[MIT](LICENSE)
