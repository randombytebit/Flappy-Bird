<div align="center">

# Flappy Bird Clone

**A 2D side-scrolling Flappy Bird clone built in Unity with sprite animation, parallax scrolling, and full game state management.**

[![Unity](https://img.shields.io/badge/Engine-Unity-000000?style=flat-square&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/Language-C%23-512BD4?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-PC%20%7C%20Mobile-lightgrey?style=flat-square)]()
[![Completed](https://img.shields.io/badge/Completed-January%202025-brightgreen?style=flat-square)]()

</div>

---

## Table of Contents

- [About the Project](#about-the-project)
- [Features](#features)
- [Project Structure](#project-structure)
- [Scripts Overview](#scripts-overview)
- [Getting Started](#getting-started)

---

## About the Project

This is a Unity recreation of Flappy Bird — the iconic side-scrolling mobile game originally developed by Dong Nguyen. The player controls a bird and must navigate through an endless series of pipe obstacles without colliding. The clone replicates the original's core gameplay while implementing clean, modular Unity scripting patterns across player input, procedural pipe spawning, parallax backgrounds, UI management, and scoring.

---

## Features

| Category | Feature |
|---|---|
| **Player Control** | Keyboard (Space), mouse click, and mobile touch input |
| **Animation** | Frame-by-frame sprite cycling for bird flap animation |
| **Parallax Scrolling** | Looping background and ground using material texture offset |
| **Pipe System** | Procedural pipe spawning at randomised heights with automatic cleanup |
| **Collision Detection** | Trigger-based detection for obstacles and scoring zones |
| **Game State** | Play, pause, and game over states managed by a central Game Manager |
| **Scoring** | Real-time score display updated on pipe pass |
| **UI** | Canvas-based UI with score text, play button, and game over screen |
| **Frame Rate** | Locked to 60 FPS for consistent gameplay |

---

## Project Structure

```
Assets/
├── Settings/               # Screen settings
├── Fonts/                  # Bitmap fonts
├── Sprites/                # Bird, pipe, background, and ground sprites
├── Materials/              # Background and ground scrolling materials
├── Prefabs/
│   └── Pipes.prefab        # Pipe obstacle prefab
├── Scripts/
│   ├── Player.cs           # Player input, movement, and collision
│   ├── Parallax.cs         # Background and ground scrolling
│   ├── PipesMovement.cs    # Pipe movement and off-screen cleanup
│   ├── Spawner.cs          # Procedural pipe spawning
│   └── GameManager.cs      # Game state, scoring, and UI control
└── Scenes/
    └── Main.unity          # Main game scene
```

---

## Scripts Overview

### `Player.cs`
Handles player input across keyboard, mouse, and touch. Applies custom gravity and upward impulse on tap. Cycles through sprite frames for flap animation using `InvokeRepeating`. Detects trigger collisions with `Obstacle` and `Scoring` tagged objects to call `GameOver` or `IncreaseScore` on the Game Manager.

### `Parallax.cs`
Attached to both the background and ground Quad objects. Continuously offsets the material's `mainTextureOffset` each frame to produce an infinite scrolling effect without moving the actual geometry.

### `PipesMovement.cs`
Moves each pipe prefab instance leftward at a fixed speed. Calculates the left edge of the screen in world space on `Start` and destroys the pipe GameObject once it passes off-screen to manage memory.

### `Spawner.cs`
Uses `InvokeRepeating` to instantiate pipe prefabs at a set interval. Randomises vertical position within a configurable `minHeight` / `maxHeight` range. Disables spawning automatically when the Game Manager pauses the game via `OnDisable`.

### `GameManager.cs`
Central controller for game flow. Manages `Time.timeScale` to pause and resume the game, resets score and destroys existing pipes on play, and toggles UI elements (play button, game over image, score text) based on the current game state.

---

## Tag Reference

| Tag | Applied To | Purpose |
|---|---|---|
| `Player` | Bird GameObject | Identifies the player object |
| `Obstacle` | Top pipe, bottom pipe, ground | Triggers game over on contact |
| `Scoring` | Middle pipe gap trigger | Increments score on pass |

---

## Getting Started

### Prerequisites

- [Unity](https://unity.com/download) 2021.x or later (2D module required)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/randombytebit/Flappy-Bird.git
   ```

2. **Import assets**

   Download the sprite assets from the original tutorial repository:
   ```
   https://github.com/zigurous/unity-flappy-bird-tutorial
   ```
   Place `.png` sprite files into `Assets/Sprites/` and font files into `Assets/Fonts/`.

3. **Configure sprite import settings**

   For the bird sprite, set the following in the Inspector:
   - Pixels Per Unit: `24`
   - Filter Mode: `Point (no filter)`
   - Max Size: `256`
   - Format: `RGBA 32 bit`

4. **Open the project in Unity**

   Launch Unity Hub, click **Open**, and select the cloned project folder.

5. **Open the Main scene and press Play.**

---

<div align="center">

Flappy Bird Clone · Built with Unity · Completed January 2025

</div>
