﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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

            foreach (var line in File.ReadLines(filePath))
            {
                string[] inOut = line.Split(new string[] {" => "}, StringSplitOptions.None);

                var inSquare = SquareFromString(inOut[0]);
                var outSquare = SquareFromString(inOut[1]);

                transformIn.Add(inSquare);
                transformOut.Add(outSquare);

                int fromIndexToCheck = transformIn.Count - 1;
                
                char[,] squareVariant;

                for (int i = 1; i <= 3; i++)
                {
                    squareVariant = RotateSquareBy90(inSquare, i);

                    if (!SquareVariantExists(squareVariant, transformIn, fromIndexToCheck))
                    {
                        transformIn.Add(squareVariant);
                        transformOut.Add(outSquare);
                    }
                }

                squareVariant = FlipSquareHorizontaly(inSquare);

                if (!SquareVariantExists(squareVariant, transformIn, fromIndexToCheck))
                {
                    transformIn.Add(squareVariant);
                    transformOut.Add(outSquare);
                }

                var inSquareVariant = squareVariant;
                
                for (int i = 1; i <= 3; i++)
                {
                    squareVariant = RotateSquareBy90(inSquareVariant, i);

                    if (!SquareVariantExists(squareVariant, transformIn, fromIndexToCheck))
                    {
                        transformIn.Add(squareVariant);
                        transformOut.Add(outSquare);
                    }
                }
                
                squareVariant = FlipSquareVerticaly(inSquare);

                if (!SquareVariantExists(squareVariant, transformIn, fromIndexToCheck))
                {
                    transformIn.Add(squareVariant);
                    transformOut.Add(outSquare);
                }
                
                inSquareVariant = squareVariant;
                
                for (int i = 1; i <= 3; i++)
                {
                    squareVariant = RotateSquareBy90(inSquareVariant, i);

                    if (!SquareVariantExists(squareVariant, transformIn, fromIndexToCheck))
                    {
                        transformIn.Add(squareVariant);
                        transformOut.Add(outSquare);
                    }
                }
            }

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

        private static bool SquareVariantExists(char[,] s, List<char[,]> inS, int startPos)
        {
            for (int i = startPos; i < inS.Count; i++)
            {
                if (AreSquaresIdentical(s, inS[i]))
                {
                    return true;
                }
            }

            return false;
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
                    var subSquare = CopySubSquare(s, div, i * div, j * div);

                    for (int k = 0; k < inS.Count; k++)
                    {
                        if (AreSquaresIdentical(inS[k], subSquare))
                        {
                            FillSubSquare(retS, outS[k], i * nDiv, j * nDiv);
                            break;
                        }
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