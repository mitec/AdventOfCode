using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    internal class Day10
    {
        internal static void Solve1Half(int sequenceLength, string input)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            int result = 0;

            var sequence = new int[sequenceLength];
            for (var i = 0; i < sequenceLength; i++) sequence[i] = i;

            int skipSize = 0;
            int currentPos = 0;
            
            int[] lengths = Array.ConvertAll(input.Split(','), int.Parse);

            Func<int, int> rIdx = (int idx) => (currentPos + idx) % sequenceLength;
            
            foreach (int len in lengths)
            {
                if (len > sequenceLength)
                {
                    Console.WriteLine($"Invalid len {len}");
                    continue;
                }
                
                for (int k = 0; k < len / 2; k++)
                {
                    int idxL = rIdx(k);
                    int idxH = rIdx(len - 1 - k);
                    
                    int tmp = sequence[idxL];
                    sequence[idxL] = sequence[idxH];
                    sequence[idxH] = tmp;
                }
                
                currentPos += len + skipSize;
                skipSize++;
            }

            if (sequence.Length >= 2)
            {
                result = sequence[0] * sequence[1];
            }
            
            Console.WriteLine($"Multiply result is '{result}'");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(int sequenceLength, string input)
        {
            var sw = new Stopwatch();

            sw.Start();
            
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
                    if (len > sequenceLength)
                    {
                        Console.WriteLine($"Invalid len {len}");
                        continue;
                    }
                
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
            
            string hash = BitConverter.ToString(sparseHash).Replace("-", "").ToLower();
            
            Console.WriteLine($"Dense hash: '{hash}'");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }
    }
}