using System;

namespace aoc2019
{
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;
            while (running){
                Console.WriteLine("Enter day number or q to quit:");
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
                        Day7.Execute();
                        break;
                    case "8":
                        Day8.Execute();
                        break;
                    case "9":
                        Day9.Execute();
                        break;
                    case "10":
                        Day10.Execute();
                        break;
                    case "11":
                        Day11.Execute();
                        break;
                    case "12":
                        Day12.Execute();
                        break;
                    case "13":
                        Day13.Execute();
                        break;
                    case "14":
                        Day14.Execute();
                        break;
                    case "15":
                        Day15.Execute();
                        break;
                    case "16":
                        Day16.Execute();
                        break;
                    case "17":
                        Day17.Execute();
                        break;
                    case "18":
                        Day18.Execute();
                        break;
                    case "19":
                        Day19.Execute();
                        break;
                    case "20":
                        Day20.Execute();
                        break;
                    case "21":
                        Day21.Execute();
                        break;
                    case "22":
                        Day22.Execute();
                        break;
                    case "23":
                        Day23.Execute();
                        break;
                    case "24":
                        Day24.Execute();
                        break;
                    case "q":
                        running = false;
                        break;
                };
            }
        }
    }
}
