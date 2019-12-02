﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2019
{
    public class Day1
    {
        public static void Execute()
        {
            Console.WriteLine($"Total sum of fuel: {CalculateFuel()}");
            Console.WriteLine($"Accumulated fuel: {CalculateAccumelate()}");
        }

        public static int CalculateFuel()
        {
            var result = Masses.Select(x => (x / 3) - 2).Sum();
            return result;
        }

        private static int CalculateAccumelate()
        {
            int result = 0;
            Parallel.ForEach(Masses, (mass) =>
            {
                int fuel = (mass / 3) - 2;
                int accumulate = 0;
                while (fuel > 0) 
                {
                    accumulate += fuel;
                    fuel = (fuel / 3) - 2;
                }
                Interlocked.Add(ref result, accumulate);
            });

            return result;
        }

        private static List<int> Masses => new List<int>
        {
            109024,
            137172,
            80445,
            80044,
            107913,
            108989,
            59638,
            120780,
            139262,
            139395,
            56534,
            129398,
            101732,
            101212,
            142352,
            123971,
            75207,
            121384,
            145719,
            66925,
            71782,
            102129,
            83220,
            147045,
            99092,
            132909,
            114504,
            141549,
            99552,
            128422,
            134505,
            58295,
            142325,
            107896,
            66181,
            86080,
            71393,
            58839,
            143851,
            149540,
            108206,
            68353,
            123196,
            61256,
            83896,
            122756,
            133066,
            138085,
            129872,
            63965,
            105520,
            141513,
            90305,
            92651,
            113284,
            66895,
            72068,
            144011,
            82963,
            136919,
            111222,
            54134,
            82662,
            107612,
            87366,
            131791,
            144708,
            116894,
            142784,
            52299,
            138414,
            56330,
            80146,
            73422,
            60308,
            125678,
            95910,
            116374,
            136257,
            100387,
            114960,
            62651,
            102946,
            56912,
            91399,
            146005,
            147222,
            125962,
            129805,
            101208,
            67998,
            85297,
            117332,
            101967,
            94339,
            130878,
            79396,
            140312,
            147746,
            136975
        };
    }
}
