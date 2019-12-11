using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2019
{
    public class Day10
    {
        public static void Execute()
        {
            FindBestLocation(Test1);
            FindBestLocation(Test2);
            FindBestLocation(Test3);
            FindBestLocation(Test4);
            var spaceInput = FindBestLocation(Input);

            Vaporize200th(spaceInput);
        }

        private static void Vaporize200th(Tuple<Coordinate, Dictionary<int, Dictionary<double, List<Coordinate>>>> spaceInput)
        {
            var vaporizedCount = 200;
            var quadrant = 0;
            while (vaporizedCount > 0)
            {
                var asteroidLines = spaceInput.Item2[quadrant].Count;
                if (asteroidLines > vaporizedCount)
                {
                    //get the 200th;
                    var asteroid200 = (quadrant % 2 == 1) ? 
                                      spaceInput.Item2[quadrant].OrderBy(x => x.Key).ElementAt(vaporizedCount-1).Value.Aggregate((a1, a2) => Math.Abs(a1.X) + Math.Abs(a1.Y) < Math.Abs(a2.X) + Math.Abs(a2.Y) ? a1 : a2) :
                                      spaceInput.Item2[quadrant].OrderByDescending(x => x.Key).ElementAt(vaporizedCount-1).Value.Aggregate((a1, a2) => Math.Abs(a1.X) + Math.Abs(a1.Y) < Math.Abs(a2.X) + Math.Abs(a2.Y) ? a1 : a2);
                    Console.WriteLine($"The 200th asteroid to be vaporized is: ({asteroid200.X + spaceInput.Item1.X}, {asteroid200.Y + spaceInput.Item1.Y})");
                }
                vaporizedCount -= asteroidLines;
                quadrant = quadrant == 3 ? 0 : quadrant + 1;
            }
        }

        private static Tuple<Coordinate, Dictionary<int, Dictionary<double, List<Coordinate>>>> FindBestLocation(List<string> space)
        {
            var asteroids = space.SelectMany(GetSpaceObjects).Where(GetAstroids).Select(c => c.Item2).ToList();
            var bestAsteroid = asteroids.Select(a => new Tuple<Coordinate, Dictionary<int, Dictionary<double, List<Coordinate>>>>(a, ViewLines(a, asteroids))).Aggregate((a1, a2) => a1.Item2.Sum(x => x.Value.Count) > a2.Item2.Sum(x => x.Value.Count) ? a1 : a2);
            Console.WriteLine($"the asteroid ({bestAsteroid.Item1.X},{-bestAsteroid.Item1.Y}) has the highest viewable asteroids: {bestAsteroid.Item2.Sum(x => x.Value.Count)}");

            return bestAsteroid;
        }

        private static Dictionary<int, Dictionary<double, List<Coordinate>>> ViewLines(Coordinate asteroid, List<Coordinate> asteroids)
        {
            return asteroids.Where(a => a.X != asteroid.X || a.Y != asteroid.Y)
                            .Select(a => GetSlopedCoordinate(a, asteroid))
                            .GroupBy(x => x.Item1.Item1)
                            .ToDictionary(x => x.Key, x => x.GroupBy(a => a.Item1.Item2).ToDictionary(s => s.Key, s => s.Select(v => v.Item2).ToList()));
        }

        private static Tuple<Tuple<int, double>, Coordinate> GetSlopedCoordinate(Coordinate asteroid, Coordinate origin)
        {
            var viewPoint = new Coordinate(asteroid.X - origin.X, asteroid.Y - origin.Y);

            var PositiveX = viewPoint.X >= 0;
            var PositiveY = viewPoint.Y >= 0;

            var quadrant = PositiveX ? (PositiveY ? 0 : 1) : (PositiveY ? 3 : 2);

            var slope = viewPoint.X / viewPoint.Y;
            return new Tuple<Tuple<int, double>, Coordinate>(new Tuple<int, double>(quadrant, slope), viewPoint);
        }

        private static bool GetAstroids(Tuple<char, Coordinate> spaceObject)
        {
            return spaceObject.Item1 == '#';
        }

        private static IEnumerable<Tuple<char, Coordinate>> GetSpaceObjects(string line, int y)
        {
            return line.ToCharArray().Select((c, x) => new Tuple<char, Coordinate>(c, new Coordinate(x, -y)));
        }

        private struct Coordinate
        {
            public Coordinate(double x, double y)
            {
                X = x;
                Y = y;
            }
            public double X { get; set; }
            public double Y { get; set; }
        }

        public static List<string> Test1 = new List<string>
        {
            ".#..#",
            ".....",
            "#####",
            "....#",
            "...##"
        };
        public static List<string> Test2 = new List<string>
        {
            "......#.#.",
            "#..#.#....",
            "..#######.",
            ".#.#.###..",
            ".#..#.....",
            "..#....#.#",
            "#..#....#.",
            ".##.#..###",
            "##...#..#.",
            ".#....####"
        };
        public static List<string> Test3 = new List<string>
        {
            "#.#...#.#.",
            ".###....#.",
            ".#....#...",
            "##.#.#.#.#",
            "....#.#.#.",
            ".##..###.#",
            "..#...##..",
            "..##....##",
            "......#...",
            ".####.###."
        };
        public static List<string> Test4 = new List<string>
        {
            ".#..#..###",
            "####.###.#",
            "....###.#.",
            "..###.##.#",
            "##.##.#.#.",
            "....###..#",
            "..#.#..#.#",
            "#..#.#.###",
            ".##...##.#",
            ".....#.#.."
        };
        public static List<string> Test5 = new List<string>
        {
            ".#..##.###...#######",
            "##.############..##.",
            ".#.######.########.#",
            ".###.#######.####.#.",
            "#####.##.#.##.###.##",
            "..#####..#.#########",
            "####################",
            "#.####....###.#.#.##",
            "##.#################",
            "#####.##.###..####..",
            "..######..##.#######",
            "####.##.####...##..#",
            ".#####..#.######.###",
            "##...#.##########...",
            "#.##########.#######",
            ".####.#.###.###.#.##",
            "....##.##.###..#####",
            ".#.#.###########.###",
            "#.#.#.#####.####.###",
            "###.##.####.##.#..##"
        };

        public static List<string> Input = new List<string>
        {
            "..............#.#...............#....#....",
            "#.##.......#....#.#..##........#...#......",
            "..#.....#....#..#.#....#.....#.#.##..#..#.",
            "...........##...#...##....#.#.#....#.##..#",
            "....##....#...........#..#....#......#.###",
            ".#...#......#.#.#.#...#....#.##.##......##",
            "#.##....#.....#.....#...####........###...",
            ".####....#.......#...##..#..#......#...#..",
            "...............#...........#..#.#.#.......",
            "........#.........##...#..........#..##...",
            "...#..................#....#....##..#.....",
            ".............#..#.#.........#........#.##.",
            "...#.#....................##..##..........",
            ".....#.#...##..............#...........#..",
            "......#..###.#........#.....#.##.#......#.",
            "#......#.#.....#...........##.#.....#..#.#",
            ".#.............#..#.....##.....###..#..#..",
            ".#...#.....#.....##.#......##....##....#..",
            ".........#.#..##............#..#...#......",
            "..#..##...#.#..#....#..#.#.......#.##.....",
            "#.......#.#....#.#..##.#...#.......#..###.",
            ".#..........#...##.#....#...#.#.........#.",
            "..#.#.......##..#.##..#.......#.###.......",
            "...#....###...#......#..#.....####........",
            ".............#.#..........#....#......#...",
            "#................#..................#.###.",
            "..###.........##...##..##.................",
            ".#.........#.#####..#...##....#...##......",
            "........#.#...#......#.................##.",
            ".##.....#..##.##.#....#....#......#.#....#",
            ".....#...........#.............#.....#....",
            "........#.##.#...#.###.###....#.#......#..",
            "..#...#.......###..#...#.##.....###.....#.",
            "....#.....#..#.....#...#......###...###...",
            "#..##.###...##.....#.....#....#...###..#..",
            "........######.#...............#...#.#...#",
            "...#.....####.##.....##...##..............",
            "###..#......#...............#......#...#..",
            "#..#...#.#........#.#.#...#..#....#.#.####",
            "#..#...#..........##.#.....##........#.#..",
            "........#....#..###..##....#.#.......##..#",
            ".................##............#.......#.."
        };
    }
}
