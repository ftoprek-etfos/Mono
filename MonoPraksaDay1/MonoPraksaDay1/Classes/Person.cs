using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    abstract class Person
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        private int Age { get; set; }
        private string Proffesion { get; set; }

        public Person(string firstName, string lastName, int age, string proffesion)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Proffesion = proffesion;
        }

        public virtual void PrintFullInfo()
        {
            Console.WriteLine($"My name is {FirstName} {LastName}");
        }

        public int ReturnAge()
        {
            return Age;
        }

        public virtual string ReturnProffesion()
        {
            return Proffesion;
        }
    }
}
