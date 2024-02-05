using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanEquip { get; }

        public Item(string name, string description, bool canEquip)
        {
            Name = name;
            Description = description;
            CanEquip = canEquip;
        }
    }
}
