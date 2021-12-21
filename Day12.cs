using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day12 : AoCDay
    {
        private Dictionary<string, List<string>> caveConnections;
        private int paths;

        public void solve()
        {
            paths = 0;
            Console.WriteLine("Part 1: ");
            solvePartOne();

            Console.WriteLine();

            paths = 0;
            Console.WriteLine("Part 2: ");
            solvePartTwo();
        }

        private void solvePartOne()
        {
            getCaves();
            findCaves("start", new List<string>());

            Console.WriteLine(paths);
        }

        private void findCaves(string cave, List<string> prevCaves)
        {
            if (cave.Equals("end"))
            {
                paths++;
                return;
            }

            List<string> connections = caveConnections[cave];
            for (int i = 0; i < connections.Count; i++)
                if (connections[i] != "start" && ((isSmall(connections[i]) && !prevCaves.Contains(connections[i])) || isBig(connections[i])))
                {
                    var newPrevCaves = new List<string>(prevCaves);
                    newPrevCaves.Add(cave);
                    findCaves(connections[i], newPrevCaves);          
                }
        }


        private List<string> smallCaves;
        private void solvePartTwo()
        {
            getCaves();
            foreach(var cave in smallCaves)
                findCavesSpecialRule("start", new List<string>(), cave);

            fullPaths.Sort();
            for (int i = 1; i < fullPaths.Count; i++)
                if (fullPaths[i].Equals(fullPaths[i-1]))
                {
                    fullPaths.RemoveAt(i);
                    i--;
                }


            Console.WriteLine(fullPaths.Count);
        }


        private List<Path> fullPaths = new List<Path>();
        private void findCavesSpecialRule(string cave, List<string> prevCaves, string visitedTwice)
        {
            if (cave.Equals("end"))
            {
                prevCaves.Add("end");
                fullPaths.Add(new Path(prevCaves));
                return;
            }

            List<string> connections = caveConnections[cave];
                for (int i = 0; i < connections.Count; i++)
                {
                    if (connections[i] != "start" && (isSmall(connections[i]) && (!prevCaves.Contains(connections[i]) || (prevCaves.FindAll(x => x.Equals(connections[i])).Count == 1) && visitedTwice == connections[i])) || isBig(connections[i]))
                    {
                        var newPrevCaves = new List<string>(prevCaves);
                        newPrevCaves.Add(cave);
                        findCavesSpecialRule(connections[i], newPrevCaves, visitedTwice);
                    }
                }
        }

        private void getCaves()
        {
            caveConnections = new Dictionary<string, List<string>>();
            smallCaves = new List<string>();
            using (TextReader read = Utilities.getInputFile(12))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] caves = line.Split("-", StringSplitOptions.RemoveEmptyEntries);
                    if (caveConnections.ContainsKey(caves[0]))
                        caveConnections[caves[0]].Add(caves[1]);
                    else
                        caveConnections.Add(caves[0], new List<string> { caves[1] });

                    if (caveConnections.ContainsKey(caves[1]))
                        caveConnections[caves[1]].Add(caves[0]);
                    else
                        caveConnections.Add(caves[1], new List<string> { caves[0] });

                    if (!smallCaves.Contains(caves[0]) && isSmall(caves[0]))
                        smallCaves.Add(caves[0]);
                    if (!smallCaves.Contains(caves[1]) && isSmall(caves[1]))
                        smallCaves.Add(caves[1]);
                }
            }
        }

        private bool isSmall(string caveName)
        {
            foreach (char c in caveName.ToCharArray())
                if (c < 'a' || c > 'z')
                    return false;

            return true;
        }

        private bool isBig(string caveName)
        {
            return !isSmall(caveName);
        }
    }

    class Path : IComparable, IComparable<Path>
    {
        public List<string> path { get; }

        public Path(List<string> path)
        {
            this.path = path;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Path other = obj as Path;
            if (other == null)
                throw new ArgumentException("A Path object is required for comparison.", "obj");

            return CompareTo(other);
        }

        public int CompareTo([AllowNull] Path other)
        {
            if (other == null)
                return 1;

            if (other.path.Count != path.Count)
                return path.Count.CompareTo(other.path.Count);

            for (int i = 0; i < path.Count; i++)
            {
                if (path[i].CompareTo(other.path[i]) != 0)
                {
                    return path[i].CompareTo(other.path[i]);
                }
            }

           
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as Path;
            if (other == null)
                return false;

            if (other.path.Count != path.Count)
                return false;

            for (int i = 0; i < path.Count; i++)
            {
                if (!path[i].Equals(other.path[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string toReturn = "";
            foreach (var x in path)
                toReturn += x + " ";
            return toReturn;
        }
    }
}
