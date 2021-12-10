using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day6 : AoCDay
    {
        public void solve()
        {
            Console.WriteLine("Part 1: ");
            solvePartOne();

            Console.WriteLine();

            Console.WriteLine("Part 2: ");
            solvePartTwo();
        }

        private void solvePartOne()
        {
            List<int> lanternfish = new List<int>(Array.ConvertAll(Utilities.getInputFile(6).ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries), int.Parse));

            int days = 0;

            while (days < 80)
            {
                List<int> newFish = new List<int>();
                for (int i = 0; i < lanternfish.Count; i++)
                {
                    if (lanternfish[i] == 0)
                    {
                        lanternfish[i] = 6;
                        newFish.Add(8);
                    }
                    else
                        lanternfish[i]--;
                }

                foreach (var fish in newFish)
                    lanternfish.Add(fish);

                days++;
            }

            Console.WriteLine(lanternfish.Count);
        }


        private void solvePartTwo()
        {
            long[] fish = new long[9];
            List<int> initialFish = new List<int>(Array.ConvertAll(Utilities.getInputFile(6).ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries), int.Parse));
            foreach (var f in initialFish)
                fish[f]++;

            int days = 0;
            while(days<256)
            {
                long birthingFish = fish[0];
                for (int i = 0; i < 8; i++)
                    fish[i] = fish[i + 1];
                fish[8] = 0;

                fish[6] += birthingFish;
                fish[8] += birthingFish;

                days++;
            }

            long sum = 0;
            foreach(var f in fish)
                sum+=f;

            Console.WriteLine(sum);
        }

    }
}
