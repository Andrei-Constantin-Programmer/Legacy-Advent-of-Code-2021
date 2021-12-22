using System;
using System.Collections.Generic;

namespace Advent_of_Code_2021
{
    interface AoCDay {
        void solve();
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, AoCDay> dayMappings = initialiseDayMappings();

            Console.Write("Select day (1-25): ");
            int day = Convert.ToInt32(Console.ReadLine());

            if (day < 1 || day > 25)
            {
                Console.WriteLine("The day must be between 1 and 25.");
                return;
            }

            if(dayMappings.ContainsKey(day))
            {
                dayMappings[day].solve();
            }
            else
                Console.WriteLine("That day has not been implemented yet.");
        }


        private static Dictionary<int, AoCDay> initialiseDayMappings()
        {
            Dictionary<int, AoCDay> dayMappings = new Dictionary<int, AoCDay>()
            {
                {1, new Day1() },
                {2, new Day2() },
                {3, new Day3() },
                {4, new Day4() },
                {5, new Day5() },
                {6, new Day6() },
                {7, new Day7() },
                {8, new Day8() },
                {9, new Day9() },
                {10, new Day10() },
                {11, new Day11() },
                {12, new Day12() },
                {13, new Day13() },
                {14, new Day14() },
                {15, new Day15() },

            };

            return dayMappings;
        }
    }
}
