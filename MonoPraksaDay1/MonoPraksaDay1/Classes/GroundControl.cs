using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class GroundControl
    {
        public void Contact(Astronaut astronaut)
        {
            if (astronaut == null) return;
            Console.WriteLine("It seems like...");
            Console.WriteLine("...module suffered a....");
            Console.WriteLine("........");
        }
        
        public void Contact()
        {
            Console.WriteLine("Floating in space...");
            Console.WriteLine("...freezing space....");
            Console.WriteLine("........");
        }
    }
}
