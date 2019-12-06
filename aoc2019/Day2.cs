using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aoc2019
{
    public static class Day2
    {
        public static void Execute()
        {
            var part1 = IntCodeComputer(12, 2);
            Console.WriteLine($"Running with 1202 returns: {part1}");
            Console.WriteLine($"To get the result 19690720, we need the verb and noun: {CalculateNounAndVerb()}");
        }

        private static List<int> SetInput(int noun, int verb)
        {
            var beforeFault = new List<int>(IntCode);
            beforeFault[1] = noun;
            beforeFault[2] = verb;
            return beforeFault;
        }

        private static int CalculateNounAndVerb()
        {
            var realNoun = 0;
            var realVerb = 0;

            Parallel.For(0, 9999, (index, state) =>
            {
                var noun = index / 100;
                var verb = index % 100;
                var result = IntCodeComputer(noun, verb);
                if (result == 19690720)
                {
                    realNoun = noun;
                    realVerb = verb;
                    state.Break();
                }
            });

            return (realNoun * 100) + realVerb;
        }

        private static int IntCodeComputer(int noun, int verb)
        {
            var program = SetInput(noun, verb);
            var instructionIndex = 0;
            var instruction = GetInstruction(program[instructionIndex]);
            while (instructionIndex < program.Count && instruction != 99)
            {
                instructionIndex = instruction switch
                {
                    1 => Add(program, instructionIndex),
                    2 => Multiply(program, instructionIndex),
                    _ => throw new Exception("Something went wrong!!")
                };
                instructionIndex++;
                instruction = GetInstruction(program[instructionIndex]);
            }
            return program[0];
        }

        private static int Multiply(List<int> program, int index)
        {
            program[program[index + 3]] = program[program[index + 1]] * program[program[index + 2]];
            return index + 3;
        }

        private static int Add(List<int> program, int index)
        {
            program[program[index + 3]] = program[program[index + 1]] + program[program[index + 2]];
            return index + 3;
        }

        private static int GetInstruction(int value)
        {
            return value % 100;
        }

        private static List<int> IntCode => new List<int> { 1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 10, 1, 19, 1, 5, 19, 23, 1, 23, 5, 27, 2, 27, 10, 31, 1, 5, 31, 35, 2, 35, 6, 39, 1, 6, 39, 43, 2, 13, 43, 47, 2, 9, 47, 51, 1, 6, 51, 55, 1, 55, 9, 59, 2, 6, 59, 63, 1, 5, 63, 67, 2, 67, 13, 71, 1, 9, 71, 75, 1, 75, 9, 79, 2, 79, 10, 83, 1, 6, 83, 87, 1, 5, 87, 91, 1, 6, 91, 95, 1, 95, 13, 99, 1, 10, 99, 103, 2, 6, 103, 107, 1, 107, 5, 111, 1, 111, 13, 115, 1, 115, 13, 119, 1, 13, 119, 123, 2, 123, 13, 127, 1, 127, 6, 131, 1, 131, 9, 135, 1, 5, 135, 139, 2, 139, 6, 143, 2, 6, 143, 147, 1, 5, 147, 151, 1, 151, 2, 155, 1, 9, 155, 0, 99, 2, 14, 0, 0 };
    }
}
