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
                
            };

            return dayMappings;
        }
    }
}
