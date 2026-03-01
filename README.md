# BetterWinColors

> A lightweight Windows CLI tool to customize system accent colors directly via the registry — no third-party bloatware, no GUI overhead.

---

## Download

> **No installation required** — just download and run.

👉 [**Download BetterWinColors.exe** (latest release)](https://github.com/Pikoumzs/BetterWinColors/releases/latest)

> ⚠️ Windows may show a SmartScreen warning on first launch since the executable is not signed. Click **"More info" → "Run anyway"** to proceed. The source code is fully available above for review.

---

## What it does

BetterWinColors lets you change two Windows system colors that are otherwise not exposed in the Settings UI:

| Color key | Effect |
|---|---|
| `Highlight` | Selection highlight color (text selection, file selection in Explorer, etc.) |
| `HotTrackingColor` | Hyperlink and hover color used across legacy Windows UI elements |

Changes are written directly to `HKEY_CURRENT_USER\Control Panel\Colors` and a system broadcast is sent immediately. A reboot is recommended for all changes to take full effect.

---

## Usage

1. **Run** `BetterWinColors.exe` as a regular user (no admin rights required)
2. **Choose** what to modify:
   - `1` — Highlight only
   - `2` — HotTrackingColor only
   - `3` — Both
3. **Enter** RGB values (0–255) for each channel
4. **Reboot** when prompted, or do it later manually

### Example session

```
=== BetterWinColors v0.1 ===
What would you like to change?
1 - Highlight
2 - HotTrackingColor
3 - Both
Your choice : 3

Enter the RGB values for Highlight :
R (0-255) : 0
G (0-255) : 120
B (0-255) : 215

Enter the RGB values for HotTrackingColor :
R (0-255) : 0
G (0-255) : 100
B (0-255) : 200

Color(s) successfully updated.
Reboot system now ? (Y/N) : Y
```

---

## How it works

- Reads user input and converts it to the `R G B` string format expected by the Windows registry
- Writes the values to `HKEY_CURRENT_USER\Control Panel\Colors`
- Calls `SystemParametersInfo` with `SPI_SETNONCLIENTMETRICS` to broadcast a system-wide settings change
- Optionally triggers an immediate reboot via `shutdown /r /t 0`

No external dependencies. No telemetry. No background processes.

---

## ⚙️ Compatibility

| OS | Status |
|---|---|
| Windows 10 | ✅ Supported |
| Windows 11 | ✅ Supported |
| Windows 7 / 8 | ⚠️ Untested |

---

## 📄 License

MIT — feel free to fork, modify, and redistribute.

---

## 🤝 Contributing

Issues and pull requests are welcome. If you'd like to suggest new color keys to support, open an issue with the registry path and its visual effect.
