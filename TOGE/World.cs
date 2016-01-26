using System;
using System.Collections.Generic;

namespace TOGE
{
    public class World
    {
        public World()
        {
            Locations = new Dictionary<string, Location>(StringComparer.InvariantCultureIgnoreCase);
            ItemStore = new Items();
            Actions = new List<Action>();
            Player = new Player();
            IsEnded = false;

            Actions.Add(new Action("N", "NORTH"));
            Actions.Add(new Action("S", "SOUTH"));
            Actions.Add(new Action("E", "EAST"));
            Actions.Add(new Action("W", "WEST"));
            Actions.Add(new Action("L", "either 'LOOK' or 'LOOK item'"));
            Actions.Add(new Action("P", "PICKUP item"));
            Actions.Add(new Action("D", "DROP item"));
            Actions.Add(new Action("I", "INVENTORY"));
            Actions.Add(new Action("U", "either 'USE item' or 'USE item1 ON item2'"));
        }

        /// <summary>
        /// The text used on game start to introduce the character to the game setup.
        /// </summary>
        public string Introduction { get; set; }

        public Dictionary<string, Location> Locations { get; set; }
        public Items ItemStore { get; set; }
        public List<Action> Actions { get; set; }
        public Player Player { get; set; }
        public bool IsEnded { get; set; }

        public static void WriteLine(string text)
        {
            Console.WriteLine("  " + text);
        }
    }
}
