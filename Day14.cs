using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Advent_of_Code_2021
{
    class Day14 : AoCDay
    {
        private Dictionary<string, char> insertionRules;
        private Dictionary<char, long> appearances;
        private Dictionary<string, long> pairAppearances;
        private List<char> polymer;

        public void solve()
        {
            readInput();

            Console.WriteLine("Part 1: ");
            solvePartOne();

            Console.WriteLine();

            Console.WriteLine("Part 2: ");
            readInput();
            solvePartTwo();
        }

        private void solvePartOne()
        {
            for(int step=0; step<10; step++)
            {
                for(int i=0; i<polymer.Count-1; i++)
                {
                    string pair = polymer[i] + "" + polymer[i + 1];
                    if(insertionRules.ContainsKey(pair))
                    {
                        char c = insertionRules[pair];
                        polymer.Insert(i+1, c);
                        addAppearance(c, 1);
                        
                        i++;
                    }
                }
            }

            var sorted = (from entry in appearances orderby entry.Value ascending select entry).ToList();

            Console.WriteLine(sorted[sorted.Count-1].Value - sorted[0].Value);
        }

        private void solvePartTwo()
        {
            for(int step=0; step<40; step++)
            {
                var newPairAppearances = new Dictionary<string, long>(pairAppearances);

                foreach(var pairApp in pairAppearances)
                {
                    if (insertionRules.ContainsKey(pairApp.Key))
                    {
                        char c = insertionRules[pairApp.Key];
                        string newPair1 = pairApp.Key[0] + "" + c;
                        string newPair2 = c + "" + pairApp.Key[1];
                        
                        newPairAppearances[pairApp.Key]-=pairApp.Value;
                        addPair(newPairAppearances, newPair1, pairApp.Value);
                        addPair(newPairAppearances, newPair2, pairApp.Value);
                        addAppearance(c, pairApp.Value);
                    }
                }

                foreach (var pairApp in newPairAppearances)
                    if (pairApp.Value <= 0)
                        removePair(newPairAppearances, pairApp.Key);

                pairAppearances = newPairAppearances;
            }
            
            var sorted = (from entry in appearances orderby entry.Value ascending select entry).ToList();
            Console.WriteLine(sorted[sorted.Count - 1].Value - sorted[0].Value);
        }

        private void addAppearance(char c, long value)
        {
            if (appearances.ContainsKey(c))
                appearances[c]+=value;
            else
                appearances.Add(c, value);
        }

        private void addPair(Dictionary<string, long> appearances, string pair, long value)
        {
            if (appearances.ContainsKey(pair))
                appearances[pair]+=value;
            else
                appearances.Add(pair, value);
        }

        private void removePair(Dictionary<string, long> appearances, string pair)
        {
            if (appearances.ContainsKey(pair))
                appearances.Remove(pair);
        }

        private void readInput()
        {
            insertionRules = new Dictionary<string, char>();
            appearances = new Dictionary<char, long>();
            pairAppearances = new Dictionary<string, long>();
            using (TextReader read = Utilities.getInputFile(14))
            {
                polymer = new List<char>(read.ReadLine().ToCharArray());
                foreach(var c in polymer)
                {
                    if (appearances.ContainsKey(c))
                        appearances[c]++;
                    else
                        appearances.Add(c, 1);
                }

                for(int i=0; i<polymer.Count-1; i++)
                {
                    string pair = polymer[i] + "" + polymer[i + 1];
                    addPair(pairAppearances, pair, 1);
                }

                read.ReadLine();
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] result = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                    insertionRules.Add(result[0], result[1][0]);
                }
            }
        }
    }
}
