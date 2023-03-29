using System;

namespace PersonLibrary
{
    public class Person
    {
        public string Name { get; set; }

        public int Cash { get; set; }

        public Person() { }

        public Person(string name, int cash)
        {
            Name = name;
            Cash = cash;
        }

        public override string ToString()
        {
            return $"{Name} has ${Cash}.";
        }

        public int GiveCash(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"{Name} says: {amount} isn't a valid amount");

                return 0;
            }
            else if (amount > Cash)
            {
                Console.WriteLine($"{Name} says: I don't have enough cash to give you ${amount}");

                return 0;
            }

            Cash -= amount;

            return amount;
        }

        public void ReceiveCash(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"{Name} says: {amount} isn't an amount I'll take");
            }

            Cash += amount;
        }
    }
}