# Zork

C# console application for the Zork tutorial assignment.

## Progress

- Step 1: display `Welcome to Zork!`.
- Step 2: a separate gameplay exercise in the original Zork.
- Step 3: accept player input for `LOOK` and `QUIT`.
- Step 4: convert trimmed input to a `Commands` enum using case-insensitive `Enum.TryParse`.
- Step 5: repeat input and command processing until `QUIT`.
- Step 6: move east and west through five rooms, starting at West of House.
- Step 7: expand to the tutorial's 3×3 room grid with movement in all four directions.
- Step 8: use tuple coordinates, a `CurrentRoom` property, guarded switch cases,
  a direction list, and a debug-only assertion helper.
- Step 9: represent each room with a `Room` object containing its name and description.
- Step 10: initialize descriptions through a dictionary keyed by room name and
  automatically display the description on arrival in a room.

The required implementation ends at Step 10. Steps 11–15 are not included.
Step 2 is a personal gameplay exercise to complete separately.

The room layout matches the supplied tutorial screenshot. Most description text
was absent from the pasted material, so descriptions for those rooms were written
for this project rather than transcribed from the tutorial screenshots.

## Run

Install the .NET 8 SDK, then run from the repository root:

```sh
dotnet run --project Zork/Zork.csproj
```

Example interaction:

```text
Welcome to Zork!
West of House
This is an open field west of a white house, with a boarded front door.
A rubber mat saying 'Welcome to Zork!' lies by the door.
> west
You moved WEST.
Forest
Tall trees surround you. Sunlight filters through the leaves onto the forest floor.
> west
The way is shut!
Forest
> quit
Thank you for playing!
```

The player starts at West of House, row 1 and column 1. NORTH increases the row
index, SOUTH decreases it, EAST increases the column, and WEST decreases it.
Movement beyond the grid is blocked with `The way is shut!`. Unrecognized commands print
`Unknown command.`. The current room is displayed before each prompt.
Descriptions appear at startup and after successful movement. Blocked movement
and unknown commands do not trigger another description. `LOOK` always displays
the current room's description. Commands ignore case and surrounding whitespace.

## Verification

The C# source compiled with PowerShell 7's bundled Roslyn compiler. Checks passed
for all 36 room/direction combinations, room descriptions, the debug assertion,
case-insensitive input with whitespace, invalid commands, manual LOOK, automatic
LOOK on initial entry and room changes, and quitting. Blocked movement and invalid
commands were checked to ensure they do not repeat the description.

A standard `dotnet build` has not been verified because the development machine
has .NET runtimes installed but no .NET SDK.
