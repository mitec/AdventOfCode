using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day11
    {
        private struct Direction
        {
            public readonly int Size;

            private readonly int x;
            private readonly int y;
            private readonly int z;

            public Direction(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;

                Size = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
            }

            public Direction(int v) : this(v, v, v)
            {
            }

            public static Direction operator +(Direction a, Direction b)
            {
                return InitDirectionFromOperation(a, b, 1);
            }

            public static Direction operator -(Direction a, Direction b)
            {
                return InitDirectionFromOperation(a, b, -1);
            }

            public Direction Normalize()
            {
                var m = new int[] {x, y, z};

                Array.Sort(m);

                return this - new Direction(m[1]);
            }

            public override string ToString()
            {
                return $"({x}, {y}, {z})";
            }

            private static Direction InitDirectionFromOperation(Direction a, Direction b, int coef)
            {
                return new Direction(
                    a.x + b.x * coef,
                    a.y + b.y * coef,
                    a.z + b.z * coef
                );
            }
        }


        private static Dictionary<String, Direction> ds = new Dictionary<String, Direction>()
        {
            {"n", new Direction(0, 1, 0)},
            {"ne", new Direction(-1, 0, 0)},
            {"nw", new Direction(0, 0, -1)},
            {"s", new Direction(0, -1, 0)},
            {"se", new Direction(0, 0, 1)},
            {"sw", new Direction(1, 0, 0)},
        };

        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            var d = new Direction();

            foreach (var input in File.ReadLines(filePath))
            {
                if (input == "") continue;

                foreach (string rd in input.Split(','))
                {
                    d += ds[rd];
                }
            }

            Console.WriteLine($"Shortest path '{d.Normalize().Size}'");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");

//            var directionCounts = new Dictionary<string, int>
//            {
//                {"n", 0},
//                {"ne", 0},
//                {"se", 0},
//                {"s", 0},
//                {"sw", 0},
//                {"nw", 0},
//            };
//
//            foreach (var input in File.ReadLines(filePath))
//            {
//                if (input == "") continue;
//
//                foreach (string direction in input.Split(','))
//                {
//                    directionCounts[direction] += 1;
//                }
//            }
//
//            var directionSums = new List<string>();
//            foreach (var kv in directionCounts)
//            {
//                directionSums.Add($"{kv.Value}{kv.Key}");
//            }
//
//            Console.WriteLine($"Multiply result is '{string.Join("+", directionSums)}'");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            var d = new Direction();
            var maxD = new Direction();

            foreach (var input in File.ReadLines(filePath))
            {
                if (input == "") continue;

                foreach (string rd in input.Split(','))
                {
                    d += ds[rd];

                    var nd = d.Normalize();
                    if (nd.Size > maxD.Size) maxD = nd;
                }
            }

            Console.WriteLine($"Max distance {maxD.Size}");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }
    }
}