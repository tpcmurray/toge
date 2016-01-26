using System.Collections.Generic;

namespace TOGE
{
    public class Player
    {
        public Player()
        {
            Inventory = new Items();
        }
        public Location Location { get; set; }
        public Items Inventory { get; private set; }
    }
}
