using System;
using PersonLibrary;

namespace Gamble
{
    internal class Program
    {
        private static void Main()
        {
            var random = new Random();

            const double odds = 0.75;
            
            var player = new Person("Michael", 100);

            Console.WriteLine($"Welcome to the casino. The odds are {odds}");
            Console.WriteLine();

            while (player.Cash > 0)
            {
                Console.WriteLine(player);
                Console.Write("How much do you want to bet: ");
                
                if (int.TryParse(Console.ReadLine(), out var bet))
                {
                    if (bet > 0)
                    {
                        var number = random.NextDouble();

                        if (number > odds)
                        {
                            Console.WriteLine($"You win {bet * 2}!");
                            Console.WriteLine();
                            player.ReceiveCash(bet);

                            continue;
                        }

                        Console.WriteLine("Bad luck, you lose.");
                        Console.WriteLine();
                        player.GiveCash(bet);

                        continue;
                    }
                }

                Console.WriteLine($"{bet} isn't a valid amount");
            }

            Console.WriteLine("The house always wins.");
        }
    }
}
