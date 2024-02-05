using MonoPraksaDay1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class Astronaut : Person, IPlayable
    {
        private int Rank {  get; set; }
        public bool IsDead { get; set; } = false;
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override int Age { get; set; }
        public override string Proffesion { get; set; }

        Inventory inventory = new Inventory();

        public Astronaut(string firstName, string lastName, int age, string proffesion, int rank) : base(firstName, lastName, age, proffesion)
        {
            this.Rank = rank;
        }

        public override void PrintFullInfo()
        {
            Console.WriteLine($"My name is {FirstName} {LastName} ({ReturnAge()}) and {ReturnProffesion()}. My rank is {Rank}");
        }

        public override string ReturnProffesion()
        {
            return "I am an astronaut";
        }

        public void ContactGroundControl(Astronaut astronaut)
        {
            GroundControl groundControl = new GroundControl();
            groundControl.Contact(astronaut);
        }

        public void ContactGroundControl()
        {
            GroundControl groundControl = new GroundControl();
            groundControl.Contact();
        }

        public void LookOutside()
        {
            Console.WriteLine("As you gaze at the moon's hidden face, " +
                "shrouded in mystery and shadows, " +
                "you hear a faint buzz in your ears. " +
                "It sounds like a cosmic whisper, " +
                "a secret message from the stars.");
        }

        public void LookAround()
        {
            Console.WriteLine("A lone hammer next to a battery lies on the dusty floor, " +
                "forgotten and useless. Behind you, " +
                "a faint light casts a bizarre shadow of your body on the ground. " +
                "It looks like a twisted creature, ready to pounce. " +
                "To your left, a small circuit seems like it has malfunctioned" +
                "To your right, a small window offers a glimpse of the outside world.");
        }

        public void PickUp(Item item)
        {
            inventory.Add(item);
        }

        public Inventory ReturnInventory()
        {
            return inventory;
        }

        public override int ReturnAge()
        {
            return Age;
        }
    }
}
