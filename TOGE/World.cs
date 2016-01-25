using System.Collections.Generic;

namespace TOGE
{
    public class World
    {
        public World()
        {
            Locations = new Dictionary<string, Location>();
            Actions = new List<Action>();
            Player = new Player();
            IsEnded = false;

            Actions.Add(new Action("N", "North"));
            Actions.Add(new Action("S", "South"));
            Actions.Add(new Action("E", "East"));
            Actions.Add(new Action("W", "West"));
            Actions.Add(new Action("L", "Look"));
        }

        /// <summary>
        /// The text used on game start to introduce the character to the game setup.
        /// </summary>
        public string Introduction { get; set; }

        public Dictionary<string, Location> Locations { get; }
        public List<Action> Actions { get; }
        public Player Player { get; }
        public bool IsEnded { get; set; }
    }
}
