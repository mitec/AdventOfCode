using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day5
    {
        internal static void Solve1Half(string filePath)
        {
            int stepCount = 0;

            var offsets = ParseInput(filePath);

            int p = 0;
            int o = 0;

            do
            {
                p += o;
                o = offsets[p];
                offsets[p]++;

                stepCount++;
            } while (p + o < offsets.Length);

            PrintResult(stepCount);
        }

        internal static void Solve2Half(string filePath)
        {
            int stepCount = 0;

            var offsets = ParseInput(filePath);

            int p = 0;
            int o = 0;

            do
            {
                p += o;
                o = offsets[p];
                offsets[p] += (o >= 3 ? -1 : 1);

                stepCount++;
            } while (p + o < offsets.Length);

            PrintResult(stepCount);
        }

        private static int[] ParseInput(string filePath)
        {
            var lines = File.ReadLines(filePath);

            var data = new int[lines.Count()];

            int i = 0;
            foreach (string line in lines)
            {
                if (line == "") continue;

                data[i] = int.Parse(line);
                i++;
            }

            return data;
        }

        private static void PrintResult(int result)
        {
            Console.WriteLine($"steps to exit '{result}'");
        }
    }
}