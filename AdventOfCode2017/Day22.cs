using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day22
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int len = 1001;
            var grid = new char[len, len];

            string[] input = File.ReadAllLines(filePath);

            int offset = (len - input.Length) / 2;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    grid[offset + i, offset + j] = input[i][j];
                }
            }

            int pX = (len - 1) / 2;
            int pY = pX;

            int d = 'u';
            int c = 0;

            for (int i = 0; i < 10000; i++)
            {
                bool isInfected;

                if (grid[pY, pX] == '#')
                {
                    isInfected = true;
                    grid[pY, pX] = '.';
                }
                else
                {
                    isInfected = false;
                    grid[pY, pX] = '#';
                    c++;
                }

                if (d == 'u')
                {
                    d = (isInfected ? 'r' : 'l');
                    pX += (d == 'r' ? 1 : -1);
                }
                else if (d == 'r')
                {
                    d = (isInfected ? 'b' : 'u');
                    pY += (d == 'b' ? 1 : -1);
                }
                else if (d == 'b')
                {
                    d = (isInfected ? 'l' : 'r');
                    pX += (d == 'r' ? 1 : -1);
                }
                else if (d == 'l')
                {
                    d = (isInfected ? 'u' : 'b');
                    pY += (d == 'b' ? 1 : -1);
                }
            }

            sw.Stop();

            Console.WriteLine($"Infected became {c} in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int len = 1001;
            var grid = new char[len, len];

            string[] input = File.ReadAllLines(filePath);

            int offset = (len - input.Length) / 2;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    grid[offset + i, offset + j] = input[i][j];
                }
            }

            int pX = (len - 1) / 2;
            int pY = pX;

            int oX = 0;
            int oY = -1;
            int c = 0;

            for (int i = 0; i < 10000000; i++)
            {
                if (grid[pY, pX] == '#')
                {
                    grid[pY, pX] = 'f';

                    if (oX != 0)
                    {
                        oY = oX;
                        oX = 0;
                    }
                    else
                    {
                        oX = -oY;
                        oY = 0;
                    }
                }
                else if (grid[pY, pX] == 'w')
                {
                    grid[pY, pX] = '#';
                    c++;
                }
                else if (grid[pY, pX] == 'f')
                {
                    grid[pY, pX] = '.';

                    oX *= -1;
                    oY *= -1;
                }
                else
                {
                    grid[pY, pX] = 'w';

                    if (oX != 0)
                    {
                        oY = -oX;
                        oX = 0;
                    }
                    else
                    {
                        oX = oY;
                        oY = 0;
                    }
                }

                pX += oX;
                pY += oY;
            }

            sw.Stop();

            Console.WriteLine($"Infected became {c} in {sw.ElapsedMilliseconds}");
        }
    }
}