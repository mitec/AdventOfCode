using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Day12
    {
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            int grout0Count = 0;
            var idToIdsMap = ParseInput(filePath);
            var groupIds = new HashSet<int>() {0};

            foreach (var k in idToIdsMap.Keys)
            {
                if (IsConnectedToGroup(k, k, groupIds, idToIdsMap)) grout0Count++;
            }

            sw.Stop();

            Console.WriteLine($"Ids in group0: '{grout0Count}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            var idToIdsMap = ParseInput(filePath);
            var groupIds = new HashSet<int>();

            foreach (var k in idToIdsMap.Keys)
            {
                if (!IsConnectedToGroup(k, k, groupIds, idToIdsMap))
                {
                    groupIds.Add(k);
                }
            }

            sw.Stop();

            Console.WriteLine($"group count: '{groupIds.Count}' in {sw.ElapsedMilliseconds}");
        }

        private static Dictionary<int, int[]> ParseInput(string filePath)
        {
            var rgx = new Regex("\\s");
            var idToIdsMap = new Dictionary<int, int[]>();

            foreach (var input in File.ReadLines(filePath))
            {
                if (input == "") continue;

                string[] parts = rgx.Replace(input, "").Split(new[] {"<->"}, StringSplitOptions.None);

                int id = int.Parse(parts[0]);
                int[] ids = Array.ConvertAll(parts[1].Split(','), int.Parse);

                idToIdsMap.Add(id, ids);
            }

            return idToIdsMap;
        }

        private static bool IsConnectedToGroup(int k, int parentId, HashSet<int> groupIds, Dictionary<int, int[]> allIds)
        {
            if (groupIds.Contains(k)) return true;

            foreach (int c in allIds[k])
            {
                if (c == parentId) continue;

                if (IsConnectedToGroup(c, k, groupIds, allIds)) return true;
            }

            return false;
        }
    }
}