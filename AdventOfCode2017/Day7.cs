using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    internal class Day7
    {
        private class Node
        {
            internal readonly string Name;
            internal readonly IEnumerable<string> ChildNames;
            internal readonly int Weight;
            
            private HashSet<Node> children;
            
            internal int TotalWeight
            {
                get
                {
                    int totalWeight = Weight;

                    foreach (var child in children)
                    {
                        totalWeight += child.TotalWeight;
                    }
                    
                    return totalWeight;
                }
            }

            internal IEnumerable<Node> Children
            {
                get { return children; }
            }
            
            internal Node(string name, int weight, IEnumerable<string> childNames)
            {
                Name = name;
                Weight = weight;
                ChildNames = childNames;
                children = new HashSet<Node>();
            }

            internal void AddChild(Node child)
            {
                children.Add(child);
            }
        }
        
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            var roots = new HashSet<string>();
            var children = new HashSet<string>();
            
            foreach (string line in File.ReadLines(filePath))
            {
                if (line.Contains("->"))
                {
                    var matches = Regex.Matches(line, "[a-zA-Z]+");
                    
                    roots.Add(matches[0].Value);

                    for (int i = 1; i < matches.Count; i++)
                    {
                        children.Add(matches[i].Value);
                    }
                }
            }

            string bottomRoot = null;
            foreach (var root in roots)
            {
                if (!children.Contains(root))
                {
                    bottomRoot = root;
                    break;
                }
            }
            
            Console.WriteLine($"Root program name is '{bottomRoot}'");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();
            
            var nameToNodeMap = new Dictionary<string, Node>();
            
            foreach (string line in File.ReadLines(filePath))
            {
                var matches = Regex.Matches(line, "\\w+");
                
                var childNames = new HashSet<string>();
                
                for (int i = 2; i < matches.Count; i++)
                {
                    childNames.Add(matches[i].Value);
                }
                
                var node = new Node(matches[0].Value, int.Parse(matches[1].Value), childNames);

                nameToNodeMap.Add(node.Name, node);
            }

            foreach (var node in nameToNodeMap.Values)
            {
                foreach (var childName in node.ChildNames)
                {
                    node.AddChild(nameToNodeMap[childName]);
                }
            }
            
            //int correctWeight = 0;

            foreach (var node in nameToNodeMap.Values)
            {
                var weightToNodes = new Dictionary<int, List<Node>>();
                
                foreach (var child in node.Children)
                {
                    if (!weightToNodes.ContainsKey(child.TotalWeight))
                    {
                        weightToNodes[child.TotalWeight] = new List<Node>();
                    }
                    
                    weightToNodes[child.TotalWeight].Add(child);
                }

                if (weightToNodes.Count > 1)
                {
                    var invalidNode = weightToNodes.Aggregate((l, r) => l.Value.Count < r.Value.Count ? l : r).Value[0];

                    var s = new List<string>();
                    foreach (var kv in weightToNodes)
                    {
                        s.Add($"{kv.Key} => {kv.Value.Count}");
                    }
                    
                    Console.WriteLine($"{node.Name} -> {string.Join(", ", node.ChildNames)} : '{string.Join(", ", s)}' {invalidNode.Name} : {invalidNode.Weight}, {invalidNode.TotalWeight}");
                    // spravna odpoved = najdi posledny node s nespravnymi vahami a od jeho vahy odcitaj rozdiel celkovej ok vahy a celkovej wrong vahy
                }
            }
            
            //Console.WriteLine($"Correct weight '{correctWeight}'");
            
            sw.Stop();
            
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds}");
        }
    }
}