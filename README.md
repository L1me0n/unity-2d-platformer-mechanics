# Easy2DPlatformer

A small 2D platformer made in Unity as a learning + portfolio project focused on **gameplay systems** (movement feel, checkpoints/respawn, scene flow, and persistence).  
Visuals/audio are intentionally minimal (greybox) to keep the scope on mechanics and engineering.

## Play
- Itch.io (Windows): https://l1me0n.itch.io/easy2dplatformer

## Controls
- **A/D** or **Left/Right**: Move
- **Space**: Jump  
  - **Double Jump** is enabled in **Level2+** (press Space again in air)
- **Esc**: Pause / Resume

## Features
- **3 levels** with hazards (spikes), moving platforms, checkpoints, and exit doors
- **Responsive movement**: acceleration/deceleration, jump buffer, coyote time, improved fall gravity
- **Moving platform carry** (delta-based, physics-friendly)
- **Checkpoint + respawn system**
  - Respawn restores the world state to the last checkpoint (including coins)
- **Coins system**
  - Coins persist across levels during a run
  - On death: restores to checkpoint state
  - On restart level: restores to level-entry coin count
- **Pause menu** (resume, restart, main menu)
- **Level timers** + **best time per level** saved across sessions
- **Win screen stats**: total coins collected + best times

## Scenes
Build order:
1. MainMenu  
2. Level1  
3. Level2  
4. Level3  
5. Win  

## Project Structure
- `Assets/_Project/Scripts/` - gameplay + UI systems  
- `Assets/_Project/Prefabs/` - player, checkpoints, coins, spikes, UI triggers  
- `Assets/_Project/Scenes/` - MainMenu, Level1-3, Win  
- `Assets/_Project/Physics/` - NoFriction Physics Material 2D
## Notes
- Uses the **Legacy Input** (`UnityEngine.Input`) with Unity's Active Input Handling set to **Both**.
- **AI assistance:** Some code and documentation were developed with AI assistance; I reviewed, integrated, and tested everything in Unity.

