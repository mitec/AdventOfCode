using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    internal class Day25
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            var t = new byte[100001];

            int i = 0;
            int p = (t.Length - 1) / 2;
            char s = 'a';

            do
            {
                switch (s)
                {
                    case 'a':
                        if (t[p] == 0)
                        {
                            t[p] = 1;
                            p++;
                            s = 'b';
                        }
                        else
                        {
                            t[p] = 0;
                            p--;
                            s = 'c';
                        }

                        break;

                    case 'b':
                        if (t[p] == 0)
                        {
                            t[p] = 1;
                            p--;
                            s = 'a';
                        }
                        else
                        {
                            t[p] = 1;
                            p++;
                            s = 'd';
                        }

                        break;

                    case 'c':
                        if (t[p] == 0)
                        {
                            t[p] = 0;
                            p--;
                            s = 'b';
                        }
                        else
                        {
                            t[p] = 0;
                            p--;
                            s = 'e';
                        }

                        break;

                    case 'd':
                        if (t[p] == 0)
                        {
                            t[p] = 1;
                            p++;
                            s = 'a';
                        }
                        else
                        {
                            t[p] = 0;
                            p++;
                            s = 'b';
                        }

                        break;

                    case 'e':
                        if (t[p] == 0)
                        {
                            t[p] = 1;
                            p--;
                            s = 'f';
                        }
                        else
                        {
                            t[p] = 1;
                            p--;
                            s = 'c';
                        }

                        break;

                    case 'f':
                        if (t[p] == 0)
                        {
                            t[p] = 1;
                            p++;
                            s = 'd';
                        }
                        else
                        {
                            t[p] = 1;
                            p++;
                            s = 'a';
                        }

                        break;
                }
            } while (++i < 12481997);

            int checksum = 0;
            for (int j = 0; j < t.Length; j++) checksum += t[j];

            sw.Stop();

            Console.WriteLine($"Checksum is {checksum} in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            sw.Stop();

            Console.WriteLine($"in {sw.ElapsedMilliseconds}");
        }
    }
}