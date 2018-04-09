using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    internal class Day15
    {
        internal static void Solve1Half(int seedA, int seedB)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            int count = 0;

            int valueA = seedA;
            int valueB = seedB;
            
            for (int i = 0; i < 40000000; i++)
            {
                valueA = GenerateValue(valueA, 16807);
                valueB = GenerateValue(valueB, 48271);

                if ((ushort) valueA == (ushort) valueB) count++;
            }
            
            sw.Stop();

            Console.WriteLine($"Matching pairs count '{count}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(int seedA, int seedB)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            int count = 0;

            int valueA = seedA;
            int valueB = seedB;
            
            for (int i = 0; i < 5000000; i++)
            {
                valueA = GenerateValueOfMultiple(valueA, 16807, 4);
                valueB = GenerateValueOfMultiple(valueB, 48271, 8);

                if ((ushort) valueA == (ushort) valueB) count++;
            }
            
            sw.Stop();

            Console.WriteLine($"Matching pairs count '{count}' in {sw.ElapsedMilliseconds}");
        }

        private static int GenerateValue(int seed, long factor)
        {
//            long a = (seed * factor);
//            long b = a & 2147483647L;
//
//            return (int)b;
            return (int)(seed * factor % 2147483647);
        }

        private static int GenerateValueOfMultiple(int seed, long factor, int multiple)
        {
            do
            {
                seed = GenerateValue(seed, factor);
            }
            while (seed % multiple != 0);

            return seed;
        }
    }
}