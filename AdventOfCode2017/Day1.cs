using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    internal class Day1
    {
        internal static void Solve1Half(string input)
        {
            var sw = new Stopwatch();

            sw.Start();

            Solve(1, input);

            sw.Stop();

            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string input)
        {
            var sw = new Stopwatch();

            sw.Start();

            if (input.Length % 2 != 0)
            {
                throw new ArgumentException("Input must have even length");
            }

            Solve(input.Length / 2, input);

            sw.Stop();

            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        private static void Solve(int offset, string input)
        {
            int i;

            int[] data = new int[input.Length];
            for (i = 0; i < input.Length; i++)
            {
                data[i] = input[i] - '0';
            }

            int sum = 0;

            i = 0;
            while (i < data.Length)
            {
                int j = (i + offset) % data.Length;
                if (data[i] == data[j]) sum += data[i];

                i++;
            }

            Console.WriteLine("sum is: " + sum + " \n for input: '" + input + "'");
        }
    }
}