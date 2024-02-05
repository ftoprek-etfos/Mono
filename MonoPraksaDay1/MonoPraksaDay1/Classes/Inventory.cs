using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class Inventory
    {
        public List<Item> Items;

        public Inventory()
        { 
            Items = new List<Item>();
        }
        public void Add(Item item)
        {
            if (item.CanEquip && !Items.Contains(item))
            { 
                Items.Add(item);
                Console.WriteLine($"You picked up a {item.Name}. {item.Description}");
            }
            else
                Console.WriteLine("You cannot pick up that!");
        }
        
        public bool IsEquipped(Item itemToCheck)
        {
            if(Items.Contains(itemToCheck)) return true;
            else
                return false;
        }
    }
}
