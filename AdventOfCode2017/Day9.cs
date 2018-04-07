using System;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day9
    {
        internal static void Solve1Half(string filePath)
        {
            int groupScore = 0;

            char[] data = File.ReadAllText(filePath).ToCharArray();

            bool doCancel = false;
            bool isGarbage = false;
            int groupNestingLevel = 0;
            
            foreach (char c in data)
            {
                if (doCancel)
                {
                    doCancel = false;
                    continue;
                }
                
                switch (c)
                {
                    case '!':
                        doCancel = true;
                        break;

                    case '<':
                        isGarbage = true;
                        break;
                        
                    case '>':
                        isGarbage = false;
                        break;
                        
                    case '{':
                        if (!isGarbage) groupNestingLevel++;
                        break;
                        
                    case '}':
                        if (!isGarbage)
                        {
                            groupScore += groupNestingLevel;
                            groupNestingLevel--;
                        }
                        break;
                }
            }

            Console.WriteLine($"Group score is: '{groupScore}'");
        }

        internal static void Solve2Half(string filePath)
        {
            int charCount = 0;

            char[] data = File.ReadAllText(filePath).ToCharArray();

            bool doCancel = false;
            bool isGarbage = false;
            
            foreach (char c in data)
            {
                if (doCancel)
                {
                    doCancel = false;
                    continue;
                }

                if (c == '!')
                {
                    doCancel = true;
                    continue;
                }

                if (c == '<' && !isGarbage)
                {
                    isGarbage = true;
                    continue;
                }

                if (c == '>')
                {
                    isGarbage = false;
                    continue;
                }

                if (isGarbage) charCount++;
            }

            Console.WriteLine($"Char count in garbage: '{charCount}'");
        }
    }
}