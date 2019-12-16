using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aoc2019
{
    public class Day16
    {
        public static void Execute()
        {
            GetFFT(Test1);
            GetFFT(Test2);
            GetFFT(Test3);
            GetFFT(Input);
            GetFFT(string.Concat(Enumerable.Repeat(Input, 10000)));
        }

        private static void GetFFT(string input)
        {
            var inputList = input.ToCharArray().Select((d, i) => new Tuple<int, int>(i + 1, (int)(d - '0'))).ToList();
            for (int i = 0; i < 100; i++)
            {
                inputList = GetNextInput(inputList);
            }

            var results = inputList.Where(x => x.Item1 < 17).OrderBy(x => x.Item1).Select(x => x.Item2).ToList();
            Console.WriteLine($"First 8 digits: {string.Concat(results.Take(8))}");
            Console.WriteLine($"Next 8 digits: {string.Concat(results.GetRange(7, 8))}");
        }

        private static List<Tuple<int, int>> GetNextInput(List<Tuple<int, int>> inputList)
        {
            var outputList = new List<Tuple<int, int>>();
            for (int i = 1; i <= inputList.Count; i++)
            {
                var groups = inputList.GroupBy(x => (x.Item1 / i) % 4);
                outputList.Add(new Tuple<int, int>(i, Math.Abs((groups.FirstOrDefault(x => x.Key == 1)?.Sum(x => x.Item2) ?? 0) - (groups.FirstOrDefault(x => x.Key == 3)?.Sum(x => x.Item2) ?? 0)) % 10));
            }
            return outputList;
        }

        //private static List<int> Pattern = new List<int> { 0, 1, 0, -1 };

        private static string Test1 = "80871224585914546619083218645595";
        private static string Test2 = "19617804207202209144916044189917";
        private static string Test3 = "69317163492948606335995924319873";
        private static string Input = "59708372326282850478374632294363143285591907230244898069506559289353324363446827480040836943068215774680673708005813752468017892971245448103168634442773462686566173338029941559688604621181240586891859988614902179556407022792661948523370366667688937217081165148397649462617248164167011250975576380324668693910824497627133242485090976104918375531998433324622853428842410855024093891994449937031688743195134239353469076295752542683739823044981442437538627404276327027998857400463920633633578266795454389967583600019852126383407785643022367809199144154166725123539386550399024919155708875622641704428963905767166129198009532884347151391845112189952083025";
    }
}
