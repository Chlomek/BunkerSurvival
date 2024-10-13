using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerSurvival
{
    internal class Program
    {
        static int health = 100;
        static List<string> list = new List<string>();
        static int daysSurvived = 0;
        static Random rnd = new Random();


        static void Main(string[] args)
        {
            StartGame();
            Console.ReadLine();

            while (health > 0)
            {
                Console.Clear();

                Inventory();
                Console.WriteLine($"You have {health} health left.");
                Console.ReadLine();
                NextAction();

                int rng = rnd.Next(1, 100);
                if (rng < 45)
                {
                    //plus supply
                    GoodEvent();
                }
                else if (rng < 80)
                {
                    //minus supply
                    BadEvent();
                }
                else
                {
                    //minus health
                    HealthEvent();
                }
                daysSurvived++;
                Console.WriteLine($"You have survived {daysSurvived} days.");

                if(OutOfSupply())
                {
                    break;
                }
            }
            Console.WriteLine("You have died.");
            Console.ReadLine();
            while(true)
            {
                Console.ReadLine();
            }
        }

        static void AddSupply(string item)
        {
            list.Add(item);
        }

        static void RemoveSupply(string item)
        {
            list.Remove(item);
        }

        static void GoodEvent()
        {
            int rng = rnd.Next(1, 110);

            if (rng < 35)
            {
                Console.WriteLine("You have found food!");
                AddSupply("Food");
            }
            else if (rng < 35)
            {
                Console.WriteLine("You have found water!");
                AddSupply("Water");
            }
            else if (rng < 90)
            {
                Console.WriteLine("You have found ammo!");
                AddSupply("Ammo");
            }
            else
            {
                Console.WriteLine("You have found medicine!");
                AddSupply("Medicine");
            }
            Console.ReadLine();
        }

        static void BadEvent()
        {
            int rng = rnd.Next(1, 100);
            int loseItem = rnd.Next(0, list.Count);

            if (rng < 50)
            {
                Console.WriteLine("You have been attacked by raiders!");
                Console.WriteLine($"You have lost {list[loseItem]}.");
                RemoveSupply(list[loseItem]);
            }
            else if (rng < 80)
            {
                Console.WriteLine("There was a thunder.");
                Console.WriteLine($"You have lost {list[loseItem]}.");
                RemoveSupply(list[loseItem]);
            }
            else
            {
                Console.WriteLine("A rat took away some supply");
                Console.WriteLine($"You have lost {list[loseItem]}.");
                RemoveSupply(list[loseItem]);
            }
            Console.ReadLine();
        }

        static void HealthEvent()
        {
            Console.WriteLine("You have been attacked by a wolf!");
            Console.WriteLine("Do you want to use your ammo? (y/n)");
            string answer = Console.ReadLine();
            if (answer == "n")
            {
                health -= 20;
                Console.WriteLine($"You have {health} health left.");
            }
            else
            {   
                if (list.Contains("Ammo"))
                {
                    Console.WriteLine("You have succesfully defended yourself.");
                    RemoveSupply("Ammo");
                }
                else
                {
                    Console.WriteLine("You don't have any ammo.");
                    health -= 20;
                    Console.WriteLine($"You have {health} health left.");
                }
            }
            Console.ReadLine();
        }

        static void Inventory()
        {
            int foodCount = list.Count(x => x == "Food");
            int waterCount = list.Count(x => x == "Water");
            int medicineCount = list.Count(x => x == "Medicine");
            int ammoCount = list.Count(x => x == "Ammo");

            Console.WriteLine("You have the following supply:");
            Console.WriteLine($"Food: {foodCount}");
            Console.WriteLine($"Water: {waterCount}");
            Console.WriteLine($"Medicine: {medicineCount}");
            Console.WriteLine($"Ammo: {ammoCount}");
        }

        static void NextAction()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Heal");
            Console.WriteLine("2. Continue");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                Heal();
            }
        }

        static void Heal()
        {
            if(list.Contains("Medicine"))
            {
                RemoveSupply("Medicine");
                health += 30;
                if (health > 100)
                {
                    health = 100;
                }
                Console.WriteLine("You healed yourself.");
                Console.WriteLine($"You have {health} health left.");
                Console.ReadLine();
            }
        }

        static bool OutOfSupply()
        {
            int foodCount = list.Count(x => x == "Food");
            int waterCount = list.Count(x => x == "Water");

            if (foodCount == 0)
            {
                Console.WriteLine("You ran out of food.");
                return true;
            }
            else if (waterCount == 0)
            {
                Console.WriteLine("You ran out of water.");
                return true;
            }
            else
            {
                return false;
            }
        }

        static void StartGame()
        {
            Console.WriteLine("You are in a bunker. You have to survive as long as you can.");
            Console.WriteLine("You have to find supply to survive.");

            AddSupply("Food");
            AddSupply("Food");

            AddSupply("Water");
            AddSupply("Water");

            AddSupply("Medicine");
            AddSupply("Ammo");
        }
    }
}
