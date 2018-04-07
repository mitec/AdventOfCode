using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day2
    {
        internal static void Solve1Half(string filePath)
        {
            int checksum = 0;
            foreach (var row in ParseInput(filePath))
            {
                checksum += row.Max() - row.Min();
            }
            
            PrintResult(checksum);
        }
        
        internal static void Solve2Half(string filePath)
        {
            int checksum = 0;
            foreach (var row in ParseInput(filePath))
            {
                for (int i = 0; i < row.Length - 1; i++)
                {
                    for (int j = i + 1; j < row.Length; j++)
                    {
                        if (row[i] % row[j] == 0)
                        {
                            checksum += row[i] / row[j];
                        }
                        
                        if (row[j] % row[i] == 0)
                        {
                            checksum += row[j] / row[i];
                        }
                    }
                }
            }
            
            PrintResult(checksum);
        }

        private static IEnumerable<int[]> ParseInput(string filePath)
        {
            var data = new List<int[]>();

            foreach (string line in File.ReadLines(filePath))
            {
                if (line == "") continue;
                
                var rawNums = line.Split('\t');
                var row = Array.ConvertAll(rawNums, int.Parse);
 
                data.Add(row);
            }

            return data;
        }

        private static void PrintResult(int result)
        {
            Console.WriteLine($"checksum is '{result}'");
        }
    }
}