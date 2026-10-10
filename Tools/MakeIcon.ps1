<#
.SYNOPSIS
    Draws the AppsMenu icon (2x2 rounded tiles) and writes AppsMenu\AppsMenu.ico.

.DESCRIPTION
    Each size is drawn separately (not shrunk from the large one) so the small taskbar sizes stay sharp.
    Change the colors below, run the script, then rebuild AppsMenu.

.EXAMPLE
    powershell -ExecutionPolicy Bypass -File Tools\MakeIcon.ps1
    powershell -ExecutionPolicy Bypass -File Tools\MakeIcon.ps1 -PreviewDir $env:TEMP   # also saves a PNG per size
#>
param(
    [string]$OutIco = (Join-Path $PSScriptRoot "..\AppsMenu\AppsMenu.ico"),
    [string]$PreviewDir
)
Add-Type -AssemblyName System.Drawing

function RoundedRect([float]$x, [float]$y, [float]$w, [float]$h, [float]$r) {
    $p = New-Object Drawing.Drawing2D.GraphicsPath
    if ($r -lt 0.5) { $p.AddRectangle((New-Object Drawing.RectangleF $x, $y, $w, $h)); return $p }
    $d = 2 * $r
    $p.AddArc($x, $y, $d, $d, 180, 90)
    $p.AddArc($x + $w - $d, $y, $d, $d, 270, 90)
    $p.AddArc($x + $w - $d, $y + $h - $d, $d, $d, 0, 90)
    $p.AddArc($x, $y + $h - $d, $d, $d, 90, 90)
    $p.CloseFigure()
    return $p
}

# Colors (each tile is a top-to-bottom gradient; the cyan pair is the top-right tile)
$blueTop =[Drawing.Color]::FromArgb(255, 0x2B, 0x88, 0xD8)
$blueBot = [Drawing.Color]::FromArgb(255, 0x00, 0x5A, 0x9E)
$cyanTop = [Drawing.Color]::FromArgb(255, 0x6C, 0xE0, 0xF5)
$cyanBot = [Drawing.Color]::FromArgb(255, 0x19, 0xA9, 0xD6)

$sizes = 16, 20, 24, 32, 40, 48, 64, 256
$pngs = @()
foreach ($s in $sizes) {
    $bmp = New-Object Drawing.Bitmap $s, $s, ([Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $g = [Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = 'AntiAlias'
    $g.PixelOffsetMode = 'HighQuality'
    $g.Clear([Drawing.Color]::Transparent)

    # Whole-pixel layout so small sizes stay crisp
    $gap = [Math]::Max(1, [Math]::Round($s * 0.10))
    $margin = [Math]::Max(1, [Math]::Round($s * 0.06))
    $tile = [Math]::Floor(($s - 2 * $margin - $gap) / 2)
    $start = [Math]::Floor(($s - (2 * $tile + $gap)) / 2)
    $radius = [Math]::Max(0, $tile * 0.24)
    if ($s -le 16) { $radius = 1.2 }

    for ($row = 0; $row -lt 2; $row++) {
        for ($col = 0; $col -lt 2; $col++) {
            $x = $start + $col * ($tile + $gap)
            $y = $start + $row * ($tile + $gap)
            $accent = ($row -eq 0 -and $col -eq 1)
            $top = if ($accent) { $cyanTop } else { $blueTop }
            $bot = if ($accent) { $cyanBot } else { $blueBot }
            $rect = New-Object Drawing.RectangleF $x, $y, $tile, $tile
            $brush = New-Object Drawing.Drawing2D.LinearGradientBrush $rect, $top, $bot, 90
            $path = RoundedRect $x $y $tile $tile $radius
            $g.FillPath($brush, $path)
            $brush.Dispose(); $path.Dispose()
        }
    }
    $g.Dispose()

    $ms = New-Object IO.MemoryStream
    $bmp.Save($ms, [Drawing.Imaging.ImageFormat]::Png)
    if ($PreviewDir) { $bmp.Save((Join-Path $PreviewDir "icon-$s.png"), [Drawing.Imaging.ImageFormat]::Png) }
    $bmp.Dispose()
    $pngs += , @($s, $ms.ToArray())
}

# ICO container: 6-byte header, 16-byte entry per image, then the PNG data
$out = New-Object IO.MemoryStream
$w = New-Object IO.BinaryWriter $out
$w.Write([UInt16]0); $w.Write([UInt16]1); $w.Write([UInt16]$pngs.Count)
$offset = 6 + 16 * $pngs.Count
foreach ($p in $pngs) {
    $dim = if ($p[0] -ge 256) { 0 } else { $p[0] }
    $w.Write([byte]$dim); $w.Write([byte]$dim); $w.Write([byte]0); $w.Write([byte]0)
    $w.Write([UInt16]1); $w.Write([UInt16]32)
    $w.Write([UInt32]$p[1].Length); $w.Write([UInt32]$offset)
    $offset += $p[1].Length
}
foreach ($p in $pngs) { $w.Write([byte[]]$p[1]) }
$w.Flush()
[IO.File]::WriteAllBytes($OutIco, $out.ToArray())
"Wrote $OutIco ($($out.Length) bytes, sizes: $($sizes -join ', '))"
