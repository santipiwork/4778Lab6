using System;
using System.Collections.Generic;

namespace Zork
{
    internal class Program
    {
        private static readonly Room[,] Rooms =
        {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH, Commands.SOUTH, Commands.EAST, Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);

        private static Room CurrentRoom => Rooms[Location.Row, Location.Column];

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescriptions();
            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                if (CurrentRoom != previousRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");
                string inputString = Console.ReadLine();

                if (inputString == null)
                {
                    break;
                }

                command = ToCommand(inputString.Trim());

                switch (command)
                {
                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        Console.WriteLine(Move(command) ? $"You moved {command}." : "The way is shut!");
                        break;

                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private static Commands ToCommand(string commandString) =>
            Enum.TryParse(commandString, true, out Commands result)
                && Enum.IsDefined(typeof(Commands), result)
                && string.Equals(commandString, result.ToString(), StringComparison.OrdinalIgnoreCase)
                ? result
                : Commands.UNKNOWN;

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    return false;
            }

            return true;
        }

        private static void InitializeRoomDescriptions()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                roomMap.Add(room.Name, room);
            }

            roomMap["Rocky Trail"].Description = "A rough trail winds between rocks at the edge of the forest.";
            roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
            roomMap["Canyon View"].Description = "You stand at a viewpoint overlooking a deep canyon. Cliffs rise above the distant river.";
            roomMap["Forest"].Description = "Tall trees surround you. Sunlight filters through the leaves onto the forest floor.";
            roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
            roomMap["Behind House"].Description = "You are behind the white house. A small window is set into the wall.";
            roomMap["Dense Woods"].Description = "The trees grow close together here, shading a narrow path through the dense woods.";
            roomMap["North of House"].Description = "You are on the north side of the white house. The forest lies nearby.";
            roomMap["Clearing"].Description = "You are in a grassy clearing surrounded by trees. The sky is open above you.";
        }
    }
}
