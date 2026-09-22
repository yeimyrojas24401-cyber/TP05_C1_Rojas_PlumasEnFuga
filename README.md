# Plumas en Fuga — Unity Project

Plumas en Fuga is a chaotic 2D endless runner inspired by the viral moment of AMLO chasing a pigeon. Run, dodge, and flap your way to freedom before you get caught!
The player cannot stop moving forward, dodging randomly spawned obstacles and collecting power-ups while difficulty ramps up over time, with a full menu system (Main Menu, Pause Menu, Settings, Game Over screen).

## Features

### Core Gameplay

* **The player cannot stop** — the character auto-runs; the only inputs are jump and crouch.
* The character can **jump**, using the Up Arrow key (`W`, configurable via `PlayerDataSo` in Unity's project, check the repo), with variable jump height depending on how long the key is held.
* The character can **crouch**, using the Down Arrow key (`S`, configurable via `PlayerDataSo`), which swaps the sprite and shrinks the collider while held.
* Obstacles spawn at **random positions/intervals**, scrolling toward the player; colliding with one ends the run.
* Difficulty **increases automatically over time**: obstacle speed rises and spawn intervals shrink the longer a run lasts, with a minimum gap distance guaranteeing the level always stays winnable.
* **Power-ups** spawn independently of obstacles and are collected on trigger (not solid collision). Picking one up temporarily slows obstacle speed and widens the gap between obstacles for a set duration, shown as an on-screen countdown.
* **Score** increases automatically over time/distance while running, and the high score persists between sessions.

### Visual Feedback

* A burst-based **Particle System** plays feathers from the player's feet on every jump.

### Scenes & Menus

* **Main Menu** scene, with access to Play, Audio, Credits, and Exit.
* **Pause Menu**, accessible mid-run with the Escape key, which pauses the game (`Time.timeScale = 0`).
* **Settings/Audio screen**, allowing configuration of Master, Background, SFX, and UI volume, applied directly to an `AudioMixer` via exposed parameters.
* **Game Over screen**, displayed when the player collides with an obstacle, showing the final score and high score, with options to Retry, return to the Main Menu, or Exit.

### Audio

Sound is integrated in the following interactions:

* Background music for the Main Menu.
* Background music for the Gameplay scene.
* UI button clicks.
* Jumping.
* Collecting a power-up.
* On losing (Game Over).

All volume is configurable from the Settings/Audio panel, routed through a single shared `AudioMixer` (Master, Background, SFX, and UI groups), converting between linear slider values and decibels at runtime.

### Architecture & Code Quality

* **ScriptableObjects** are used for initialization/configuration data, decoupling game data from scene-specific logic:

  * `PlayerDataSo`: movement keys, jump force, and jump time.
  * `ScoreData`: tracks current score and high score, persisted via `PlayerPrefs`, and broadcasts change events for UI.
  * `AudioDataSo`: master/background/SFX/UI volume levels, persisted via `PlayerPrefs`, and applied to the shared `AudioMixer`.
* Difficulty ramp and power-up effects are kept fully decoupled: `ObstacleSpawner` ramps a base speed/spawn-rate over elapsed time, while the power-up only ever applies a temporary multiplier on top — the two systems never overwrite each other.
* Marker components (e.g. `ObstacleMarker`, `PowerUpMarker`) are used instead of string-based tags for collision/trigger identification, providing compile-time safety.
* Reusable, single-purpose UI scripts (e.g. `SliderVolumeChannel`, driven by a `Channel` enum, and `ButtonClickSound`, sharing one scene `AudioSource`) avoid duplicating near-identical code across sliders and buttons.

## Controls

| Action | Key |
|-|-|
| Jump | `↑` |
| Crouch | `↓` |

## Credits

* Game Dev/ Game Designer / SFX / Pixel Artist assistant jeje - Yeimy Rojas.
* Game Designer/ Pixel Artist Lead- Aurora Salazar. 

## Notes

During the development of this project, artificial intelligence tools were used as support for resolving questions, understanding concepts, and reviewing code. AI was primarily used as a reference and learning resource, providing explanations of programming structures, syntax, and possible solutions to problems encountered during development. No code was directly copied from AI-generated responses; the code implemented in the project was written and developed by the author based on their own understanding and adapted to the specific needs of the project.

Link Itchio https://yeimy-rojas-midnightbaker.itch.io/plumas-en-fuga

* Contact:
* Yeimy Rojas The Midnight Baker.
https://www.artstation.com/yeimy24401
