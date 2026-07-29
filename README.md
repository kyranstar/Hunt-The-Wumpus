# Hunt The Wumpus

**A neon, Pac-Man-skinned arcade take on the classic *Hunt the Wumpus* — stalk a glowing Pac-Man through a procedurally generated hex cave, dodge pits and bats, and answer trivia to cheat death.**

<p align="center">
  <img src="Final%20Game/Content/Images/menu%20art.png" width="560" alt="Hunt the PacMan title screen: neon arcade lettering beside a Pac-Man ghost and a Pac-Man chasing a trail of dots">
</p>

You play a neon ghost hunting the Wumpus — reimagined here as Pac-Man — across a randomly generated 30-room hexagonal cave. Each turn you're told which dangers lurk next door; you map the cave by inference, then loose one well-aimed arrow to win. Built in C# on MonoGame (the open-source XNA framework) for Windows, as an entry in Microsoft's high-school Hunt the Wumpus competition.

<p align="center">
  <img src="Final%20Game/Content/Images/Character.png" width="150" alt="The player: a red neon ghost">
  &nbsp;&nbsp;
  <img src="Final%20Game/Content/Images/Wumpus.png" width="150" alt="The Wumpus, drawn as a yellow neon Pac-Man">
  &nbsp;&nbsp;
  <img src="Final%20Game/Content/Images/RoomBase.png" width="150" alt="A cracked-stone hexagonal cave room">
</p>

## Features

- **Classic hunt-the-wumpus hazards** — each cave hides two bottomless pits, two super-bat nests, and one Wumpus. Wander into the bats and they fling you to a random safe room; the pits and the Wumpus are deadly.
- **Sensing by inference** — before every move you're warned when a pit, bat, or the Wumpus sits in an adjacent room, so you deduce the layout without ever seeing it.
- **Shoot to win** — you carry a limited quiver (three arrows to start) and fire into a neighbouring room; strike the Wumpus and you win, miss and the arrow is spent — run dry and it's game over.
- **Trivia twist** — step onto a pit or into the Wumpus and, instead of instant death, you're thrown a multiple-choice trivia gauntlet; answer enough correctly to scramble out of the pit or drive the Wumpus off. Gold picked up around the cave buys extra arrows and hints, one trivia round at a time.
- **Procedural hex caves & a roaming Wumpus** — every game builds a fresh 30-room hex cave with validated connectivity, and the Wumpus pathfinds around it with active/passive behaviour rather than sitting still.
- **Arcade presentation** — neon sprites, an arcade-cabinet frame, a particle system, fog of war, and a camera that tracks you through the tunnels, all driven by the MonoGame content pipeline.

## Run it

Windows-only: this targets MonoGame with the XNA API, so it does not build on macOS or Linux.

```text
# Requirements: Windows, Visual Studio 2015 (or later) + MonoGame
# 1. Open the solution:
Final Game/HuntTheWumpus.sln
# 2. Set "Desktop" as the startup project.
# 3. Build and run (F5). The MonoGame Content Pipeline (Content.mgcb)
#    compiles the art and fonts on build.
```

There are also Windows Store / Windows Phone heads under `Final Game/Platform/ModernWindows`, and a test project under `Final Game/Test`.

## How it works

A `GameController` owns a `Map`, which wraps a `Cave` of hexagonal rooms produced by `MapGenerator` (30 rooms, two pits, two bats, gold in every room). Every player move also ticks the Wumpus, and `Map.GetPlayerWarnings()` reports hazards in adjacent rooms for the sensing HUD. Stepping onto a hazard raises a trivia round through `GameController`, which grades your answers and ends the game on failure; a clean arrow hit on the Wumpus is the only win. Rendering is split across `MapRenderer`, `HUDRenderer`, a particle system and fog-of-war layer, with the menus and overlays built on the EmptyKeys XAML UI toolkit. A hidden developer "god mode" console (teleport, room info, view toggles) lives under `Shared Code/Game Control/God Mode`.

Tech: C#, MonoGame (XNA), EmptyKeys UI, .NET Framework 4.5.
