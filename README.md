# VB Space Shooting Game

A simple arcade-style space shooter built with Visual Basic .NET and Windows Forms. This project follows the "VB Game Programming Chapter 19" concept and implements a classic dodge-and-shoot gameplay loop.

## Features

- Player ship movement with keyboard controls
- Laser firing with the space bar
- Enemy ships that spawn continuously
- Enemy projectiles that damage the player
- Score tracking and health system
- Increasing difficulty as the score rises
- Restart and exit options in the game UI

## Controls

- Left Arrow / A: move left
- Right Arrow / D: move right
- Space: shoot

## Requirements

- Windows 10 or later
- Visual Studio 2022
- .NET desktop development workload
- .NET 10 SDK (project targets `net10.0-windows`)

## Project Files

- `Form1.vb` — main gameplay logic
- `Form1.Designer.vb` — form designer code
- `Form1.resx` — form resources
- `SpaceShootingGame.vbproj` — project configuration
- `My Project/` — Visual Basic project settings and resources

## Running the Game

1. Open `SpaceShootingGame.slnx` in Visual Studio, or open `SpaceShootingGame.vbproj` directly.
2. Restore any required dependencies if prompted.
3. Set the startup project if needed.
4. Press F5 to build and run the application.

## Notes

This is a Windows Forms application, so it is intended to run on Windows. The gameplay is intentionally lightweight and beginner-friendly, making it a good example of VB.NET game development fundamentals.
