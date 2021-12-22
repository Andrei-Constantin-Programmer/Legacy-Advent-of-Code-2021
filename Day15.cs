using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day15 : AoCDay
    {
        private int n;
        private int[,] riskMatrix;
        private int[,] pathMatrix;
        Queue<KeyValuePair<int, int>> queue;

        public void solve()
        {
            readMatrix();
            Console.WriteLine("Part 1: ");
            findPath();

            Console.WriteLine();

            createBigMatrix();
            Console.WriteLine("Part 2: ");
            findPath();
        }

        private void findPath()
        {
            queue = new Queue<KeyValuePair<int, int>>();
            pathMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    pathMatrix[i, j] = -1; //Infinity
            pathMatrix[0, 0] = 0;
            queue.Enqueue(new KeyValuePair<int, int>(0, 0));
            while(queue.Count>0)
            {
                var elem = queue.Dequeue();
                calculatePathsFrom(elem.Key, elem.Value);
            }

            Console.WriteLine(pathMatrix[n-1, n-1]);
        }

        private void readMatrix()
        {
            string[] lines = File.ReadAllLines(Utilities.getInputFileString(15));
            n = lines.Length;
            riskMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                int[] values = Array.ConvertAll(lines[i].ToCharArray(), character => (int)Char.GetNumericValue(character));
                for (int j = 0; j < n; j++)
                    riskMatrix[i, j] = values[j];
            }
        }

        private void createBigMatrix()
        {
            readMatrix();
            var bigMatrix = new int[5 * n, 5 * n];
            var matrices = new List<int[,]>();
            matrices.Add(riskMatrix);
            for (int i = 1; i <= 8; i++)
                matrices.Add(incrementMatrix(matrices[i - 1]));

            int startIndex = 0;

            for (int iMatrix = 0; iMatrix < 5; iMatrix++)
            {
                int matrixNo = startIndex;
                for (int jMatrix = 0; jMatrix < 5; jMatrix++)
                {
                    for (int i = iMatrix * n; i < iMatrix * n + n; i++)
                        for (int j = jMatrix * n; j < jMatrix * n + n; j++)
                        {
                            bigMatrix[i, j] = matrices[matrixNo][i % n, j % n];
                        }
                    matrixNo++;
                }

                startIndex++;
            }

            n = 5 * n;
            riskMatrix = bigMatrix;
        }

        private int[,] incrementMatrix(int[,] matrix)
        {
            var newMatrix = new int[n,n];

            for(int i=0; i<n; i++)
                for(int j=0; j<n; j++)
                {
                    newMatrix[i, j] = matrix[i, j] + 1;
                    if (newMatrix[i, j] > 9)
                        newMatrix[i, j] = 1;
                }

            return newMatrix;
        }

        private void calculatePathsFrom(int i, int j)
        {
            if(i-1>=0)
                calculatePath(i, j, i - 1, j);
            if (i + 1 < n)
                calculatePath(i, j, i + 1, j);
            if (j - 1 >= 0)
                calculatePath(i, j, i, j - 1);
            if (j + 1 < n)
                calculatePath(i, j, i, j + 1);
        }

        private void calculatePath(int iSource, int jSource, int iDest, int jDest)
        {
            int sourcePath = pathMatrix[iSource, jSource];
            int destPath = pathMatrix[iDest, jDest];
            if (destPath == -1 || sourcePath + riskMatrix[iDest, jDest] < destPath)
            {
                pathMatrix[iDest, jDest] = sourcePath + riskMatrix[iDest, jDest];
                queue.Enqueue(new KeyValuePair<int, int>(iDest, jDest));
            }
        }


        private void printMatrix(int[,] matrix)
        {
            for(int i=0; i<matrix.GetLength(0); i++)
            {
                for(int j=0; j<matrix.GetLength(1); j++)
                    Console.Write(matrix[i,j]+" ");
                Console.WriteLine();
            }
        }
    }
}
