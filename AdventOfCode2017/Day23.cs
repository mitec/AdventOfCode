using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day23
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            long c = 0;

            string[] cmds = File.ReadAllLines(filePath);
            var regs = new Dictionary<char, long>();

            long i = 0;
            while (i >= 0 && i < cmds.Length)
            {
                string cmd = cmds[i];

                string ins = cmd.Substring(0, 3);

                long rv;
                char r = cmd[4];

                if (char.IsLetter(r))
                {
                    regs.TryGetValue(r, out rv);
                }
                else
                {
                    rv = int.Parse(r.ToString());
                    r = char.MinValue;
                }

                if (ins == "set")
                {
                    regs[r] = GetValue(cmd, regs);
                }
                else if (ins == "mul")
                {
                    regs[r] = rv * GetValue(cmd, regs);
                    c++;
                }
                else if (ins == "sub")
                {
                    regs[r] = rv - GetValue(cmd, regs);
                }
                else if (ins == "jnz")
                {
                    if (rv != 0)
                    {
                        i += GetValue(cmd, regs);
                        continue;
                    }
                }

                i++;
            }

            sw.Stop();

            Console.WriteLine($"Mul called {c} in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

//            string[] cmds = File.ReadAllLines(filePath);
//            var regs = new Dictionary<char, long>{{'a', 1}};
//
//            long i = 0;
//            while (i >= 0 && i < cmds.Length)
//            {
//                string cmd = cmds[i];
//
//                string ins = cmd.Substring(0, 3);
//
//                long rv;
//                char r = cmd[4];
//
//                if (char.IsLetter(r))
//                {
//                    regs.TryGetValue(r, out rv);
//                }
//                else
//                {
//                    rv = int.Parse(r.ToString());
//                    r = char.MinValue;
//                }
//
//                if (ins == "set")
//                {
//                    regs[r] = GetValue(cmd, regs);
//                }
//                else if (ins == "mul")
//                {
//                    regs[r] = rv * GetValue(cmd, regs);
//                }
//                else if (ins == "sub")
//                {
//                    regs[r] = rv - GetValue(cmd, regs);
//                }
//                else if (ins == "jnz")
//                {
//                    if (rv != 0)
//                    {
//                        i += GetValue(cmd, regs);
//                        continue;
//                    }
//                }
//
//                i++;
//            }

            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long f = 0;
            long g = 0;
            long h = 0;

            b = 84;
            b *= 100;
            b -= -100000;
            c = b;
            c -= -17000;
            
            do
            {
                f = 1;
                d = 2;

                do
                {
                    e = 2;

                    if (b % d == 0)
                    {
                        f = 0;
                        goto breakLoops;
                        
                        do
                        {
                            g = d;
                            g *= e;
                            g -= b;

                            if (g == 0)
                            {
                                f = 0;
                                goto breakLoops;
                            }

                            e++;
                            g = e;
                            g -= b;
                        }
                        while (g != 0);
                    }

                    d++;
                    g = d;
                    g -= b;
                }
                while (g != 0);

                breakLoops:
                if (f == 0) h++;

                g = b;
                g -= c;

                if (g == 0) break;

                b += 17;
            }
            while (true);

            sw.Stop();

//            Console.WriteLine($"Register 'h' contains '{regs['h']}' in {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Register 'h' contains '{h}' in {sw.ElapsedMilliseconds}");
        }

        private static long GetValue(string cmd, Dictionary<char, long> regs)
        {
            long v;
            if (char.IsLetter(cmd[6]))
                regs.TryGetValue(cmd[6], out v);
            else
                v = int.Parse(cmd.Substring(6));

            return v;
        }
    }
}