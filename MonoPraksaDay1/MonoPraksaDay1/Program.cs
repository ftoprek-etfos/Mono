using MonoPraksaDay1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameController gameController = new GameController();
            gameController.MainMenu();
            Console.ReadKey();
        }

    }
}
