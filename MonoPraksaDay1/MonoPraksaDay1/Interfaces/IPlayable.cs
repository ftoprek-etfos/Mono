using MonoPraksaDay1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Interfaces
{
    internal interface IPlayable
    {
        bool IsDead { get; set; }
        void ContactGroundControl(Astronaut astronaut);
        void LookOutside();
        void LookAround();
        void PickUp(Item item);
    }
}
