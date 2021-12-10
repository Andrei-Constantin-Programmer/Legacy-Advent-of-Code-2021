using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day7 : AoCDay
    {
        public void solve()
        {
            Console.WriteLine("Part 1: ");
            solution(false);

            Console.WriteLine();

            Console.WriteLine("Part 2: ");
            solution(true);
        }


        private void solution(bool increasingFuel)
        {
            List<int> initialPositions = new List<int>(Array.ConvertAll(Utilities.getInputFile(7).ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries), int.Parse));
            Dictionary<int, int> fishByPosition = new Dictionary<int, int>();
            foreach (var x in initialPositions)
            {
                if (fishByPosition.ContainsKey(x))
                    fishByPosition[x]++;
                else
                    fishByPosition.Add(x, 1);
            }

            int minFuel = int.MaxValue, minPos = -1;
            for (int i = 0; i < fishByPosition.Count; i++)
            {
                int sum = 0;
                if(increasingFuel)
                    foreach (var x in fishByPosition)
                        sum += x.Value * getUsedFuel(i, x.Key);
                else
                    foreach (var x in fishByPosition)
                        sum += x.Value * Math.Abs(x.Key - i);

                if (sum < minFuel)
                {
                    minFuel = sum;
                    minPos = i;
                }
            }

            Console.WriteLine(minFuel);
        }

        private int getUsedFuel(int start, int end)
        {
            if (start > end)
            {
                int aux = start;
                start = end;
                end = aux;
            }

            int no = 1, sum = 0;
            while (start < end)
            {
                sum += no;
                no++;
                start++;
            }

            return sum;
        }
    }
}
