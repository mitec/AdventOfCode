using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    internal class Day3
    {
        internal static void Solve1Half(int input)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            int[,] grid = GenerateGrid2(input);

            //PrintGrid(grid);
            
            int x1, y1;
            GetPos(grid, 1, out x1, out y1);
            
            int xInput, yInput;
            GetPos(grid, input, out xInput, out yInput);

            int dist = ComputeDist(x1, y1, xInput, yInput);
            
            Console.WriteLine($"Distance from 1 to {input} is {dist}");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }
        
        internal static void Solve2Half(int input)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            int[,] grid = GenerateGrid2(input);

            //PrintGrid(grid);

            int largerNum = GetMaxValue(grid);
            
            Console.WriteLine($"1. larger num then {input} is {largerNum}");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        private static int[,] GenerateGrid(int input)
        {
            var s = (int) Math.Ceiling(Math.Sqrt(input));
            if (s % 2 == 0) s += 1;

            var grid = new int[s, s];

            int x = (s - 1) / 2;
            int y = x;

            var dir = new int[][]
            {
                new int[] {0,1},
                new int[] {-1,0},
                new int[] {0,-1},
                new int[] {1,0}
            };

            int v = 1;
            int o = 0;
            
            grid[x, y] = v;

            while (v < input)
            {
                for (int i = 0; i < dir.Length; i++)
                {
                    if (i % 2 == 0) o++;

                    int dx = dir[i][0];
                    int dy = dir[i][1];
                    
                    int j = 0;
                    while (++j <= o)
                    {
                        x += dx;
                        y += dy;
                        
                        grid[x, y] = ++v;

                        if (v == input) goto Exit;
                    }
                }
            }
            
            Exit:
            return grid;
        }
        
        private static void GetPos(int[,] grid, int input, out int outX, out int outY)
        {
            outX = 0;
            outY = 0;
            
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == input)
                    {
                        outX = x;
                        outY = y;

                        goto Exit;
                    }
                }
            }
            
            Exit:
                return;
            
            throw new Exception($"Failed to find position for input {input}");
        }

        private static int ComputeDist(int x0, int y0, int x1, int y1)
        {
            return Math.Abs(x1 - x0) + Math.Abs(y1 - y0);
        }
 
        private static int[,] GenerateGrid2(int input)
        {
            var s = (int) Math.Ceiling(Math.Sqrt(input));
            if (s % 2 == 0) s += 1;

            var grid = new int[s, s];

            int x = (s - 1) / 2;
            int y = x;

            var dir = new int[][]
            {
                new int[] {0,1},
                new int[] {-1,0},
                new int[] {0,-1},
                new int[] {1,0}
            };

            int v = 1;
            int o = 0;
            
            grid[x, y] = v;

            while (v < input)
            {
                for (int i = 0; i < dir.Length; i++)
                {
                    if (i % 2 == 0) o++;

                    int dx = dir[i][0];
                    int dy = dir[i][1];
                    
                    int j = 0;
                    while (++j <= o)
                    {
                        x += dx;
                        y += dy;

                        v = 0;
                        if (y + 1 < s) v += grid[x, y + 1];
                        if (y - 1 >= 0) v += grid[x, y - 1];
                        if (x + 1 < s) v += grid[x + 1, y];
                        if (x + 1 < s && y + 1 < s) v += grid[x + 1, y + 1];
                        if (x + 1 < s && y - 1 >= 0) v += grid[x + 1, y - 1];
                        if (x - 1 >= 0) v += grid[x - 1, y];
                        if (x - 1 >= 0 && y + 1 < s) v += grid[x - 1, y + 1];
                        if (x - 1 >= 0 && y - 1 >= 0) v += grid[x - 1, y - 1];
                        
                        grid[x, y] = v;

                        if (v > input) goto Exit;
                    }
                }
            }
            
            Exit:
            return grid;
        }

        private static int GetMaxValue(int[,] grid)
        {
            int maxV = 0;
            
            for (int x = 0; x < grid.GetLength(0); x++)
            {                
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] > maxV) maxV = grid[x, y];
                }
            }

            return maxV;
        }
        
        private static void PrintGrid(int[,] grid)
        {
            string rows = "";
            
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                string row = "";
                
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    row += $"{grid[x, y]}\t";
                }

                rows += $"{row}\n";
            }
            
            Console.WriteLine(rows);
        }
    }
}