using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Day13
    {
        private class Scanner
        {
            public readonly int Range;
            
            private int pos;
            private int dir;

            internal bool IsAtTop => pos == 0;

            internal Scanner(int range)
            {
                Range = range;

                Reset();
            }

            internal void Move()
            {
                if (pos == 0 || pos == Range - 1) dir *= -1;
                pos += dir;
            }
            
            internal void Reset()
            {
                pos = 0;
                dir = -1;
            }
        }

        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int severity = 0;
            int maxLayerId;
            Dictionary<int, Scanner> scanners;
            
            ParseInput(filePath, out scanners, out maxLayerId);

            for (int i = 0; i <= maxLayerId; i++)
            {
                Scanner curScanner;
                if (scanners.TryGetValue(i, out curScanner))
                {
                    if (curScanner.IsAtTop)
                    {
                        severity += curScanner.Range * i;
                    }
                }

                foreach (var scanner in scanners.Values) scanner.Move();
            }

            sw.Stop();

            Console.WriteLine($"Severity is '{severity}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            List<int> ids;
            List<int> ranges;
            
            ParseInput(filePath, out ids, out ranges);

            int delay = -2;
            long maxDelay = 1000000000;//556920;
            
            while (delay < maxDelay)
            {
                nextDelay:
                delay += 2;    // musi byt parne, lebo 2.layer nesmie platit (d + 1) % 2 == 0
                
                for (int i = 0; i < ids.Count; i++)
                {
                    if ((delay + ids[i]) % ranges[i] == 0) goto nextDelay;
                }

                break;
            }

            sw.Stop();

            Console.WriteLine($"Delay '{delay}' in {sw.ElapsedMilliseconds}");
        }

        
        internal static void Solve2HalfToSlow(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int maxLayerId;
            Dictionary<int, Scanner> scanners;
            
            ParseInput(filePath, out scanners, out maxLayerId);

            int delay = 0;
            long maxDelay = 556920;
            foreach (var scanner in scanners.Values)
            {
                if (maxDelay % scanner.Range != 0) maxDelay *= scanner.Range;
            }
            
            while (delay < maxDelay)
            {
                for (int i = 0; i < delay; i++)
                {
                    foreach (var scanner in scanners.Values) scanner.Move();
                }
                
                bool isCaught = false;
                
                for (int i = 0; i <= maxLayerId; i++)
                {
                    Scanner curScanner;
                    if (scanners.TryGetValue(i, out curScanner))
                    {
                        if (curScanner.IsAtTop)
                        {
                            isCaught = true;
                            break;
                        }
                    }

                    foreach (var scanner in scanners.Values) scanner.Move();
                }

                if (!isCaught) break;
                
                foreach (var scanner in scanners.Values) scanner.Reset();
                delay++;
            }

            sw.Stop();

            Console.WriteLine($"Delay '{delay}' in {sw.ElapsedMilliseconds}");
        }

        private static void ParseInput(string filePath, out Dictionary<int, Scanner> layers, out int maxLayerId)
        {
            maxLayerId = 0;
            layers = new Dictionary<int, Scanner>();

            foreach (var input in File.ReadLines(filePath))
            {
                if (input == "") continue;

                string[] parts = input.Split(new[] {": "}, StringSplitOptions.None);

                maxLayerId = int.Parse(parts[0]);
                layers.Add(maxLayerId, new Scanner(int.Parse(parts[1])));
            }
        }
        
        private static void ParseInput(string filePath, out List<int> ids, out List<int> ranges)
        {
            ids = new List<int>();
            ranges = new List<int>();

            foreach (var input in File.ReadLines(filePath))
            {
                if (input == "") continue;

                string[] parts = input.Split(new[] {": "}, StringSplitOptions.None);

                ids.Add(int.Parse(parts[0]));
                ranges.Add((int.Parse(parts[1]) - 1) * 2);
            }
        }
    }
}