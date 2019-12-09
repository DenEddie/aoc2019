using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2019
{
    public class IntCodeComputer
    {
        private List<int> _program;
        private int _phase;
        private bool _phaseSet;
        private int _instructionPointer;
        private int _output;

        public IntCodeComputer(List<int> program, int? phase)
        {
            _program = new List<int>(program);
            _phase = phase ?? 0;
            _phaseSet = phase.HasValue;
            _instructionPointer = 0;
            _output = 0;
        }

        public bool Halted => GetInstruction() == 99;

        private int GetInstruction() => _program[_instructionPointer] % 100;


        public int Execute(int input)
        {
            var instruction = GetInstruction();
            while (_instructionPointer < _program.Count - 1 && instruction != 99)
            {
                var keepExecuting = ExecuteInstruction(instruction, input);
                instruction = keepExecuting ? GetInstruction() : 99;
            }
            return _output;
        }

        private bool ExecuteInstruction(int instruction, int input)
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
                default:
                    throw new Exception("Something went wrong!!");
            }
            return instruction != 4;
        }

        private void Add()
        {
            var parameters = GetParameters(2).ToList();
            _program[_program[++_instructionPointer]] = parameters[0] + parameters[1];
            _instructionPointer++;
        }

        private void Multiply()
        {
            var parameters = GetParameters(2).ToList();
            _program[_program[++_instructionPointer]] = parameters[0] * parameters[1];
            _instructionPointer++;

        }

        private void Set(int input)
        {
            _program[_program[++_instructionPointer]] = _phaseSet ? _phase : input;
            _phaseSet = false;
            _instructionPointer++;
        }

        private void Get()
        {
            var parameters = GetParameters(1).ToList();
            _output = parameters[0];
            _instructionPointer++;
        }

        private void JumpIfTrue()
        {
            var parameters = GetParameters(2).ToList();
            _instructionPointer = parameters[0] != 0 ? parameters[1] : _instructionPointer + 1;

        }

        private void JumpIfFalse()
        {
            var parameters = GetParameters(2).ToList();
            _instructionPointer = parameters[0] == 0 ? parameters[1] : _instructionPointer + 1;
        }

        private void LessThanSet()
        {
            var parameters = GetParameters(2).ToList();
            _program[_program[++_instructionPointer]] = (parameters[0] < parameters[1]) ? 1 : 0;
            _instructionPointer++;
        }

        private void EqualSet()
        {
            var parameters = GetParameters(2).ToList();
            _program[_program[++_instructionPointer]] = (parameters[0] == parameters[1]) ? 1 : 0;
            _instructionPointer++;
        }

        private IEnumerable<int> GetParameters(int count)
        {
            var source = _program[_instructionPointer] / 100;

            while (count > 0)
            {
                var digit = source % 10 == 0 ? _program[_program[++_instructionPointer]] : _program[++_instructionPointer];
                source /= 10;
                count--;
                yield return digit;
            }
        }
    }
}
