﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day4 : AoCDay
    {
        private const int n = 5;

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
            using (TextReader read = Utilities.getInputFile(4))
            {
                var drawn = Array.ConvertAll(read.ReadLine().Split(","), int.Parse);
                var boards = getBingoBoards(read);

                bool found = false;
                int winNumber = 0;
                var winBoard = new BingoBoard(null);

                for(int i=0; i<drawn.Length && !found; i++)
                {
                    int x = drawn[i];

                    for(int j=0; j<boards.Count && !found; j++)
                    {
                        boards[j].markChecked(x);
                        if(boards[j].checkWin())
                        {
                            winNumber = x;
                            winBoard = boards[j];
                            found = true;
                        }
                    }
                }

                Console.WriteLine(winNumber * winBoard.sumUnchecked());
            }
        }

        private void solvePartTwo()
        {
            using (TextReader read = Utilities.getInputFile(4))
            {
                var drawn = Array.ConvertAll(read.ReadLine().Split(","), int.Parse);
                var boards = getBingoBoards(read);

                bool foundLast = false;

                for (int i = 0; i < drawn.Length && !foundLast; i++)
                {
                    int x = drawn[i];

                    for (int j = 0; j < boards.Count && !foundLast; j++)
                    {
                        boards[j].markChecked(x);
                        if (boards[j].checkWin())
                        {
                            if (boards.Count > 1)
                            {
                                boards.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                foundLast = true;
                                Console.WriteLine(x * boards[0].sumUnchecked());
                            }
                        }
                    }
                }
            }
        }

        private List<BingoBoard> getBingoBoards(TextReader read)
        {
            var boards = new List<BingoBoard>();

            while (read.ReadLine() != null)
            {
                BingoItem[,] board = new BingoItem[n, n];
                for (int i = 0; i < n; i++)
                {
                    int j = 0;
                    foreach (var x in read.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    {
                        board[i, j] = new BingoItem(Convert.ToInt32(x));
                        j++;
                    }
                }

                boards.Add(new BingoBoard(board));
            }

            return boards;
        }
    }

    class BingoItem
    {
        public int value { get; }

        public bool isChecked { get; set; }

        public BingoItem(int value)
        {
            this.value = value;
            isChecked = false;
        }
    }

    class BingoBoard
    {
        public BingoItem[,] board;

        public BingoBoard(BingoItem[,] board)
        {
            this.board = board;
        }

        public void markChecked(int value)
        {
            for (int i = 0; i < Math.Sqrt(board.Length); i++)
                for (int j = 0; j < Math.Sqrt(board.Length); j++)
                {
                    if (board[i, j].value == value)
                        board[i, j].isChecked = true;
                }
        }

        public bool checkWin()
        {
            bool won = false;
            for(int i=0; i<Math.Sqrt(board.Length) && won==false; i++)
            { 
                bool victory = true;
                for (int j = 0; j < Math.Sqrt(board.Length) && victory == true; j++)
                    if (!board[i, j].isChecked)
                        victory = false;
                won = victory;
            }

            if (won) return won;

            for(int j=0; j<Math.Sqrt(board.Length) && won==false; j++)
            {
                bool victory = true;
                for (int i = 0; i < Math.Sqrt(board.Length) && victory == true; i++)
                    if (!board[i, j].isChecked)
                        victory = false;
                won = victory;
            }

            return won;
        }

        public int sumUnchecked()
        {
            int sum = 0;

            for (int i = 0; i < Math.Sqrt(board.Length); i++)
                for (int j = 0; j < Math.Sqrt(board.Length); j++)
                    if(!board[i,j].isChecked)
                        sum += board[i, j].value;

            return sum;
        }

        public override string ToString()
        {
            string matrix = "";
            for(int i=0; i<Math.Sqrt(board.Length); i++)
            {
                for (int j = 0; j < Math.Sqrt(board.Length); j++)
                    matrix += board[i, j].value + " ";
                matrix += "\n";
            }

            return matrix;
        }
    }
}
