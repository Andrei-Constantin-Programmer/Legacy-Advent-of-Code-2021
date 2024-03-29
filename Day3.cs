﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day3 : AoCDay
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
            var bits = Enumerable.Range(0, 12).Select(x => new BitAmmount { }).ToList();

            using (TextReader read = Utilities.getInputFile(3))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                        if (line[i] == '0')
                            bits[i].zeroes++;
                        else
                            bits[i].ones++;
                }
            }

            string gamma="", epsilon="";
            foreach(var bit in bits)
            {
                gamma += bit.getHighestChar();
                epsilon += bit.getLowestChar();
            }

            Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
        }

        private void solvePartTwo()
        {
            string[] lines = File.ReadAllLines(Utilities.getInputFileString(3));
            var O2 = new List<string>(lines);
            var CO2 = new List<string>(lines);

            var bits = Enumerable.Range(0, 12).Select(x => new BitAmmount { }).ToList();
            int index = 0;
            while(O2.Count > 1)
            {
                foreach(var o2 in O2)
                {
                    if (o2[index] == '1')
                        bits[index].ones++;
                    else
                        bits[index].zeroes++;
                }

                for (int i=0; i<O2.Count; i++)
                {
                    if (O2[i][index] != bits[index].getHighestChar())
                    {
                        O2.RemoveAt(i);
                        i--;
                    }
                }

                index++;
            }

            bits = Enumerable.Range(0, 12).Select(x => new BitAmmount { }).ToList();
            index = 0;
            while (CO2.Count > 1)
            {
                foreach (var co2 in CO2)
                {
                    if (co2[index] == '0')
                        bits[index].zeroes++;
                    else
                        bits[index].ones++;
                }

                for (int i = 0; i < CO2.Count; i++)
                {
                    if (CO2[i][index] != bits[index].getLowestChar())
                    {
                        CO2.RemoveAt(i);
                        i--;
                    }
                }

                index++;
            }

            Console.WriteLine(Convert.ToInt32(O2[0], 2) * Convert.ToInt32(CO2[0], 2));
        }
    }

    class BitAmmount
    {
        public int zeroes { get; set; }
        public int ones { get; set; }

        public char getHighestChar()
        {
            return Math.Max(ones, zeroes) == ones ? '1' : '0';
        }

        public char getLowestChar()
        {
            return Math.Min(ones, zeroes) == zeroes ? '0' : '1';
        }
    }
}
