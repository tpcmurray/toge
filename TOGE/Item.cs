using System.Collections.Generic;

namespace TOGE
{
    public abstract class Item
    {
        public string Description { get; set; }
    }

    public class Items
    {
        private List<Item> _items;
    }
}
