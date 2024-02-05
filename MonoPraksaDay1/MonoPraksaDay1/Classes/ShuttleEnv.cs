using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class ShuttleEnv
    {
        public string Name {  get; set; }
        public bool CanBreak { get; } = false;
        public bool IsBroken { get; set; } = false;
        public bool Fixed { get; set; } = false;
        private Inventory Inventory { get; set; }
        public ShuttleEnv(string name, bool canBreak, Inventory inventory)
        {
            Name = name;
            CanBreak = canBreak;
            Inventory = inventory;
        }

        public void HandleFix(Item item)
        {
            if (Inventory.IsEquipped(item) && item.Name == "Battery")
            {
                Fixed = true;
            }
            else
                Console.WriteLine("You need a battery to do that!");
        }

        public void HandleBreak(Item item)
        {
            if (Inventory.IsEquipped(item) && item.Name == "Hammer")
            {
                IsBroken = true;

                if (Name == "Circuit")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOU ARE DOOMED! YOU BROKE THE CIRCUIT! THERE IS NO WAY HOME!");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
            }
            else Console.WriteLine("You need a hammer to do that!");
        }
    }
}
