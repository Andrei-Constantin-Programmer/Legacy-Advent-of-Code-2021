﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2021
{
    class Day9 : AoCDay
    {
        private int n, m;

        public void solve()
        {
            string[] lines = File.ReadAllLines(Utilities.getInputFileString(9));
            n = lines.Length;
            m = lines[0].Length;

            Console.WriteLine("Part 1: ");
            solvePartOne();

            Console.WriteLine();

            Console.WriteLine("INCOMPLETE Part 2: ");
            solvePartTwo();
        }


        private void solvePartOne()
        {
            int[,] heightMap = readHeightMap();
            using (TextReader read = Utilities.getInputFile(9))
            {
                for (int i = 0; i < n; i++)
                {
                    char[] heightsAsChar = read.ReadLine().ToCharArray();
                    byte[] heights = Array.ConvertAll(heightsAsChar, character => (byte)Char.GetNumericValue(character));
                    for (int j = 0; j < m; j++)
                        heightMap[i, j] = heights[j];
                }

                int sum = 0;
                var lowPoints = getLowPoints(heightMap);

                foreach (var point in lowPoints)
                {
                    sum += heightMap[point.Key, point.Value] + 1;
                }

                Console.WriteLine(sum);
            }
        }

        private void solvePartTwo()
        {
            int[,] heightMap = readHeightMap();

            var lowPoints = getLowPoints(heightMap);
            var basinSizes = new List<int>();

            int[,] basinMap = (int[,])heightMap.Clone();

            foreach (var point in lowPoints)
            {
                int size = 1;
                basinMap[point.Key, point.Value] = 9;
                var neighbours = getNeighbourLowPoints(basinMap, point);
                
                while(neighbours.Count>0)
                {
                    var newNeighbours = new List<KeyValuePair<int, int>>();

                    size += neighbours.Count;
                    foreach(var neighbour in neighbours)
                    {
                        basinMap[neighbour.Key, neighbour.Value] = 9;
                        newNeighbours.AddRange(getNeighbourLowPoints(basinMap, neighbour));
                    }

                    neighbours = newNeighbours;
                }

                basinSizes.Add(size);
            }

            basinSizes = basinSizes.OrderByDescending(x => x).ToList();

            foreach(var x in basinSizes)
                Console.WriteLine(x);

            Console.WriteLine(basinSizes[0]*basinSizes[1]*basinSizes[2]);
        }

        private int[,] readHeightMap()
        {
            var heightMap = new int[n, m];
            using (TextReader read = Utilities.getInputFile(9))
            {
                for (int i = 0; i < n; i++)
                {
                    char[] heightsAsChar = read.ReadLine().ToCharArray();
                    byte[] heights = Array.ConvertAll(heightsAsChar, character => (byte)Char.GetNumericValue(character));
                    for (int j = 0; j < m; j++)
                        heightMap[i, j] = heights[j];
                }

                return heightMap;
            }
        }

        private List<KeyValuePair<int, int>> getLowPoints(int[,] heightMap)
        {
            var lowPoints = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (isLowPoint(heightMap, new KeyValuePair<int, int>(i, j)))
                    {
                        lowPoints.Add(new KeyValuePair<int, int>(i, j));
                    }
                }

            return lowPoints;
        }

        private List<KeyValuePair<int, int>> getNeighbourLowPoints(int[,] heightMap, KeyValuePair<int, int> coord)
        {
            var neighbours = new List<KeyValuePair<int, int>>();
            var coords = getNeighbourCoords(coord);
            foreach (var x in coords)
            {
                if (inBounds(x) && isLowPoint(heightMap, x))
                    neighbours.Add(x);
            }

            return neighbours;
        }

        private bool isLowPoint(int[,] heightMap, KeyValuePair<int, int> coord)
        {
            int i = coord.Key, j = coord.Value;
            int x = heightMap[i, j];
            bool isLowPoint = true;
            if (i - 1 >= 0 && heightMap[i - 1, j] <= x)
                isLowPoint = false;
            if (j - 1 >= 0 && heightMap[i, j - 1] <= x)
                isLowPoint = false;
            if (i + 1 < n && heightMap[i + 1, j] <= x)
                isLowPoint = false;
            if (j + 1 < m && heightMap[i, j + 1] <= x)
                isLowPoint = false;

            return isLowPoint;
        }

        private List<KeyValuePair<int, int>> getNeighbourCoords(KeyValuePair<int, int> coord)
        {
            int i = coord.Key, j = coord.Value;
            var coords = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(i - 1, j), new KeyValuePair<int, int>(i, j - 1), new KeyValuePair<int, int>(i + 1, j), new KeyValuePair<int, int>(i, j + 1) };

            for (int k = 0; k < coords.Count; k++)
                if (!inBounds(coords[k]))
                {
                    coords.RemoveAt(k);
                    k--;
                }

            return coords;
        }

        private bool inBounds(KeyValuePair<int, int> coord)
        {
            return coord.Key >= 0 && coord.Key < n && coord.Value >= 0 && coord.Value < m;
        }
    }
}
