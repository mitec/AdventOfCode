using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day24
    {
        private class Pipe
        {
            private int inPort;
            private int outPort;
            private List<Pipe> childs;

            public int InPort => inPort;
            public int OutPort => outPort;
            public IEnumerable<Pipe> Childs => childs;

            public Pipe(int inPort, int outPort)
            {
                this.inPort = inPort;
                this.outPort = outPort;

                childs = new List<Pipe>();
            }

            public Pipe(Pipe org) : this(org.inPort, org.outPort)
            {
            }

            public void AddChild(Pipe child)
            {
                childs.Add(child);
            }

            public void SwapPorts()
            {
                int t = inPort;
                inPort = outPort;
                outPort = t;
            }
        }

        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            var pipes = ParseInput(filePath);
            int maxStregth = 0;

            var root = new Pipe(0, 0);
            BuildPipeline(root, pipes);
            FindStrongestPipeline(root, 0, ref maxStregth);

            sw.Stop();

            Console.WriteLine($"Strongest pipeline is '{maxStregth}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            var pipes = ParseInput(filePath);
            int maxStregth = 0;
            int maxLen = 0;

            var root = new Pipe(0, 0);
            BuildPipeline(root, pipes);
            FindLongestStrongestPipeline(root, 0, ref maxStregth, 0, ref maxLen);

            sw.Stop();

            Console.WriteLine($"Longest strongest pipeline is '{maxStregth}' in {sw.ElapsedMilliseconds}");
        }

        private static List<Pipe> ParseInput(string filePath)
        {
            var pipes = new List<Pipe>();

            foreach (var l in File.ReadLines(filePath))
            {
                int p = l.IndexOf('/');
                int inPort = int.Parse(l.Substring(0, p));
                int outPort = int.Parse(l.Substring(p + 1));

                pipes.Add(new Pipe(inPort, outPort));
            }

            return pipes;
        }

        private static void BuildPipeline(Pipe parent, IList<Pipe> pipes)
        {
            for (int i = 0; i < pipes.Count; i++)
            {
                var p = pipes[i];

                if (parent.InPort == p.InPort && parent.OutPort == p.OutPort ||
                    parent.InPort == p.OutPort && parent.OutPort == p.InPort)
                {
                    return;
                }

                if (p.InPort == parent.OutPort || p.OutPort == parent.OutPort)
                {
                    var nPipes = new List<Pipe>(pipes);
                    nPipes.RemoveAt(i);

                    var np = new Pipe(p);
                    if (p.InPort != parent.OutPort) np.SwapPorts();
                    parent.AddChild(np);

                    BuildPipeline(np, nPipes);
                }
            }
        }

        private static void FindStrongestPipeline(Pipe pp, int stregth, ref int maxStrength)
        {
            stregth += pp.InPort + pp.OutPort;

            if (pp.Childs.Any())
            {
                foreach (var p in pp.Childs)
                {
                    FindStrongestPipeline(p, stregth, ref maxStrength);
                }
            }
            else
            {
                if (stregth > maxStrength) maxStrength = stregth;
            }
        }

        private static void FindLongestStrongestPipeline(Pipe pp, int stregth, ref int maxStrength, int len,
            ref int maxLen)
        {
            stregth += pp.InPort + pp.OutPort;
            len++;

            if (pp.Childs.Any())
            {
                foreach (var p in pp.Childs)
                {
                    FindLongestStrongestPipeline(p, stregth, ref maxStrength, len, ref maxLen);
                }
            }
            else
            {
                if (len >= maxLen && stregth >= maxStrength)
                {
                    maxLen = len;
                    maxStrength = stregth;
                }
            }
        }
    }
}