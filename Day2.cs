using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day2 : AoCDay
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
            int depth = 0, horizontal = 0;
            using (TextReader read = Utilities.getInputFile(2))
            {
                string line = "";
                while ((line = read.ReadLine()) != null)
                {
                    string[] command = line.Split(" ");
                    int x = Convert.ToInt32(command[1]);
                    if (command[0].Equals("forward"))
                        horizontal += x;
                    else if (command[0].Equals("down"))
                        depth += x;
                    else if (command[0].Equals("up"))
                        depth -= x;
                }
            }

            Console.WriteLine(depth*horizontal);
        }

        private void solvePartTwo()
        {
            int depth = 0, horizontal = 0, aim = 0;

            using (TextReader read = Utilities.getInputFile(2))
            {
                string line = "";
                while ((line = read.ReadLine()) != null)
                {
                    string[] command = line.Split(" ");
                    int x = Convert.ToInt32(command[1]);
                    if (command[0].Equals("forward"))
                    {
                        horizontal += x;
                        depth += x*aim;
                    }
                    else if (command[0].Equals("down"))
                        aim += x;
                    else if (command[0].Equals("up"))
                        aim -= x;
                }
            }

            Console.WriteLine(depth * horizontal);
        }
    }
}
