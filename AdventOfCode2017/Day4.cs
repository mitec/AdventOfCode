using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day4
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            int validPhrasesCount = 0;
            
            foreach (var phrases in ParseInput(filePath))
            {
                bool isValid = true;
                
                Array.Sort(phrases);
                for (int i = 0; i < phrases.Length - 1; i++)
                {
                    if (phrases[i] == phrases[i + 1])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid) validPhrasesCount++;
            }
            
            PrintResult(validPhrasesCount);
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }
        
        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            int validPhrasesCount = 0;

            string[][] phrasesAll = ParseInput(filePath).ToArray();
            
            for (int i = 0; i < phrasesAll.Length; i++)
            {
                for (int j = 0; j < phrasesAll[i].Length; j++)
                {
                    phrasesAll[i][j] = String.Concat(phrasesAll[i][j].OrderBy(c => c));                    
                }
                
                Array.Sort(phrasesAll[i]);
            }
            
            foreach (var phrases in phrasesAll)
            {
                bool isValid = true;
                
                for (int i = 0; i < phrases.Length - 1; i++)
                {
                    if (phrases[i] == phrases[i + 1])
                    {
                        isValid = false;
                        break;
                    }
                }
                
                if (isValid) validPhrasesCount++;
            }
            
            PrintResult(validPhrasesCount);
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        private static IEnumerable<String[]> ParseInput(string filePath)
        {
            var data = new List<string[]>();

            foreach (string line in File.ReadLines(filePath))
            {
                if (line == "") continue;

                data.Add(line.Split(' '));
            }

            return data;
        }

        private static void PrintResult(int result)
        {
            Console.WriteLine($"num of valid phrases is '{result}'");
        }
    }
}