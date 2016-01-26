using System;
using System.Collections;
using System.Collections.Generic;

namespace TOGE
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public bool CanBePickedUp { get; set; }
        public string LookDescription { get; set; }
        public abstract void Use(World world);
        public abstract void UseOn(Item item, World world);
        public abstract void Look(World world);
    }

    public class Items
    {
        private readonly Dictionary<string, Item> _items;

        public Items()
        {
            _items = new Dictionary<string, Item>(StringComparer.InvariantCultureIgnoreCase);
        }

        public Item Get(string itemName)
        {
            return _items[itemName];
        }

        public void Add(Item item)
        {
            _items.Add(item.Name, item);
        }

        public bool Contains(string itemName)
        {
            return _items.ContainsKey(itemName);
        }

        public bool Contains(Item item)
        {
            return _items.ContainsKey(item.Name);
        }

        public bool Remove(Item item)
        {
            return _items.Remove(item.Name);
        }

        public ICollection<Item> GetAll()
        {
            return _items.Values;
        }
    }
}
