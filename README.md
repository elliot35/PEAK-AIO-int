<img width="539" height="339" alt="屏幕截图 2026-03-03 023947" src="https://github.com/user-attachments/assets/94fcbccf-5833-4e3f-8beb-6d5709f11481" />

# PEAK AIO Mod

[![Stars](https://img.shields.io/github/stars/elliot35/PEAK-AIO-int?style=flat)](https://github.com/elliot35/PEAK-AIO-int/stargazers)
[![Forks](https://img.shields.io/github/forks/elliot35/PEAK-AIO-int?style=flat)](https://github.com/elliot35/PEAK-AIO-int/network/members)
[![Contributors](https://img.shields.io/github/contributors/elliot35/PEAK-AIO-int?style=flat)](https://github.com/elliot35/PEAK-AIO-int/graphs/contributors)
![C#](https://img.shields.io/badge/-C%23-239120?logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/-.NET_Framework_4.7.2-512BD4?logo=dotnet&logoColor=white)
![English](https://img.shields.io/badge/lang-English-blue)
![中文](https://img.shields.io/badge/lang-简体中文-red)
![日本語](https://img.shields.io/badge/lang-日本語-green)
![한국어](https://img.shields.io/badge/lang-한국어-orange)

An all-in-one mod menu for [PEAK](https://store.steampowered.com/app/2873498/PEAK/) that brings together player enhancements, inventory tools, teleportation, world interaction, and lobby control in a clean, tabbed ImGui interface.

Supports **English**, **简体中文**, **日本語**, and **한국어**.

---

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Opening the Menu](#opening-the-menu)
- [Configuration](#configuration)
- [Feature Guide](#feature-guide)
- [Troubleshooting](#troubleshooting)
- [Screenshots](#screenshots)
- [Building from Source](#building-from-source)
- [Credits](#credits)
- [Disclaimer](#disclaimer)

---

## Features

### Player
- **Infinite Stamina** — sprint and perform actions without stamina drain
- **Freeze Afflictions** — lock all status effects in their current state
- **No Weight** — remove carry-weight penalties from items and backpack
- **Speed Modifier** — adjustable movement speed multiplier
- **Jump Modifier** — adjustable jump height with optional no-fall-damage
- **Climb / Vine / Rope Speed** — independent speed multipliers for each traversal type
- **Fly Mode** — free-flight in all directions with configurable speed and acceleration
- **Teleport to Ping** — instantly warp to your map ping location
- **Teleport to Coordinates** — enter X/Y/Z and teleport directly

### Inventory
- **Item Slot Editor** — search and assign any game item to your 3 inventory slots
- **Item Recharge** — restore charges (fuel, uses, durability) to any held item

### World
- **Container Browser** — list all luggage/containers within 300 m, sorted by distance
- **Open Container** — remotely open any selected container
- **Open All Nearby** — unlock every container in range at once
- **Warp to Container** — teleport directly to a selected container

### Lobby
- **Player List** — view all connected players in the session
- **Revive / Kill** — revive or kill any individual player
- **Warp To / Warp To Me** — teleport to a player or bring them to you
- **Revive All / Kill All** — batch actions with an "exclude self" toggle
- **Warp All To Me** — pull every player to your position
- **Spawn Scoutmaster** — spawn the Scoutmaster enemy near a selected player (host only)

### UI
- **Tabbed Sidebar** — Player, Items, Lobby, World, About, and Language tabs
- **Configurable Menu Key** — change the toggle hotkey without editing code (default: `Insert`)
- **Multi-Language** — switch languages in-game from the Language tab
- **Persistent Settings** — all toggles, sliders, and preferences are saved to a config file

---

## Requirements

| Dependency | Version | Link |
|---|---|---|
| BepInEx | 5.4.23.3 | [GitHub Releases](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.3) |
| DearImGuiInjection | Latest | [Thunderstore](https://thunderstore.io/c/peak/p/penswer/DearImGuiInjection/) |

> **Important:** Use BepInEx 5.x (not 6.x). The mod targets the BepInEx 5 plugin API.

---

## Installation

### Step 1 — Install BepInEx

1. Download [BepInEx 5.4.23.3](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.3) (x64 for Windows).
2. Locate your PEAK game folder:
   - **Steam:** Right-click PEAK in your Library → Manage → Browse Local Files
3. Extract the BepInEx archive **directly into the game folder** so that `BepInEx/` sits alongside `PEAK.exe`.
4. **Run the game once** and then close it. This generates the required folder structure:
   ```
   PEAK/
   ├── BepInEx/
   │   ├── config/
   │   ├── plugins/
   │   └── ...
   ├── PEAK.exe
   └── ...
   ```

### Step 2 — Install DearImGuiInjection

1. Download [DearImGuiInjection](https://thunderstore.io/c/peak/p/penswer/DearImGuiInjection/) from Thunderstore.
2. Place the `DearImGuiInjection.dll` into `BepInEx/plugins/`.

### Step 3 — Install PEAK AIO

1. Download the latest `PEAK-AIO.dll` from the [Releases](https://github.com/elliot35/PEAK-AIO-int/releases) page.
2. Place `PEAK-AIO.dll` into `BepInEx/plugins/`.

### Step 4 — Launch

1. Launch PEAK using **DirectX 12** (not Vulkan).
   - In Steam: Right-click PEAK → Properties → Launch Options → ensure `-dx12` or select DirectX 12 in-game settings.
2. Press **Insert** (default) to open the mod menu.

Your final `BepInEx/plugins/` folder should look like:

```
BepInEx/plugins/
├── DearImGuiInjection.dll
└── PEAK-AIO.dll
```

---

## Opening the Menu

Press the **Insert** key (default) in-game to toggle the mod overlay and cursor. The menu appears as a floating ImGui window with a tabbed sidebar.

To change the hotkey, see [Configuration](#configuration) below.

---

## Configuration

All settings are automatically saved to a BepInEx config file the first time you run the mod:

```
BepInEx/config/com.onigremlin.peakaio.cfg
```

You can edit this file with any text editor while the game is **closed**. Settings you can configure include:

### Menu Toggle Key

```ini
[General]

## Key to toggle the mod menu overlay. Uses UnityEngine.KeyCode names.
# Setting type: KeyCode
# Default value: Insert
MenuToggleKey = Insert
```

To change the hotkey, replace `Insert` with any valid [UnityEngine.KeyCode](https://docs.unity3d.com/ScriptReference/KeyCode.html) name. Common choices:

| Key | Value |
|---|---|
| Insert | `Insert` |
| Home | `Home` |
| F1–F12 | `F1`, `F2`, ... `F12` |
| Right Shift | `RightShift` |
| Backslash | `Backslash` |
| Numpad 0 | `Keypad0` |

### Language

```ini
[UI]

## Language: 0=English, 1=简体中文, 2=日本語, 3=한국어
LanguageIndex = 0
```

You can also change the language in-game from the **LANG** tab.

### Cheat Defaults

Every toggle and slider (stamina, fly speed, jump height, etc.) is saved in the config file. Change default values here to have them pre-set when the game starts.

---

## Feature Guide

### Fly Mode

Enable **Fly Mode** from the Player tab. While flying:
- **WASD** — move horizontally
- **Jump (Space)** — ascend
- **Crouch (Ctrl)** — descend
- Adjust **Fly Speed** and **Fly Acceleration** sliders in the Details panel

### Teleport to Ping

Enable **Teleport to Ping**, then place a map ping in-game. Your character will instantly warp to the pinged location.

### Inventory Editor

Switch to the **Items** tab to:
1. Use the search bar to find any game item by name
2. Select it from the dropdown to assign it to a slot
3. Use the **Recharge** slider and button to restore item charges

### Lobby Controls

The **Lobby** tab lets you interact with other players in your session. Select a player from the dropdown, then use the action buttons. **Spawn Scoutmaster** is host-only and forces the enemy to aggro the selected player.

### World / Containers

The **World** tab lists all luggage and containers within 300 meters. You can remotely open them or teleport to their location.

---

## Troubleshooting

| Issue | Solution |
|---|---|
| Menu doesn't appear | Make sure you're running **DirectX 12** (not Vulkan). Check that both `DearImGuiInjection.dll` and `PEAK-AIO.dll` are in `BepInEx/plugins/`. |
| Insert key doesn't work | Another program may be capturing the key. Change the hotkey in the config file (see [Configuration](#configuration)). |
| Game crashes on launch | Verify you're using **BepInEx 5.4.23.3** (not 6.x). Remove any conflicting mods from the plugins folder. |
| Config file doesn't exist | Run the game once with the mod installed. The file is generated automatically at `BepInEx/config/com.onigremlin.peakaio.cfg`. |
| Features don't work in-game | Most features require you to be **in a session** (not on the main menu). Some lobby features require being the host. |
| Fly mode feels sluggish | Increase the **Fly Speed** and **Fly Acceleration** sliders in the Player → Details panel. |

---

## Screenshots

<img width="913" height="519" alt="Players tab chinese" src="https://github.com/user-attachments/assets/1371390e-9a60-4393-8985-af6dd81c00a0" />
<img width="913" height="519" alt="Items tab chinese" src="https://github.com/user-attachments/assets/ebb2778c-cdd9-4727-ac26-b661ca061335" />
![Player Tab](https://i.imgur.com/BdaiB4F.png)
![Items Tab](https://i.imgur.com/KOoMNCj.png)
![Lobby Tab](https://i.imgur.com/yY5JnAh.png)

---

## Building from Source

### Prerequisites

- Visual Studio 2022 (or later) with the **.NET desktop development** workload
- .NET Framework 4.7.2 targeting pack

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/elliot35/PEAK-AIO-int.git
   cd PEAK-AIO
   ```
2. Open `PEAK-AIO.sln` in Visual Studio.
3. Restore NuGet packages if prompted.
4. Ensure the following assembly references resolve (you may need to point them to your game's `Managed/` folder or BepInEx `core/` folder):
   - `BepInEx.dll`, `0Harmony.dll`
   - `DearImGuiInjection.dll`, `ImGui.NET.dll`
   - `Assembly-CSharp.dll`
   - `UnityEngine.dll`, `UnityEngine.CoreModule.dll`, `UnityEngine.PhysicsModule.dll`
   - `Photon*.dll`, `Zorro.Core.Runtime.dll`
5. Build in **Release** configuration.
6. Copy `bin/Release/PEAK-AIO.dll` to your `BepInEx/plugins/` folder.

---

## Credits

- [DearImGuiInjection](https://github.com/xiaoxiao921/DearImGuiInjection) — ImGui UI injection for Unity
- [HarmonyX](https://github.com/BepInEx/HarmonyX) — runtime method patching
- [BepInEx](https://github.com/BepInEx/BepInEx) — Unity modding framework
- [Penswer](https://github.com/Penswer/Peak-Everything) — insight and guidance
- [Luluberlu](https://thunderstore.io/c/peak/p/Luluberlu/) — FlyMod example code

---

## Disclaimer

This mod is provided as-is for **educational and personal use only**. It is not affiliated with or endorsed by the developers of PEAK. Use at your own risk and responsibility. Multiplayer features may affect other players in your session — use them considerately.
