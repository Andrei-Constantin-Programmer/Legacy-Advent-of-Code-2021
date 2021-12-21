using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_Code_2021
{
    class Utilities
    {
        public static TextReader getInputFile(int day)
        {
            return File.OpenText(getInputFileString(day));
        }

        public static string getInputFileString(int day)
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\inputFiles\day" + day + ".txt";
        }

        public static StreamWriter getOutputFile(int day)
        {
            string directory = Environment.CurrentDirectory+@"\..\..\..";
            return new StreamWriter(Path.Combine(directory, @"outputFiles\", "day"+day+"out.txt"));
        }
    }
}
