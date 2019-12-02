using System;

namespace aoc2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Which day:");
            var day = Console.ReadLine();
            switch(day)
            {
                case "1":
                    Day1.Execute();
                    break;
            };
            Console.WriteLine("Waiting to quit...");
            Console.ReadKey();
        }
    }
}
