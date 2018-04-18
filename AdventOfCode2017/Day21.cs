using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Schema;

namespace AdventOfCode2017
{
    internal class Day21
    {
        internal static void Solve1Half(int itr, string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            var square = SquareFromString(".#./..#/###");
            var transformIn = new List<char[,]>();
            var transformOut = new List<char[,]>();

            string s = "";
            
            foreach (var line in File.ReadLines(filePath))
            {
                string[] inOut = line.Split(new string[] {" => "}, StringSplitOptions.None);

                var inSquare = SquareFromString(inOut[0]);
                var outSquare = SquareFromString(inOut[1]);

//                transformIn.Add(inSquare);
//                transformOut.Add(outSquare);
                
//                s += inOut[0] + "|org\n";
//                
                for (int i = 0; i < 4; i++)
                {
                    transformIn.Add(RotateSquareBy90(inSquare, i));
                    transformOut.Add(outSquare);
                    
//                    s += SquareToString(RotateSquareBy90(inSquare, i)) + $"rot:{i}\n";
                }
//
                transformIn.Add(FlipSquareHorizontaly(inSquare));
                transformOut.Add(outSquare);
//                
//                s += SquareToString(FlipSquareHorizontaly(inSquare)) + "flipH\n";
//
                transformIn.Add(FlipSquareVerticaly(inSquare));
                transformOut.Add(outSquare);
//                
//                s += SquareToString(FlipSquareVerticaly(inSquare)) + "flipV\n";
            }
            
//            File.WriteAllText("../../data/d21_out.txt", s);
            
            int len;

            for (int i = 0; i < itr; i++)
            {
                len = square.GetLength(0);

                if (len % 2 == 0)
                {
                    square = TransformSquare(square, 2, transformIn, transformOut);
                }
                else if (len % 3 == 0)
                {
                    square = TransformSquare(square, 3, transformIn, transformOut);
                }
            }

            int count = 0;

            len = square.GetLength(0);

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (square[i, j] == '#') count++;
                }
            }

            sw.Stop();

            Console.WriteLine($"Count of pixels turned on '{count}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(int itr, string filePath)
        {
            Solve1Half(itr, filePath);
        }

        private static char[,] SquareFromString(string input)
        {
            string[] parts = input.Split('/');

            var s = new char[parts.Length, parts.Length];

            for (int j = 0; j < parts.Length; j++)
            {
                string p = parts[j];
                for (int i = 0; i < p.Length; i++)
                {
                    s[j, i] = p[i];
                }
            }

            return s;
        }

        private static char[,] TransformSquare(char[,] s, int div, List<char[,]> inS, List<char[,]> outS)
        {
            int len = s.GetLength(0);
            int r = len / div;
            int nDiv = div + 1;
            int nLen = nDiv * r;

            var retS = new char[nLen, nLen];

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    var vars = new List<char[,]>();
                    
                    var subSquare = CopySubSquare(s, div, i * div, j * div);
                    vars.Add(subSquare);
                    vars.Add(RotateSquareBy90(vars[vars.Count - 1]));
                    vars.Add(RotateSquareBy90(vars[vars.Count - 1]));
                    vars.Add(RotateSquareBy90(vars[vars.Count - 1]));

                    vars.Add(FlipSquareHorizontaly(subSquare));
                    vars.Add(FlipSquareVerticaly(subSquare));

                    foreach (var v in vars)
                    {
                        bool found = false;
                        for (int k = 0; k < inS.Count; k++)
                        {
                            if (AreSquaresIdentical(inS[k], v))
                            {
                                FillSubSquare(retS, outS[k], i * nDiv, j * nDiv);
                                found = true;
                                break;
                            }
                        }
                        if (found) break;
                    }
                }
            }

            return retS;
        }

        private static char[,] RotateSquareBy90(char[,] s, int count)
        {
            char[,] retS = CopySquare(s);

            for (int i = 0; i < count; i++) retS = RotateSquareBy90(retS);

            return retS;
        }

        private static char[,] RotateSquareBy90(char[,] s)
        {
            int len = s.GetLength(0);
            int li = len - 1;
            
            var retS = new char[len, len];

            for (int i = 0; i <= li; i++)
            {
                for (int j = 0; j <= li; j++)
                {
                    retS[j, li - i] = s[i, j];
                }
            }

            return retS;
        }

        private static char[,] FlipSquareVerticaly(char[,] s)
        {
            int len = s.GetLength(0);
            int li = len - 1;
            
            var retS = new char[len, len];

            for (int i = 0; i <= li; i++)
            {
                for (int j = 0; j <= li; j++)
                {
                    retS[li - i, j] = s[i, j];
                }
            }

            return retS;
        }

        private static char[,] FlipSquareHorizontaly(char[,] s)
        {
            int len = s.GetLength(0);
            int li = len - 1;
            
            var retS = new char[len, len];

            for (int i = 0; i <= li; i++)
            {
                for (int j = 0; j <= li; j++)
                {
                    retS[i, li - j] = s[i, j];
                }
            }

            return retS;
        }

        private static char[,] CopySquare(char[,] s)
        {
            int len = s.GetLength(0);
            var retS = new char[len, len];

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    retS[i, j] = s[i, j];
                }
            }

            return retS;
        }

        private static char[,] CopySubSquare(char[,] s, int subLen, int xOffset, int yOffset)
        {
            var retS = new char[subLen, subLen];

            for (int i = 0; i < subLen; i++)
            {
                for (int j = 0; j < subLen; j++)
                {
                    retS[i, j] = s[xOffset + i, yOffset + j];
                }
            }

            return retS;
        }

        private static void FillSubSquare(char[,] s, char[,] subSquare, int xOffset, int yOffset)
        {
            int subLen = subSquare.GetLength(0);

            for (int i = 0; i < subLen; i++)
            {
                for (int j = 0; j < subLen; j++)
                {
                    s[xOffset + i, yOffset + j] = subSquare[i, j];
                }
            }
        }

        private static bool AreSquaresIdentical(char[,] s1, char[,] s2)
        {
            int len = s1.GetLength(0);

            if (len != s2.GetLength(0)) return false;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (s1[i, j] != s2[i, j]) return false;
                }
            }

            return true;
        }

        private static string SquareToString(char[,] s)
        {
            string ret = "";
            
            int len = s.GetLength(0);
            
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    ret += s[i, j];
                }
                
                if (i < len - 1) ret += '/';
            }

            return ret;
        }
    }
}