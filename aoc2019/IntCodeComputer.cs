using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class IntCodeComputer
    {
        private int _instructionPointer;
        private int _relativeBase;


        public IntCodeComputer(List<long> program)
        {
            _instructionPointer = 0;
            Memory = new List<long>(program);
            _relativeBase = 0;
        }

        public List<long> Memory { get; private set; }

        public bool Halted => GetInstruction() == 99;
        public bool AtEnd => _instructionPointer == Memory.Count - 1;

        private int GetInstruction() => (int)(GetDigit(_instructionPointer) % 100);

        public void Reset()
        {
            _instructionPointer = 0;
            _relativeBase = 0;
        }

        public long Execute(List<long> input)
        {
            var instruction = GetInstruction();
            while (!AtEnd && instruction != 99)
            {
                var keepExecuting = ExecuteInstruction(instruction, input);
                instruction = keepExecuting ? GetInstruction() : 99;
            }
            return Memory.LastOrDefault();
        }

        private bool ExecuteInstruction(int instruction, List<long> input)
        {
            switch (instruction)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Multiply();
                    break;
                case 3:
                    Set(input);
                    break;
                case 4:
                    Get();
                    break;
                case 5:
                    JumpIfTrue();
                    break;
                case 6:
                    JumpIfFalse();
                    break;
                case 7:
                    LessThanSet();
                    break;
                case 8:
                    EqualSet();
                    break;
                case 9:
                    SetRelativeBase();
                    break;
                default:
                    throw new Exception("Something went wrong!!");
            }
            return instruction != 4;
        }

        private void Add()
        {
            var parameters = GetParameters();
            SetDigit(parameters.Item2.Last(), parameters.Item1[0] + parameters.Item1[1]);
            _instructionPointer++;
        }

        private void Multiply()
        {
            var parameters = GetParameters();
            SetDigit(parameters.Item2.Last(), parameters.Item1[0] * parameters.Item1[1]);
            _instructionPointer++;

        }

        private void Set(List<long> input)
        {
            var parameters = GetParameters();
            SetDigit(parameters.Item2.Last(), input.First());
            input.RemoveAt(0);
            _instructionPointer++;
        }

        private void Get()
        {
            Memory.Add(GetParameters().Item1.First());
            _instructionPointer++;
        }

        private void JumpIfTrue()
        {
            var parameters = GetParameters().Item1;
            _instructionPointer = parameters[0] != 0 ? (int)parameters[1] : _instructionPointer + 1;

        }

        private void JumpIfFalse()
        {
            var parameters = GetParameters().Item1;
            _instructionPointer = parameters[0] == 0 ? (int)parameters[1] : _instructionPointer + 1;
        }

        private void LessThanSet()
        {
            var parameters = GetParameters();
            SetDigit(parameters.Item2.First(), (parameters.Item1[0] < parameters.Item1[1]) ? 1 : 0);
            _instructionPointer++;
        }

        private void EqualSet()
        {
            var parameters = GetParameters();
            SetDigit(parameters.Item2.First(), (parameters.Item1[0] == parameters.Item1[1]) ? 1 : 0);
            _instructionPointer++;
        }

        private void SetRelativeBase()
        {
            _relativeBase += (int)GetParameters().Item1.First();
            _instructionPointer++;
        }

        private Tuple<List<long>, List<int>> GetParameters()
        {
            var instruction = (int)GetDigit(_instructionPointer);

            var parameterDefinition = (instruction % 100) switch
            {
                1 => new Tuple<int, int>(2, 1),
                2 => new Tuple<int, int>(2, 1),
                3 => new Tuple<int, int>(0, 1),
                4 => new Tuple<int, int>(1, 0),
                5 => new Tuple<int, int>(2, 0),
                6 => new Tuple<int, int>(2, 0),
                7 => new Tuple<int, int>(2, 1),
                8 => new Tuple<int, int>(2, 1),
                9 => new Tuple<int, int>(1, 0),
                _ => throw new NotImplementedException()
            };

            var source = instruction / 100;

            var result = new Tuple<List<long>, List<int>>(new List<long>(), new List<int>());

            for (int count = 0; count < parameterDefinition.Item1; count++)
            {
                var mode = source % 10;
                var parameterValue = GetDigit(++_instructionPointer);
                var digit = mode switch
                {
                    0 => GetDigit((int)parameterValue),
                    1 => parameterValue,
                    2 => GetDigit(_relativeBase + (int)parameterValue),
                    _ => throw new Exception($"Unknown parameter mode {mode}")
                };
                source /= 10;
                result.Item1.Add(digit);
            }
            for (int count = 0; count < parameterDefinition.Item2; count++)
            {
                var mode = source % 10;
                var digit = mode switch
                {
                    0 => GetDigit(++_instructionPointer),
                    2 => _relativeBase + GetDigit(++_instructionPointer),
                    _ => throw new Exception($"Unknown parameter mode {mode}")
                };
                source /= 10;
                result.Item2.Add((int)digit);
            }
            return result;
        }

        private long GetDigit(int pointer)
        {
            while (Memory.Count <= pointer)
            {
                Memory.Add(0);
            }
            return Memory[pointer];
        }
        private void SetDigit(int pointer, long value)
        {
            while (Memory.Count <= pointer)
            {
                Memory.Add(0);
            }
            Memory[pointer] = value;
        }
    }
}
