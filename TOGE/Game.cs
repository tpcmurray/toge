using System;

namespace TOGE
{
    public class Game
    {
        private readonly World _world;
        private Location _currentLocation;

        public Game(World world)
        {
            _world = world;
            _currentLocation = null;
        }

        public void Start()
        {
            Console.WriteLine(_world.Introduction);
            var newGame = true;

            while (!_world.IsEnded)
            {
                // see if we are in a new place
                if (_world.Player.Location != _currentLocation)
                {
                    // we are in a new place, so describe it
                    Console.WriteLine(_world.Player.Location.LookDescription);

                    _currentLocation = _world.Player.Location;

                    DescribeDirections();
                }

                if (newGame)
                {
                    Console.WriteLine("([?] for help)");
                    newGame = false;
                }

                // await command
                ProcessInput(Console.ReadLine());
            }
        }

        private void ProcessInput(string command)
        {
            switch (command.ToLower())
            {
                case "?":
                    foreach (var action in _world.Actions)
                    {
                        Console.WriteLine("[" + action.CommandLetter + "]" + " " + action.CommandFull);
                    }
                    break;
                case "n":
                case "north":
                    if (_currentLocation.North == null)
                    {
                        Console.WriteLine("There is no passage to the North.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.NorthIsBlockedText))
                    {
                        Console.WriteLine(_currentLocation.NorthIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.North;
                    break;
                case "s":
                case "south":
                    if (_currentLocation.South == null)
                    {
                        Console.WriteLine("You don't see a way to go South.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.SouthIsBlockedText))
                    {
                        Console.WriteLine(_currentLocation.SouthIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.South;
                    break;
                case "e":
                case "east":
                    if (_currentLocation.East == null)
                    {
                        Console.WriteLine("It would be nice to go East, but there's no way.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.EastIsBlockedText))
                    {
                        Console.WriteLine(_currentLocation.EastIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.East;
                    break;
                case "w":
                case "west":
                    if (_currentLocation.West == null)
                    {
                        Console.WriteLine("You can't go west.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.WestIsBlockedText))
                    {
                        Console.WriteLine(_currentLocation.WestIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.West;
                    break;
                case "l":
                case "look":
                    Console.WriteLine(_world.Player.Location.LookDescription);
                    DescribeDirections();
                    break;
                default:
                    Console.WriteLine("You prepare to '" + command + "', but then realize you have no idea idea what '" +
                                      command + "' means.\r\n You must still be dazed.");
                    break;
            }
        }

        private void DescribeDirections()
        {
            // describe directions
            if (_currentLocation.North != null)
                Console.WriteLine("To the [N]orth you see " + _currentLocation.North.DistanceDescription);
            if (_currentLocation.South != null)
                Console.WriteLine("To the [S]outh you see " + _currentLocation.South.DistanceDescription);
            if (_currentLocation.East != null)
                Console.WriteLine("To the [E]ast you see " + _currentLocation.East.DistanceDescription);
            if (_currentLocation.West != null)
                Console.WriteLine("To the [W]est you see " + _currentLocation.West.DistanceDescription);
        }
    }
}