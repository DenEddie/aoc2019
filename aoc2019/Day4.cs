using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public static class Day4
    {
        private static int Begin => 147981;
        private static int End => 691423;


        public static void Execute()
        {
            var passwordRange = Enumerable.Range(Begin, End - Begin);
            var possiblePasswordsPart1 = passwordRange.AsParallel().Where(CompareDigits).ToList();
            Console.WriteLine($"Number of possible passwords part1: {possiblePasswordsPart1.Count}");
            var possiblePasswordsPart2 = possiblePasswordsPart1.AsParallel().Where(OnlyDoubleDigits).ToList();
            Console.WriteLine($"Number of possible passwords part2: {possiblePasswordsPart2.Count}");
        }

        private static bool OnlyDoubleDigits(int value)
        {
            var digits = GetDigits(value).ToList();
            var index = 0;
            var hasDouble = false;

            while(index < digits.Count-1 && !hasDouble)
            {
                var currentIsSame = digits[index] == digits[index + 1];
                var previousIsNotDouble = index == 0 || digits[index] != digits[index - 1];
                var nextIsNotDouble = index == digits.Count - 2 || digits[index + 1] != digits[index + 2];
                hasDouble = currentIsSame && previousIsNotDouble && nextIsNotDouble;
                index++;
            }
            return hasDouble;
        }

        private static bool CompareDigits(int value)
        {
            var increasing = true;
            var hasDoubleDigit = false;
            var current = value % 10;
            do
            {
                value /= 10;
                var previous = value % 10;
                increasing = current >= previous;
                hasDoubleDigit = hasDoubleDigit || current == previous;
                current = previous;
            } while (value > 0 && increasing);
            return increasing&&hasDoubleDigit;
        }
        private static IEnumerable<int> GetDigits(int source)
        {
            while (source > 0)
            {
                var digit = source % 10;
                source /= 10;
                yield return digit;
            }
        }
    }
}
