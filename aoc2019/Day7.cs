using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public static class Day7
    {
        public static void Execute()
        {
            Console.WriteLine($"running test 1 returns: {Execute(TestIntCode1, Phases, 0)}");
            Console.WriteLine($"running test 2 returns: {Execute(TestIntCode2, Phases, 0)}");
            Console.WriteLine($"running test 3 returns: {Execute(TestIntCode3, Phases, 0)}");
            Console.WriteLine($"running input returns: {Execute(IntCode, Phases, 0)}");
            Console.WriteLine($"running test 4 with feedback returns: {Execute(TestIntCode4, FeedbackPhases, 0)}");
            Console.WriteLine($"running test 5 with feedback returns: {Execute(TestIntCode5, FeedbackPhases, 0)}");
            Console.WriteLine($"running input with feedback returns: {Execute(IntCode, FeedbackPhases, 0)}");
        }

        private static long Execute(List<long> intCode, List<long> phases, long input)
        {
            var phaseCombinations = phases.Permute().Select(pc => pc.ToList());
            long maxResult = 0;

            foreach (var combination in phaseCombinations)
            {
                var result = input;
                List<IntCodeComputer> amplifiers = combination.Select(p => new IntCodeComputer(intCode)).ToList();

                var index = 0;
                while (!amplifiers[index].Halted)
                {
                    if (combination.Any())
                    {
                        result = amplifiers[index].Execute(new List<long> { combination[0], result });
                        combination.RemoveAt(0);
                    }
                    else
                    {
                        result = amplifiers[index].Execute(new List<long> { result });
                    }
                    index = index < amplifiers.Count - 1 ? index + 1 : 0;
                }
                if (maxResult < result)
                {
                    maxResult = result;
                }
            }
            return maxResult;
        }

        private static List<long> Phases = new List<long> { 0, 1, 2, 3, 4 };
        private static List<long> FeedbackPhases = new List<long> { 5, 6, 7, 8, 9 };


        private static List<long> TestIntCode1 = new List<long> { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 };

        private static List<long> TestIntCode2 = new List<long> { 3, 23, 3, 24, 1002, 24, 10, 24, 1002, 23, -1, 23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0 };

        private static List<long> TestIntCode3 = new List<long> { 3, 31, 3, 32, 1002, 32, 10, 32, 1001, 31, -2, 31, 1007, 31, 0, 33, 1002, 33, 7, 33, 1, 33, 31, 31, 1, 32, 31, 31, 4, 31, 99, 0, 0, 0 };

        private static List<long> TestIntCode4 = new List<long> { 3, 26, 1001, 26, -4, 26, 3, 27, 1002, 27, 2, 27, 1, 27, 26, 27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5 };
        private static List<long> TestIntCode5 = new List<long> { 3, 52, 1001, 52, -5, 52, 3, 53, 1, 52, 56, 54, 1007, 54, 5, 55, 1005, 55, 26, 1001, 54, -5, 54, 1105, 1, 12, 1, 53, 54, 53, 1008, 54, 0, 55, 1001, 55, 1, 55, 2, 53, 55, 53, 4, 53, 1001, 56, -1, 56, 1005, 56, 6, 99, 0, 0, 0, 0, 10 };

        private static List<long> IntCode => new List<long> { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 46, 67, 76, 101, 118, 199, 280, 361, 442, 99999, 3, 9, 1002, 9, 4, 9, 1001, 9, 2, 9, 102, 3, 9, 9, 101, 3, 9, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 3, 9, 102, 2, 9, 9, 1001, 9, 2, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 1002, 9, 5, 9, 101, 5, 9, 9, 1002, 9, 4, 9, 101, 5, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 1001, 9, 5, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99 };
    }
}
