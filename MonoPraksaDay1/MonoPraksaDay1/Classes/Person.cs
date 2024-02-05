using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    abstract class Person
    {
        public abstract string FirstName {  get; set; }
        public abstract string LastName { get; set; }
        public abstract int Age { get; set; }
        public abstract string Proffesion { get; set; }

        public Person(string firstName, string lastName, int age, string proffesion)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Proffesion = proffesion;
        }

        public abstract void PrintFullInfo();

        public abstract int ReturnAge();

        public abstract string ReturnProffesion();
    }
}
