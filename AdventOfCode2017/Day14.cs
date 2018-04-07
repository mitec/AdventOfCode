using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    internal class Day14
    {
        internal static void Solve1Half(string input)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            int used = 0;

            for (int j = 0; j < 128; j++)
            {
                foreach (byte rb in KnotHashToHexa(input + "-" + j))
                {
                    int b = rb;
                    while (b > 0)
                    {
                        used += b & 1;
                        b = b >> 1;
                    }
                }
            }
            
            sw.Stop();

            Console.WriteLine($"Used '{used}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string input)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            var map = new int[128, 128];

            for (int j = 0; j < 128; j++)
            {
                int k = 0;
                foreach (byte rb in KnotHashToHexa(input + "-" + j))
                {
                    k += 8;
                    
                    int b = rb;
                    for (int i = 1; i <= 8; i++)
                    {
                        map[j, k - i] = (b & 1) * -1;
                        b = b >> 1;
                    }
                }
            }
            
            int id = 1;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (Flood(map, i, j, id)) id++;
                }
            }
            
            sw.Stop();

            Console.WriteLine($"Distinct regions '{id - 1}' in {sw.ElapsedMilliseconds}");
        }

        private static bool Flood(int[,] map, int i, int j, int id)
        {
            if (map[i, j] != -1) return false;

            map[i, j] = id;
            
            if (i > 0 && map[i - 1, j] == -1) Flood(map, i - 1, j, id);
            if (i < 127 && map[i + 1, j] == -1) Flood(map, i + 1, j, id);
            if (j > 0 && map[i, j - 1] == -1) Flood(map, i, j - 1, id);
            if (j < 127 && map[i, j + 1] == -1) Flood(map, i, j + 1, id);

            return true;
        }
        
        private static byte[] KnotHashToHexa(string input)
        {
            int sequenceLength = 256;
            
            var sequence = new byte[sequenceLength];
            for (var i = 0; i < sequenceLength; i++) sequence[i] = (byte)i;

            int skipSize = 0;
            int currentPos = 0;

            var suffixes = new byte[] {17, 31, 73, 47, 23};
            byte[] lengths = Encoding.ASCII.GetBytes(input).Concat(suffixes).ToArray();

            Func<int, int> rIdx = (int idx) => (currentPos + idx) % sequenceLength;

            for (int i = 0; i < 64; i++)
            {
                foreach (int len in lengths)
                {
                    for (int k = 0; k < len / 2; k++)
                    {
                        int idxL = rIdx(k);
                        int idxH = rIdx(len - 1 - k);
                    
                        byte tmp = sequence[idxL];
                        sequence[idxL] = sequence[idxH];
                        sequence[idxH] = tmp;
                    }
                
                    currentPos += len + skipSize;
                    skipSize++;
                }
            }
            
            int m = 0, n = 0;
            byte h = 0;
           
            var sparseHash = new byte[(int)Math.Ceiling(sequence.Length / 16.0)];
            
            while (m < sequence.Length)
            {
                if (m > 0 && m % 16 == 0)
                {
                    sparseHash[n] = h;
                    h = 0;
                    n++;
                }
                
                h ^= sequence[m];
                m++;
            }

            sparseHash[n] = h;

            return sparseHash;
        }
    }
}