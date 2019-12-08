using System;

namespace aoc2019
{
    class Program
    {
        static void Main(string[] args)
        {
            do {
                Console.WriteLine("Which day:");
                var day = Console.ReadLine();
                switch (day)
                {
                    case "1":
                        Day1.Execute();
                        break;
                    case "2":
                        Day2.Execute();
                        break;
                    case "3":
                        Day3.Execute();
                        break;
                    case "4":
                        Day4.Execute();
                        break;
                    case "5":
                        Day5.Execute();
                        break;
                    case "6":
                        Day6.Execute();
                        break;
                    case "7":
                        //Day7.Execute();
                        break;
                    case "8":
                        Day8.Execute();
                        break;
                };
                Console.WriteLine(@"Press q to quit or any key to retry...");
            }while(Console.ReadKey().KeyChar != 'q');
        }
    }
}
