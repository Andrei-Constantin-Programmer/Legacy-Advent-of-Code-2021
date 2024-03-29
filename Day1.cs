﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day1 : AoCDay
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
            int no = 0;
            using (TextReader read = Utilities.getInputFile(1))
            {
                int prev = Convert.ToInt32(read.ReadLine());
                int x = 0;
                while ((x = Convert.ToInt32(read.ReadLine())) != 0)
                {
                    if (x > prev)
                        no++;
                    prev = x;
                }
            }

            Console.WriteLine(no);
        }

        private void solvePartTwo()
        {
            List<int> depths = new List<int>(Array.ConvertAll(File.ReadAllLines(Utilities.getInputFileString(1)), int.Parse));
            List<int> sums = new List<int>(depths);
            int highestSum = 0;
            for(int i=0; i<depths.Count; i++)
            {
                if (highestSum > 1)
                    sums[highestSum - 2] += depths[i];
                if (highestSum > 0)
                    sums[highestSum - 1] += depths[i];
                sums[highestSum] += depths[i];
                highestSum++;
            }

            int no = 0;
            for (int i = 1; i < highestSum; i++)
                if (sums[i] > sums[i - 1])
                    no++;

            Console.WriteLine(no);
        }
    }
}
