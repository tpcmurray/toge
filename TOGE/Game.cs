using System;
using System.Collections.Generic;

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
            World.WriteLine(_world.Introduction);
            var newGame = true;

            while (!_world.IsEnded)
            {
                // see if we are in a new place
                if (_world.Player.Location != _currentLocation)
                {
                    // we are in a new place, so describe it
                    World.WriteLine(_world.Player.Location.LookDescription);

                    _currentLocation = _world.Player.Location;
                }

                if (newGame)
                {
                    World.WriteLine("([?] for help)");
                    newGame = false;
                }

                Console.WriteLine("-------------------------------------------------------------------------------");

                // await command
                ProcessInput(Console.ReadLine());
            }

            World.WriteLine(_world.Outro);
            Console.ReadKey();
        }

        private void ProcessInput(string command)
        {
            var input = command.ToLower().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            switch (input[0])
            {
                #region movement
                case "n":
                case "north":
                    if (_currentLocation.North == null)
                    {
                        World.WriteLine("There is no passage to the North.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.NorthIsBlockedText))
                    {
                        World.WriteLine(_currentLocation.NorthIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.North;
                    break;

                case "s":
                case "south":
                    if (_currentLocation.South == null)
                    {
                        World.WriteLine("You don't see a way to go South.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.SouthIsBlockedText))
                    {
                        World.WriteLine(_currentLocation.SouthIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.South;
                    break;

                case "e":
                case "east":
                    if (_currentLocation.East == null)
                    {
                        World.WriteLine("It would be nice to go East, but there's no way.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.EastIsBlockedText))
                    {
                        World.WriteLine(_currentLocation.EastIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.East;
                    break;

                case "w":
                case "west":
                    if (_currentLocation.West == null)
                    {
                        World.WriteLine("You can't go west.");
                        break;
                    }

                    if (!string.IsNullOrEmpty(_currentLocation.WestIsBlockedText))
                    {
                        World.WriteLine(_currentLocation.WestIsBlockedText);
                        break;
                    }

                    _world.Player.Location = _currentLocation.West;
                    break;
                #endregion

                case "?":
                    foreach (var action in _world.Actions)
                    {
                        World.WriteLine("[" + action.CommandLetter + "]" + " " + action.CommandFull);
                    }
                    break;

                case "l":
                case "look":
                    if (input.Length == 1)
                    {
                        World.WriteLine(_world.Player.Location.LookDescription);
                        DescribeDirections();
                        break;
                    }
                    if (input.Length > 2)
                    {
                        World.WriteLine("Too many words, you are confusing yourself. Try just LOOK, or LOOK item");
                        break;
                    }
                    if (_world.Player.Location.Items.Contains(input[1]))
                    {
                        _world.Player.Location.Items.Get(input[1]).Look(_world);
                        break;
                    }
                    if (_world.Player.Inventory.Contains(input[1]))
                    {
                        _world.Player.Inventory.Get(input[1]).Look(_world);
                        break;
                    }
                    
                    World.WriteLine("You look, but you don't see that anywhere.");
                    break;

                case "p":
                case "pickup":
                    if (input.Length == 1)
                    {
                        World.WriteLine("Pickup what?");
                        break;
                    }
                    if (input.Length > 2)
                    {
                        World.WriteLine("Too many words, you are confusing yourself. Try just PICKUP item");
                        break;
                    }
                    if (!_world.Player.Location.Items.Contains(input[1]))
                    {
                        World.WriteLine("And how do you see yourself picking up that exactly?");
                        break;
                    }

                    var pickedUpItem = _world.Player.Location.Items.Get(input[1]);
                    if (!pickedUpItem.CanBePickedUp)
                    {
                        World.WriteLine("Pretty sure that's not something you should pick up.");
                        break;
                    }

                    _world.Player.Location.Items.Remove(pickedUpItem); // remove from location
                    _world.Player.Inventory.Add(pickedUpItem);         // add to player inventory
                    World.WriteLine("You pickup the " + pickedUpItem.Name + ".");
                    break;

                case "d":
                case "drop":
                    if (input.Length == 1)
                    {
                        World.WriteLine("Ok, but drop what?");
                        break;
                    }
                    if (input.Length > 2)
                    {
                        World.WriteLine("Too many words, you are confusing yourself. Try just DROP item");
                        break;
                    }
                    if (!_world.Player.Inventory.Contains(input[1]))
                    {
                        World.WriteLine("But then you realize you can't drop something you don't have.");
                        break;
                    }

                    var droppedItem = _world.Player.Location.Items.Get(input[1]);
                    _world.Player.Inventory.Remove(droppedItem);   // remove from player
                    _world.Player.Location.Items.Add(droppedItem); // add to location
                    World.WriteLine("You pickup the " + droppedItem.Name + ".");
                    break;

                case "i":
                case "inventory":
                    DescribeInventory();
                    break;

                case "u":
                case "use":
                    if (input.Length == 1)
                    {
                        World.WriteLine("Use what?");
                        break;
                    }

                    if (input.Length == 2) // use item
                    {
                        UseOneItem(input);
                        break;
                    }

                    if (input.Length == 4 && input[2].ToLower().Trim() == "on") // use item
                    {
                        UseTwoItems(input);
                        break;
                    }

                    World.WriteLine("Illogical. Type either 'USE item' or 'USE item1 ON item2'");
                    break;

                default:
                    World.WriteLine("You prepare to '" + command + "', but then realize you have no idea idea what '" +
                                      command + "' means.\r\n You must be dazed.");
                    break;
            }
        }

        private void DescribeDirections()
        {
            // describe directions
            if (_currentLocation.North != null)
                World.WriteLine("To the [N]orth you see " + _currentLocation.North.DistanceDescription);
            if (_currentLocation.South != null)
                World.WriteLine("To the [S]outh you see " + _currentLocation.South.DistanceDescription);
            if (_currentLocation.East != null)
                World.WriteLine("To the [E]ast you see " + _currentLocation.East.DistanceDescription);
            if (_currentLocation.West != null)
                World.WriteLine("To the [W]est you see " + _currentLocation.West.DistanceDescription);
        }

        private void DescribeInventory()
        {
            var items = _world.Player.Inventory.GetAll();
            if (items.Count == 0)
            {
                World.WriteLine("Your inventory is empty.");
                return;
            }

            World.WriteLine("In your inventory you carry:");
            foreach (var item in items)
            {
                World.WriteLine(" - " + item.Name);
            }
        }

        private void UseOneItem(IList<string> input)
        {
            // if the player has this item, use it
            if (_world.Player.Inventory.Contains(input[1]))
            {
                _world.Player.Inventory.Get(input[1]).Use(_world);
                return;
            }

            // otherwise maybe the location has the item
            if (_world.Player.Location.Items.Contains(input[1]))
            {
                _world.Player.Location.Items.Get(input[1]).Use(_world);
                return;
            }

            World.WriteLine("You don't see that item anywhere.");
        }

        private void UseTwoItems(IList<string> input)
        {
            Item item1 = null, item2 = null;

            // ensure item1 exists
            if (_world.Player.Inventory.Contains(input[1]))
            {
                item1 = _world.Player.Inventory.Get(input[1]);
            }
            else if (_world.Player.Location.Items.Contains(input[1]))
            {
                item1 = _world.Player.Inventory.Get(input[1]);
            }
            if (item1 == null)
            {
                World.WriteLine("You don't see any kind of " + input[1] + " around here.");
                return;
            }

            // ensure item2 exists
            if (_world.Player.Inventory.Contains(input[3]))
            {
                item2 = _world.Player.Inventory.Get(input[3]);
            }
            else if (_world.Player.Location.Items.Contains(input[3]))
            {
                item2 = _world.Player.Location.Items.Get(input[3]);
            }
            if (item2 == null)
            {
                World.WriteLine("You don't see any kind of " + input[3] + " around here.");
                return;
            }

            item1.UseOn(item2, _world);
        }
    }
}