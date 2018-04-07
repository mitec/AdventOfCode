using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Day8
    {
        internal static void Solve1Half(string filePath)
        {
            int maxValue = 0;
            var vars = new Dictionary<string, int>();

            var regex = new Regex("([a-zA-Z]+) (inc|dec) (-?\\d+) if ([a-zA-Z]+) (>|<|>=|<=|==|!=) (-?\\d+)");
            
            foreach (string line in File.ReadLines(filePath))
            {
                if (line == "") continue;

                var match = regex.Match(line);

                string var1Name = match.Groups[1].Value;
                string operation = match.Groups[2].Value;
                int var1Value = int.Parse(match.Groups[3].Value);
                
                string var2Name = match.Groups[4].Value;
                string cond = match.Groups[5].Value;
                int var2Value = int.Parse(match.Groups[6].Value);
                
                if (!vars.ContainsKey(var1Name)) vars[var1Name] = 0;
                if (!vars.ContainsKey(var2Name)) vars[var2Name] = 0;

                bool canDoOperation = false;
                
                if (cond == ">")
                    canDoOperation = (vars[var2Name] > var2Value);
                else if (cond == ">=")
                    canDoOperation = (vars[var2Name] >= var2Value);
                else if (cond == "<")
                    canDoOperation = (vars[var2Name] < var2Value);
                else if (cond == "<=")
                    canDoOperation = (vars[var2Name] <= var2Value);
                else if (cond == "==")
                    canDoOperation = (vars[var2Name] == var2Value);
                else if (cond == "!=")
                    canDoOperation = (vars[var2Name] != var2Value);
                else
                    throw new Exception($"Unhandled cond '{cond}'");

                if (canDoOperation)
                {
                    if (operation == "inc")
                        vars[var1Name] += var1Value;
                    else if (operation == "dec")
                        vars[var1Name] -= var1Value;
                    else
                        throw new Exception($"Unhandled '{operation}'");
                }

                maxValue = vars.Values.Max();
            }
            
            Console.WriteLine($"Max registry value is '{maxValue}'");
        }

        internal static void Solve2Half(string filePath)
        {
            int highstValue = 0;
            var vars = new Dictionary<string, int>();

            var regex = new Regex("([a-zA-Z]+) (inc|dec) (-?\\d+) if ([a-zA-Z]+) (>|<|>=|<=|==|!=) (-?\\d+)");
            
            foreach (string line in File.ReadLines(filePath))
            {
                if (line == "") continue;

                var match = regex.Match(line);

                string var1Name = match.Groups[1].Value;
                string operation = match.Groups[2].Value;
                int var1Value = int.Parse(match.Groups[3].Value);
                
                string var2Name = match.Groups[4].Value;
                string cond = match.Groups[5].Value;
                int var2Value = int.Parse(match.Groups[6].Value);
                
                if (!vars.ContainsKey(var1Name)) vars[var1Name] = 0;
                if (!vars.ContainsKey(var2Name)) vars[var2Name] = 0;

                bool canDoOperation = false;
                
                if (cond == ">")
                    canDoOperation = (vars[var2Name] > var2Value);
                else if (cond == ">=")
                    canDoOperation = (vars[var2Name] >= var2Value);
                else if (cond == "<")
                    canDoOperation = (vars[var2Name] < var2Value);
                else if (cond == "<=")
                    canDoOperation = (vars[var2Name] <= var2Value);
                else if (cond == "==")
                    canDoOperation = (vars[var2Name] == var2Value);
                else if (cond == "!=")
                    canDoOperation = (vars[var2Name] != var2Value);
                else
                    throw new Exception($"Unhandled cond '{cond}'");

                if (canDoOperation)
                {
                    if (operation == "inc")
                        vars[var1Name] += var1Value;
                    else if (operation == "dec")
                        vars[var1Name] -= var1Value;
                    else
                        throw new Exception($"Unhandled '{operation}'");

                    if (vars[var2Name] > highstValue)
                        highstValue = vars[var2Name];
                    
                    if (vars[var1Name] > highstValue)
                        highstValue = vars[var1Name];
                }
            }
            
            Console.WriteLine($"Highst value in registry was '{highstValue}'");
        }
    }
}