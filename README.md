# Plumas en Fuga — Unity Project

Plumas en Fuga is a chaotic 2D endless runner inspired by the viral moment of AMLO chasing a pigeon. Run, dodge, and flap your way to freedom before you get caught!
The player cannot stop moving forward, dodging randomly spawned obstacles and collecting power-ups while difficulty ramps up over time. It includes two difficulty levels with their own biome, and a full menu system (Main Menu, Settings, Audio, Credits, Pause Menu, Game Over screen).

## Features

### Core Gameplay

* **The player cannot stop** — the character auto-runs while the world scrolls toward it; the only inputs are jump and crouch.
* The character can **jump** with `Space`, `W`, or `↑` (keys configurable in `PlayerDataSo`), with variable jump height depending on how long the key is held.
* The character can **crouch** with `S` or `↓` (configurable in `PlayerDataSo`), which plays the crouch animation and shrinks the collider while held, so the player can pass under tall obstacles.
* Obstacles spawn at **random intervals**, choosing a random prefab each time, and scroll toward the player.
* **Lives system** — hitting an obstacle costs one life, followed by a short invulnerability window. The run ends when the player runs out of lives. Lives are shown as icons in the UI.
* Difficulty **increases automatically over time**: world speed rises the longer a run lasts, and obstacles spawn more often. The gap between obstacles is calculated as a random distance divided by the current speed, with a minimum spawn time, so the level always stays winnable.
* **Score** increases automatically with survival time, and the high score persists between sessions.

### Difficulty Levels

* **Easy** and **Hard** can be selected from the Settings panel in the Main Menu (Easy is the default).
* Each level changes the starting world speed, how fast it ramps up, the distance between obstacles, how often power-ups appear, and **which biome is used**.
* The choice carries over from the Main Menu to the Gameplay scene and is kept on Retry.

### Power-ups

Power-ups spawn independently of obstacles, scroll with the world, and are collected on trigger (not solid collision):

* **Extra Life** — adds one life, up to the maximum.
* **Invincibility** — 5 seconds without losing lives, shown as an on-screen countdown.
* **Slow** — temporarily slows the whole world (background, obstacles, and power-ups) and widens the gap between obstacles, shown as an on-screen countdown.

Picking up the same power-up again restarts its timer and its UI animation.

### Visual Feedback

* **Parallax background** with 4 layers moving at different speeds, recycling tiles so the street never ends. Two biomes are available (one per difficulty).
* Background, obstacles, and power-ups move at the **same world speed**, including while the Slow power-up is active.
* A burst-based **Particle System** plays feathers from the player's feet on every jump.
* Animated player (walk, jump, crouch), obstacles, power-ups, and power-up timers.

### Scenes & Menus

* **Main Menu** scene, with Play, Settings (difficulty), Audio, Credits, and Exit.
* **Pause Menu**, opened mid-run with the Escape key, which pauses the game (`Time.timeScale = 0`). Pause is disabled while the Game Over screen is open.
* **Audio panel**, allowing configuration of Master, Background, SFX, and UI volume, applied directly to an `AudioMixer` via exposed parameters.
* **Game Over screen**, displayed when the player runs out of lives, showing the final score and high score, with options to Retry, return to the Main Menu, or Exit.

### Audio

Sound is integrated in the following interactions:

* Background music for the Main Menu.
* Background music for the Gameplay scene.
* UI button clicks.
* Jumping.
* Collecting a power-up.
* On losing (Game Over).

All volume is configurable from the Audio panel and persists between sessions. It is routed through a single shared `AudioMixer` (Master, Background, SFX, and UI groups), converting linear slider values to decibels at runtime.

### Architecture & Code Quality

* **ScriptableObjects** separate data from scene logic, split by role:

  * `PlayerDataSo` (configuration): jump and crouch keys, jump force and time, ground detection, crouch collider, and starting/maximum lives.
  * `DifficultySettingsSo` (configuration, one asset per level): starting world speed, speed increase per second, obstacle distances, minimum spawn time, power-up spawn time, and biome index.
  * `DifficultySelectorSo` (selection): stores which difficulty was chosen, so the choice survives the scene change from the Main Menu to Gameplay.
  * `WorldSpeedSo` (runtime state): the current world speed. `ObstacleSpawner` writes it every frame, and the parallax layers and every obstacle and power-up read it.
  * `ScoreDataSo`: current score and high score, persisted via `PlayerPrefs`, broadcasting change events for the UI.
  * `AudioDataSo`: master/background/SFX/UI volume levels, persisted via `PlayerPrefs` and applied to the shared `AudioMixer`.
* **Observer pattern** with C# events: the UI and `GameManager` subscribe to events (score, lives, death, power-up timers, volume) instead of searching for objects at runtime. The Slow power-up uses a static event, since a prefab cannot reference scene objects.
* Difficulty ramp and power-up effects are kept fully decoupled: `ObstacleSpawner` ramps a base speed over elapsed time, while the Slow power-up only applies a temporary multiplier on top — the two systems never overwrite each other.
* Prefabs reference only assets (`WorldSpeedSo`), never scene objects, so obstacles and power-ups need no setup when spawned. Off-screen objects are destroyed automatically (`DestroyOffScreen`).
* Physics movement uses `Rigidbody2D` velocity in `FixedUpdate` with interpolation, so objects stay smooth relative to the background.
* Marker components (e.g. `ObstacleMarker`) are used instead of string-based tags for collision identification, providing compile-time safety.
* Reusable, single-purpose UI scripts (e.g. `SliderVolumeChannel`, driven by the shared `AudioChannel` enum, and `ButtonClickSound`, which caches its `Button` and shares one scene `AudioSource`) avoid duplicating near-identical code across sliders and buttons.

## Controls

| Action | Keys |
|-|-|
| Jump | `Space`, `W`, `↑` |
| Crouch | `S`, `↓` |
| Pause | `Esc` |

## Credits

* Game Dev / Game Designer / SFX / Pixel Artist assistant jeje - Yeimy Rojas.
* Game Designer / Pixel Artist Lead - Aurora Salazar.


## Notes

During the development of this project, artificial intelligence tools were used as support for resolving questions, understanding concepts, and reviewing code. AI was primarily used as a reference and learning resource, providing explanations of programming structures, syntax, and possible solutions to problems encountered during development. No code was directly copied from AI-generated responses; the code implemented in the project was written and developed by the author based on their own understanding and adapted to the specific needs of the project.

Link Itchio https://yeimy-rojas-midnightbaker.itch.io/plumas-en-fuga

* Contact:
* Yeimy Rojas The Midnight Baker.
https://www.artstation.com/yeimy24401
