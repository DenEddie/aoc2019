using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2019
{
    public class Day12
    {
        public static void Execute()
        {
            Move(CopyList(MoonsTest1), 10);
            Move(CopyList(MoonsTest2), 100);
            Move(CopyList(Moons), 1000);
            StepsBeforeMatchingState(MoonsTest1);
            StepsBeforeMatchingState(MoonsTest2);
            StepsBeforeMatchingState(Moons);
        }

        private static void Move(List<Coordinate> moons, long steps)
        {
            long step = 0;
            List<Coordinate> velocities = moons.Select(m => new Coordinate(0, 0, 0)).ToList();
            for (int i = 0; i < steps; i++)
            {
                CalculateVelocity(moons, velocities);
                CalculatePosition(moons, velocities);
            }
            var totals = moons.Select((m, i) => (Math.Abs(m.X) + Math.Abs(m.Y) + Math.Abs(m.Z)) * (Math.Abs(velocities[i].X) + Math.Abs(velocities[i].Y) + Math.Abs(velocities[i].Z))).Sum();
            Console.WriteLine($"After {steps} the sum of total energy is {totals}");
        }

        private static void StepsBeforeMatchingState(List<Coordinate> moons)
        {
           long xSteps = CalculateStepsInAxis(moons.Select(m => new Moon(m.X)).ToList());
           long ySteps = CalculateStepsInAxis(moons.Select(m => new Moon(m.Y)).ToList());
           long zSteps = CalculateStepsInAxis(moons.Select(m => new Moon(m.Z)).ToList());

            Console.WriteLine($"Steps before matching: {LeastCommonMultiple(LeastCommonMultiple(xSteps, ySteps), zSteps)}");
        }

        private static int CalculateStepsInAxis(List<Moon> axisMoons)
        {
            var hashSet = new HashSet<string>();
            var step = 0;
            do
            {
                hashSet.Add(string.Concat(axisMoons));
                step++;
                foreach (var moon in axisMoons)
                {
                    foreach (var other in axisMoons)
                    {
                        if (moon != other)
                        {
                            moon.ProcessGravity(other);
                        }
                    }
                }
                foreach (var moon in axisMoons)
                {
                    moon.Move();
                }
            } while (hashSet.Count == step);

            return hashSet.Count;
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivider(a, b);
        }

        public static long GreatestCommonDivider(long a, long b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return GreatestCommonDivider(b, a % b);
            }
        }

        private static void CalculateVelocity(List<Coordinate> moons, List<Coordinate> velocities)
        {
            Parallel.For(0, velocities.Count, i =>
             {
                 velocities[i].X += moons.Sum(m => m.X > moons[i].X ? 1 : (moons[i].X == m.X ? 0 : -1));
                 velocities[i].Y += moons.Sum(m => m.Y > moons[i].Y ? 1 : (moons[i].Y == m.Y ? 0 : -1));
                 velocities[i].Z += moons.Sum(m => m.Z > moons[i].Z ? 1 : (moons[i].Z == m.Z ? 0 : -1));
             });
        }
        private static void CalculatePosition(List<Coordinate> moons, List<Coordinate> velocities)
        {
            Parallel.For(0, velocities.Count, i =>
            {
                moons[i].X += velocities[i].X;
                moons[i].Y += velocities[i].Y;
                moons[i].Z += velocities[i].Z;
            });
        }

        private static List<Coordinate> CopyList(List<Coordinate> list) => list.Select(c => new Coordinate(c.X, c.Y, c.Z)).ToList();

        private static List<Coordinate> MoonsTest1 = new List<Coordinate>{
                                                                    new Coordinate(-1,0,2),
                                                                    new Coordinate(2,-10,-7),
                                                                    new Coordinate(4,-8,8),
                                                                    new Coordinate(3,5,-1)
        };

        private static List<Coordinate> MoonsTest2 = new List<Coordinate>{
                                                                    new Coordinate(-8,-10,0),
                                                                    new Coordinate(5,5,10),
                                                                    new Coordinate(2,-7,3),
                                                                    new Coordinate(9,-8,-3)
        };
        private static List<Coordinate> Moons = new List<Coordinate>{
                                                                    new Coordinate(5,4,4),
                                                                    new Coordinate(-11,-11,-3),
                                                                    new Coordinate(0,7,0),
                                                                    new Coordinate(-13,2,10),
        };

        private class Coordinate
        {
            public Coordinate(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public override string ToString() => $"({X},{Y},{Z})";
        }

        private class Moon
        {
            private int position;
            private int velocity;

            public Moon(int position)
            {
                this.position = position;
            }

            public void Move()
            {
                position += velocity;
            }

            public void ProcessGravity(Moon other)
            {
                if (this.position < other.position) velocity++;
                if (this.position > other.position) velocity--;
            }

            public override string ToString() => $"({position},{velocity})";
        }
    }
}
