using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day19
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            string text = "";

            string[] map = File.ReadAllLines(filePath);

            int dx = 0, dy = 0;
            int j = 0, i;

            for (i = 0; i < map[j].Length; i++)
            {
                char c = map[j][i];
                
                if (c != ' ') {
                    if (c == '|') dy = 1;
                    else dx = 1;
                    break;
                }
            }
            
            while (true)
            {
                char c = map[j][i];
                        
                if (c == '+')
                {
                    if (dx == 0)
                    {
                        dx = (i < map[j].Length && map[j][i + 1] != ' ' ? 1 : -1);
                        dy = 0;
                    }
                    else
                    {
                        dx = 0;
                        dy = (j < map.Length && map[j + 1][i] != ' ' ? 1 : -1);
                    }
                }
                else if (char.IsLetter(c))
                {
                    text += c;
                }
                else if (c == ' ')
                {
                    break;
                }
                
                i += dx;
                j += dy;

                if (j < 0 || j >= map.Length || i < 0 || i >= map[j].Length)
                {
                    break;
                }
            }
            
            sw.Stop();

            Console.WriteLine($"Text '{text}'  in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int count = 0;
            
            string[] map = File.ReadAllLines(filePath);

            int dx = 0, dy = 0;
            int j = 0, i;

            for (i = 0; i < map[j].Length; i++)
            {
                char c = map[j][i];
                
                if (c != ' ') {
                    if (c == '|') dy = 1;
                    else dx = 1;
                    break;
                }
            }
            
            while (true)
            {
                char c = map[j][i];
                        
                if (c == '+')
                {
                    if (dx == 0)
                    {
                        dx = (i < map[j].Length && map[j][i + 1] != ' ' ? 1 : -1);
                        dy = 0;
                    }
                    else
                    {
                        dx = 0;
                        dy = (j < map.Length && map[j + 1][i] != ' ' ? 1 : -1);
                    }
                }
                else if (c == ' ')
                {
                    break;
                }
                
                i += dx;
                j += dy;

                if (j < 0 || j >= map.Length || i < 0 || i >= map[j].Length)
                {
                    break;
                }

                count++;
            }
            
            sw.Stop();

            Console.WriteLine($"Step count {count} in {sw.ElapsedMilliseconds}");
        }
    }
}