using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    internal class Day6
    {
        internal static void Solve1Half(int[] input)
        {
            string orgInput = string.Join(", ", input);
            
            int cycleCount = 0;
            var states = new HashSet<string>();

            while (true)
            {
                string state = string.Join(",", input);
                //Console.WriteLine(state);
                if (states.Contains(state)) break;

                states.Add(state);
                
                int maxValue = 0;
                int maxPos = -1;
                
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] > maxValue)
                    {
                        maxPos = i;
                        maxValue = input[i];
                    }
                }

                input[maxPos] = 0;

                int j = maxPos;
                while (maxValue > 0)
                {
                    j = (j + 1) % input.Length;
                    input[j] += 1;
                    maxValue -= 1;
                }

                cycleCount++;
            }
            
            Console.WriteLine($"Num of cycles for input {orgInput} is {cycleCount}");
        }
        
        internal static void Solve2Half(int[] input)
        {
            Solve1Half(input);
            Solve1Half(input);
        }
    }
}