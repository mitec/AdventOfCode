using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    internal class Day16
    {
        internal static void Solve1Half(int programsCount, string filePath)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            char[] programs = InitPrograms(programsCount);
            IEnumerable<string> moves = ParseMoves(filePath);
            Dance(programs, moves);
            
            sw.Stop();

            Console.WriteLine($"Programs after dance '{ArrayToString(programs)}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(int programsCount, string filePath)
        {
            var sw = new Stopwatch();
            
            sw.Start();

            char[] initPrograms = InitPrograms(programsCount);
            char[] programs = (char[])initPrograms.Clone();
            IEnumerable<string> moves = ParseMoves(filePath);
            
            int i = 0;
            do
            {
                Dance(programs, moves);
                i++;
            }
            while (!programs.SequenceEqual(initPrograms));

            int r = 1000000000 % i;
            for (int j = 0; j < r; j++) Dance(programs, moves);
            
            sw.Stop();

            Console.WriteLine($"Matching pairs count '{ArrayToString(programs)}' {i} in {sw.ElapsedMilliseconds}");
        }

        private static void Dance(char[] programs, IEnumerable<string> moves)
        {
            int l = programs.Length - 1;
            
            foreach (string move in moves)
            {
                char moveType = move[0];
                
                if (moveType == 's')
                {
                    int c = int.Parse(move.Substring(1));
                    for (int j = 0; j < c; j++)
                    {
                        char t = programs[l];
                        for (int i = l; i > 0; i--) programs[i] = programs[i - 1];
                        programs[0] = t;
                    }
                }
                else if (moveType == 'x')
                {
                    int d = move.IndexOf('/');
                    int p1 = int.Parse(move.Substring(1, d - 1));
                    int p2 = int.Parse(move.Substring(d + 1));
                    
                    char t = programs[p1];
                    programs[p1] = programs[p2];
                    programs[p2] = t;
                }
                else if (moveType == 'p')
                {
                    int p1 = ProgramToPos(programs, move[1]);
                    int p2 = ProgramToPos(programs, move[3]);
                    
                    char t = programs[p1];
                    programs[p1] = programs[p2];
                    programs[p2] = t;
                }
            }
        }

        private static List<string> ParseMoves(string filePath)
        {
            var moves = new List<string>();
            foreach (string line in File.ReadLines(filePath))
            {
                if (line == "") continue;
                
                moves.AddRange(line.Split(','));
            }

            return moves;
        }

        private static char[] InitPrograms(int programsCount)
        {
            var programs = new char[programsCount];
            
            for (int i = 0; i < programsCount; i++)
            {
                programs[i] = (char)('a' + i);
            }

            return programs;
        }
        
        private static int ProgramToPos(char[] programs, char program)
        {
            for (int i = 0; i < programs.Length; i++) {
                if (programs[i] == program) return i;
            }

            return 0;
        }

        private static string ArrayToString(char[] array)
        {
            string ret = "";
            foreach (char c in array) ret += c;
            return ret;
        }
    }
}