using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day10 : AoCDay
    {
        private static Dictionary<char, int> pointValues = new Dictionary<char, int>()
            {
                { ')', 3},
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137}
            };
        private static Dictionary<char, int> scoreValues = new Dictionary<char, int>()
        {
            {')', 1 },
            {']', 2 },
            {'}', 3 },
            {'>', 4 },
        };
        private static Dictionary<char, char> openCloseValues = new Dictionary<char, char>()
        {
            {'(', ')' },
            {'[', ']' },
            {'{', '}' },
            {'<', '>' },
        };


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
            using (TextReader read = Utilities.getInputFile(10))
            {
                int points = 0;
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    char? c = corruptedChar(line);
                    if (c != null)
                    {
                        points += pointValues[(char)c];
                    }
                }

                Console.WriteLine(points);
            }
        }

        private char? corruptedChar(string line)
        {
            var stack = new Stack<char>();
            for(int i=0; i<line.Length; i++)
            {
                if (openCloseValues.ContainsKey(line[i]))
                    stack.Push(line[i]);
                else if (openCloseValues[stack.Pop()] != line[i])
                {
                    //Console.WriteLine(line);
                    return line[i];
                }
            }

            return null;
        }

        private void solvePartTwo()
        {
            using (TextReader read = Utilities.getInputFile(10))
            {
                var scores = new List<long>();
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    if(corruptedChar(line)==null)
                    {
                        var stack = new Stack<char>();
                        for (int i = 0; i < line.Length; i++)
                            if (openCloseValues.ContainsKey(line[i]))
                                stack.Push(line[i]);
                            else
                                stack.Pop();

                        if(stack.Count>0)
                        {
                            long score = 0;
                            while(stack.Count>0)
                                score = score * 5 + scoreValues[openCloseValues[stack.Pop()]];
                            scores.Add(score);
                        }
                    }
                }

                scores.Sort();
                Console.WriteLine(scores[(scores.Count-1)/2]);
            }
        }
    }
}
