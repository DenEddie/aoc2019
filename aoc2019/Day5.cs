﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public static class Day5
    {
        public static void Execute()
        {
            Console.WriteLine($"Running with 1 returns: {IntCodeComputer(IntCode, 1)}");
            Console.WriteLine($"Running test with 7 returns: {IntCodeComputer(TestIntCode, 7)}");
            Console.WriteLine($"Running test with 8 returns: {IntCodeComputer(TestIntCode, 8)}");
            Console.WriteLine($"Running test with 9 returns: {IntCodeComputer(TestIntCode, 9)}");
            Console.WriteLine($"Running with 5 returns: {IntCodeComputer(IntCode, 5)}");
        }

        private static List<int> SetInput(List<int> intCode, int input)
        {
            var beforeFault = new List<int>(intCode);
            beforeFault.Add(input);
            return beforeFault;
        }

        private static int IntCodeComputer(List<int> intCode, int input)
        {
            var program = SetInput(intCode, input);
            var instructionPointer = 0;
            var instruction = GetInstruction(program[instructionPointer]);
            while (instructionPointer < program.Count - 1 && instruction != 99)
            {
                instructionPointer = ExecuteInstruction(program, instructionPointer, instruction);
                instruction = GetInstruction(program[instructionPointer]);
            }
            return program.Last();
        }

        private static int ExecuteInstruction(List<int> program, int instructionPointer, int instruction)
        {
            return instruction switch
            {
                1 => Add(program, instructionPointer),
                2 => Multiply(program, instructionPointer),
                3 => Set(program, instructionPointer),
                4 => Get(program, instructionPointer),
                5 => JumpIfTrue(program, instructionPointer),
                6 => JumpIfFalse(program, instructionPointer),
                7 => LessThanSet(program, instructionPointer),
                8 => EqualSet(program, instructionPointer),
                _ => throw new Exception("Something went wrong!!")
            };
        }

        private static int EqualSet(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            program[program[instructionPointer + 3]] = (parameters[0] == parameters[1]) ? 1 : 0;

            return instructionPointer + 4;
        }

        private static int LessThanSet(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            program[program[instructionPointer + 3]] = (parameters[0] < parameters[1]) ? 1 : 0;

            return instructionPointer + 4;
        }

        private static int JumpIfFalse(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            return parameters[0] == 0 ? parameters[1] : instructionPointer + 3;
        }

        private static int JumpIfTrue(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            return parameters[0] != 0 ? parameters[1] : instructionPointer + 3;
        }

        private static int Set(List<int> program, int instructionPointer)
        {
            program[program[instructionPointer + 1]] = program[program.Count - 1];
            return instructionPointer + 2;
        }

        private static int Get(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 1).ToList();
            program[program.Count - 1] = parameters[0];
            return instructionPointer + 2;
        }

        private static int Multiply(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            program[program[instructionPointer + 3]] = parameters[0] * parameters[1];
            return instructionPointer + 4;
        }


        private static int Add(List<int> program, int instructionPointer)
        {
            var parameters = GetParameters(program, instructionPointer, 2).ToList();
            program[program[instructionPointer + 3]] = parameters[0] + parameters[1];
            return instructionPointer + 4;
        }

        private static IEnumerable<int> GetParameters(List<int> program, int pointer, int count)
        {
            var source = program[pointer] / 100;

            while (count > 0)
            {
                pointer++;
                var digit = source % 10 == 0 ? program[program[pointer]] : program[pointer];
                source /= 10;
                count--;
                yield return digit;
            }
        }
        private static int GetInstruction(int value)
        {
            return value % 100;
        }

        private static List<int> TestIntCode = new List<int> { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 };

        private static List<int> IntCode => new List<int> { 3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1101, 81, 30, 225, 1102, 9, 63, 225, 1001, 92, 45, 224, 101, -83, 224, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 224, 223, 223, 1102, 41, 38, 225, 1002, 165, 73, 224, 101, -2920, 224, 224, 4, 224, 102, 8, 223, 223, 101, 4, 224, 224, 1, 223, 224, 223, 1101, 18, 14, 224, 1001, 224, -32, 224, 4, 224, 1002, 223, 8, 223, 101, 3, 224, 224, 1, 224, 223, 223, 1101, 67, 38, 225, 1102, 54, 62, 224, 1001, 224, -3348, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 1, 224, 1, 224, 223, 223, 1, 161, 169, 224, 101, -62, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 1, 224, 224, 1, 223, 224, 223, 2, 14, 18, 224, 1001, 224, -1890, 224, 4, 224, 1002, 223, 8, 223, 101, 3, 224, 224, 1, 223, 224, 223, 1101, 20, 25, 225, 1102, 40, 11, 225, 1102, 42, 58, 225, 101, 76, 217, 224, 101, -153, 224, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 5, 224, 1, 224, 223, 223, 102, 11, 43, 224, 1001, 224, -451, 224, 4, 224, 1002, 223, 8, 223, 101, 6, 224, 224, 1, 223, 224, 223, 1102, 77, 23, 225, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 8, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 329, 1001, 223, 1, 223, 7, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 344, 101, 1, 223, 223, 108, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 359, 101, 1, 223, 223, 1107, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 374, 101, 1, 223, 223, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 389, 101, 1, 223, 223, 1007, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 404, 1001, 223, 1, 223, 1107, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 419, 1001, 223, 1, 223, 108, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 434, 1001, 223, 1, 223, 7, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 449, 1001, 223, 1, 223, 107, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 464, 101, 1, 223, 223, 107, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 479, 101, 1, 223, 223, 1007, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 494, 1001, 223, 1, 223, 1008, 226, 226, 224, 1002, 223, 2, 223, 1006, 224, 509, 101, 1, 223, 223, 7, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 524, 1001, 223, 1, 223, 1007, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 539, 101, 1, 223, 223, 8, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 554, 101, 1, 223, 223, 1008, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 569, 101, 1, 223, 223, 1108, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 584, 101, 1, 223, 223, 107, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 599, 1001, 223, 1, 223, 1108, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 614, 1001, 223, 1, 223, 1107, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 629, 1001, 223, 1, 223, 108, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 644, 101, 1, 223, 223, 8, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 659, 101, 1, 223, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 674, 101, 1, 223, 223, 4, 223, 99, 226 };
    }
}
