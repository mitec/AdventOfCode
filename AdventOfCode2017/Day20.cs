using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Day20
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            string[] ls = File.ReadAllLines(filePath);
            
            var ps = new long[ls.Length, 9];
            
            var rgx = new Regex(@"(-?\d+),(-?\d+),(-?\d+)");
            
            for (int i = 0; i < ls.Length; i++)
            {
                int j = -1;
                foreach (Match m in rgx.Matches(ls[i]))
                {
                    var g = m.Groups;
                    for (int k = 1; k < g.Count; k++) ps[i, ++j] = int.Parse(g[k].Value);
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                for (int k = 0; k < ls.Length; k++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        ps[k, j + 3] += ps[k, j + 6];
                        ps[k, j] += ps[k, j + 3];
                    }
                }
            }

            long mv = int.MaxValue;
            int mk = -1;
            for (int k = 0; k < ls.Length; k++)
            {
                long m = 0;
                for (int j = 0; j < 3; j++) m += Math.Abs(ps[k, j]);

                if (m < mv)
                {
                    mv = m;
                    mk = k;
                }
            }
            
            sw.Stop();

            Console.WriteLine($"Nearest point is {mk} in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int leftParticles = 0;
            
            string[] ls = File.ReadAllLines(filePath);
            
            var ps = new long[ls.Length, 10];
            
            var rgx = new Regex(@"(-?\d+),(-?\d+),(-?\d+)");
            
            for (int i = 0; i < ls.Length; i++)
            {
                ps[i, 0] = i;
                
                int j = 0;
                foreach (Match m in rgx.Matches(ls[i]))
                {
                    var g = m.Groups;
                    for (int k = 1; k < g.Count; k++) ps[i, ++j] = int.Parse(g[k].Value);
                }
            }

            for (int i = 0; i < 500; i++)
            {
                for (int k = 0; k < ls.Length; k++)
                {
                    if (ps[k, 0] == 0) continue;
                    
                    for (int j = 1; j <= 3; j++)
                    {
                        ps[k, j + 3] += ps[k, j + 6];
                        ps[k, j] += ps[k, j + 3];
                    }
                }
                
                for (int k = 0; k < ls.Length - 1; k++)
                {
                    if (ps[k, 0] == 0) continue;

                    for (int l = k + 1; l < ls.Length - 1; l++)
                    {
                        if (ps[k, 1] == ps[l, 1] && ps[k, 2] == ps[l, 2] && ps[k, 3] == ps[l, 3])
                        {
                            ps[k, 0] = 0;
                            ps[l, 0] = 0;
                        }
                    }
                }
            }

            for (int k = 0; k < ls.Length; k++)
            {
                if (ps[k, 0] > 0) leftParticles++;
            }
            
            sw.Stop();

            Console.WriteLine($"Particles left '{leftParticles}' in {sw.ElapsedMilliseconds}");
        }
    }
}