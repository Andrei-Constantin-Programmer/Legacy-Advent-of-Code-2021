using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2021
{
    class Day8 : AoCDay
    {
        Dictionary<byte, char[]> segmentsByDigit;
        Dictionary<char, char> positionsByChar;

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
            generateInitialMapping();

            var uniqueSegmentsDigits = new List<byte>(){1, 4, 7, 8};

            using(TextReader read = Utilities.getInputFile(8))
            {
                int no = 0;
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var digit in parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    {
                        int segments = digit.Length;
                        foreach(var x in uniqueSegmentsDigits)
                            if (segmentsByDigit[x].Length == segments)
                                no++;
                    }
                }

                Console.WriteLine(no);
            }
        }


        private void solvePartTwo()
        {
            using (TextReader read = Utilities.getInputFile(8))
            {
                int sum = 0;
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                    string[] wiring = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);


                    positionsByChar = getPositionsByChar(wiring);
                    segmentsByDigit = getSegmentsByDigit(positionsByChar);

                    int number = 0;
                    foreach (var digit in parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    {
                        char[] digitArray = digit.ToCharArray();
                        Array.Sort(digitArray);
                        bool found = false;
                        for (byte i = 0; i < segmentsByDigit.Count && !found; i++)
                        {
                            if (segmentsByDigit[i].SequenceEqual(digitArray))
                            {
                                number = number * 10 + i;
                                found = true;
                            }
                        }
                    }

                    sum += number;
                }

                Console.WriteLine(sum);
            }
        }


        private Dictionary<char, char> getPositionsByChar(string[] wiring)
        {
            positionsByChar = new Dictionary<char, char>();
            var chars = "abcdefg".ToCharArray();
            var one = wiring.Where(i => i.Length == 2).First();
            var four = wiring.Where(i => i.Length == 4).First();
            var seven = wiring.Where(i => i.Length == 3).First();

            var a = seven.Except(one); //a is the top segment
            var b = chars.Where(character => wiring.Count(wire => wire.Contains(character)) == 6); //b is the top-left segment
            var c = chars.Where(character => wiring.Count(wire => wire.Contains(character)) == 8).Except(a); //c is the top-right segment
            var d = four.Except(one).Except(b); //d is the middle segment
            var e = chars.Where(character => wiring.Count(wire => wire.Contains(character)) == 4); //e is the bottom-left
            var f = chars.Where(character => wiring.Count(wire => wire.Contains(character)) == 9); //f is the bottom-right
            var g = chars.Where(character => wiring.Count(wire => wire.Contains(character)) == 7).Except(d); //g is the bottom

            positionsByChar['a'] = a.First();
            positionsByChar['b'] = b.First();
            positionsByChar['c'] = c.First();
            positionsByChar['d'] = d.First();
            positionsByChar['e'] = e.First();
            positionsByChar['f'] = f.First();
            positionsByChar['g'] = g.First();

            return positionsByChar;
        }

        private Dictionary<byte, char[]> getSegmentsByDigit(Dictionary<char, char> positionsByChar)
        {
            char[] zero = getCharsFromMapping(positionsByChar.Where(x => x.Key != 'd'));
            char[] one = getCharsFromMapping(positionsByChar.Where(x => x.Key=='c' || x.Key=='f'));
            char[] two = getCharsFromMapping(positionsByChar.Where(x => x.Key!='b' && x.Key!='f'));
            char[] three = getCharsFromMapping(positionsByChar.Where(x => x.Key != 'b' && x.Key != 'e'));
            char[] four = getCharsFromMapping(positionsByChar.Where(x => x.Key!='a' && x.Key!='e' && x.Key!='g'));
            char[] five = getCharsFromMapping(positionsByChar.Where(x => x.Key!='c' && x.Key!='e'));
            char[] six = getCharsFromMapping(positionsByChar.Where(x => x.Key!='c'));
            char[] seven = getCharsFromMapping(positionsByChar.Where(x => x.Key=='a' || x.Key=='c' || x.Key=='f'));
            char[] eight = getCharsFromMapping(positionsByChar.Where(x => true));
            char[] nine = getCharsFromMapping(positionsByChar.Where(x => x.Key!='e'));

            return new Dictionary<byte, char[]>()
            {
                { 0, zero},
                { 1, one},
                { 2, two },
                { 3, three },
                { 4, four },
                { 5, five },
                { 6, six },
                { 7, seven },
                { 8, eight },
                { 9, nine }
            };
        }

        private char[] getCharsFromMapping(IEnumerable<KeyValuePair<char, char>> mapping)
        {
            char[] map = new char[mapping.Count()];
            int i = 0;
            foreach(var x in mapping)
            {
                map[i] = x.Value;
                i++;
            }

            Array.Sort(map);

            return map;
        }

        private void generateInitialMapping()
        {
            positionsByChar = new Dictionary<char, char>(){{ 'a', 'a' },{ 'b', 'b' },{ 'c', 'c' },{ 'd', 'd' },{ 'e', 'e' },{ 'f', 'f' },{ 'g', 'g' },};

            segmentsByDigit = getSegmentsByDigit(positionsByChar);
        }

    }
}
