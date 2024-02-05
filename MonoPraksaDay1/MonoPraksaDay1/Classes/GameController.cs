using MonoPraksaDay1.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoPraksaDay1.Classes
{
    internal class GameController
    {
        private Stopwatch gameTimer = new Stopwatch();
        private List<ShuttleEnv> fixedShipPartsList = new List<ShuttleEnv>();
        private Item hammer = new Item("Hammer", "Seems heavy...", true);
        private Item battery = new Item("Battery", "Just an ordinary 9V battery.", true);

        private List<Astronaut> crewList = new List<Astronaut>();

        private ShuttleEnv shuttleWindow;
        private ShuttleEnv shuttleCircuit;

        private void EditMenu(Astronaut astronaut)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"1. Edit name");
            Console.WriteLine($"2. Delete astronaut");
            Console.WriteLine($"3. Go back");
            Console.WriteLine("Please select an option:");

            Console.ForegroundColor = ConsoleColor.White;

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine("Input a name: ");
                    string newName = Console.ReadLine();
                    astronaut.FirstName = newName.Split(' ')[0];
                    astronaut.FirstName = newName.Split(' ')[1];
                    crewList[crewList.IndexOf(astronaut)].FirstName = astronaut.FirstName;
                    crewList[crewList.IndexOf(astronaut)].FirstName = astronaut.LastName;
                    break;
                case 2:
                    crewList.RemoveAt(crewList.IndexOf(astronaut));
                    EditCrew();
                    return;
                case 3:
                    EditCrew();
                    return;
            }
        }
        private void EditCrew()
        {
            while(true)
            {
                if (crewList.Count == 0) return;
                foreach(Astronaut crewMember in crewList)
                {
                    Console.WriteLine($"{crewList.IndexOf(crewMember)}. {crewMember.FirstName} {crewMember.LastName} ({crewMember.ReturnAge()})");
                }
                Console.WriteLine("4. Go back");
                Console.WriteLine("Please select which astronaut you want to edit:");
                int input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        EditMenu(crewList.ElementAt(input));
                        break;
                    case 1:
                        EditMenu(crewList.ElementAt(input));
                        break;
                    case 2:
                        EditMenu(crewList.ElementAt(input));
                        break;
                    case 3:
                        EditMenu(crewList.ElementAt(input));
                        break;
                    case 4:
                        return;
                }
            }
        }

        private void InputCrew()
        {
            Console.Clear();
            Console.WriteLine("Please input 4 of your crewmates:");
            while(crewList.Count != 4)
            {
                Console.WriteLine("Please input a name:");
                string crewmateName = Console.ReadLine();
                int crewmateAge = 0;
                while (crewmateAge == 0)
                {
                    Console.WriteLine($"Please input age for {crewmateName}:");
                    crewmateAge = Convert.ToInt32(Console.ReadLine());
                }

                Astronaut crewmate = new Astronaut(crewmateName.Split(' ')[0], crewmateName.Split(' ')[1], crewmateAge, "Astronaut", 3);
                crewList.Add(crewmate);
            }
        }

        private void MainGame(Astronaut astronaut)
        {
            shuttleWindow = new ShuttleEnv("Window", true, astronaut.ReturnInventory());
            shuttleCircuit = new ShuttleEnv("Circuit", false, astronaut.ReturnInventory());

            List<ShuttleEnv> listOfShuttleEnv = new List<ShuttleEnv>();
            listOfShuttleEnv.Add(shuttleWindow);
            listOfShuttleEnv.Add(shuttleCircuit);

            Dictionary<string, Action<Item>> actions = new Dictionary<string, Action<Item>>
            {
                { "fix circuit",  shuttleCircuit.HandleFix },
                { "break window", shuttleWindow.HandleBreak },
                { "break circuit", shuttleCircuit.HandleBreak },
                { "pick up hammer",  astronaut.PickUp },
                { "pick up battery", astronaut.PickUp }
            };


            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. Look around");
                Console.WriteLine("2. Look outside");
                Console.WriteLine("3. Contact ground controll");
                Console.WriteLine("4. Edit crewmates");
                Console.WriteLine("What do you want to do?");
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine().ToLower();
                try
                {
                    switch(Convert.ToInt32(input))
                    {
                        case 1:
                            Console.Clear();
                            astronaut.LookAround();
                            break;
                        case 2:
                            Console.Clear();
                            astronaut.LookOutside(); 
                            break;
                        case 3:
                            Console.Clear();
                            Random rand = new Random();
                            if (rand.Next(0, 2) != 0)
                            {
                                astronaut.ContactGroundControl(astronaut);
                            }else
                            {
                                astronaut.ContactGroundControl();
                            }
                            break;
                        case 4:
                            Console.Clear();
                            EditCrew();
                            break;
                    }
                }
                catch
                {
                    if (actions.TryGetValue(input, out Action<Item> action))
                    {
                        Console.Clear();
                        if (input.Contains("hammer") || input.Contains("break"))
                            action.Invoke(hammer);
                        else if (input.Contains("battery") || input.Contains("fix"))
                            action.Invoke(battery);
                        CheckGameState(astronaut, listOfShuttleEnv);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You cannot do that!");
                    }
                }
            }
        }

        private void Start()
        {
            Console.Clear();
            gameTimer.Start();
            Console.WriteLine("You were a moon-bound astronaut. " +
                "You boarded the shuttle with excitement and fear. " +
                "You orbited the earth, then switched to the lunar module. " +
                "You said farewell to your pilot, who stayed in the shuttle. " +
                "You saw the earth shrink and the moon grow. You were in awe. " +
                "Almostwere almost there.But then, disaster struck." +
                "You heard a bang, then alarms. You saw sparks and smoke. You felt the module shake.");

            Console.WriteLine("Please enter your full name (ex. John Doe): ");
            string astronautName = Console.ReadLine();
            Console.WriteLine("Please enter your age: ");
            int astronautAge = Convert.ToInt32(Console.ReadLine());
            Astronaut astronaut = new Astronaut(astronautName.Split(' ')[0], astronautName.Split(' ')[1], astronautAge, "Astronaut", 2);

            astronaut.PrintFullInfo();
            MainGame(astronaut);
        }

        private void Help()
        {
            Console.Clear();
            Console.WriteLine("How to play?");
            Console.WriteLine("Use key words to interact with the world: ");
            Console.WriteLine("pick up");
            Console.WriteLine("break");
            Console.WriteLine("fix");
            Console.WriteLine("Have fun!");
        }

        public void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please choose an option from the menu:");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Help");
            Console.WriteLine("3. Quit");
            Console.ForegroundColor = ConsoleColor.White;
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    InputCrew();
                    Start();
                    break;
                case 2:
                    Help();
                    MainMenu();
                    return;
                case 3:
                    Console.WriteLine("Thanks for playing!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }
        }

        private void EndGame(Astronaut astronaut)
        {
            if (astronaut.IsDead)
            {
                Console.Clear();
                gameTimer.Stop();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Game Over!");
                Console.WriteLine("You broke a window and got sucked out into space!");
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Your game lasted {Math.Round(gameTimer.Elapsed.TotalMinutes, 2)} minutes.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                if(fixedShipPartsList.Count > 0)
                {
                    Console.Clear();
                    gameTimer.Stop();
                    Console.WriteLine("You breathe a sigh of relief as you repair the last malfunctioning part of your space shuttle." +
                    "You have survived the most dangerous mission of your life. " +
                    "Now, you can finally head back to your home planet, " +
                    "where your loved ones are waiting for you. " +
                    "You strap yourself in and press the ignition button. " +
                    "The shuttle roars to life and blasts off into the starry sky.");
                    Console.ReadKey();
                    Console.WriteLine($"Your game lasted {gameTimer.Elapsed.TotalMinutes} minutes.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
        }

        public void CheckGameState(Astronaut astronaut, List<ShuttleEnv> listOfEnviroments)
        {
            foreach (ShuttleEnv env in listOfEnviroments)
            {
                if (env.Name == "Window" && env.IsBroken)
                {
                    astronaut.IsDead = true;
                    EndGame(astronaut);
                    return;
                }else
                {
                    if(!env.IsBroken && env.Fixed)
                    {
                        fixedShipPartsList.Add(env);
                    }
                    EndGame(astronaut);
                }
            }
        }
    }
}
