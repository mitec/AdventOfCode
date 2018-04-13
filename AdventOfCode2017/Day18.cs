using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2017
{
    internal class Day18
    {
        private class Prog
        {
            internal readonly Queue<long> RecBuff;
            
            private readonly Dictionary<char, long> regs;
            private readonly string[] cmds;
            private long it;
            private int sendCount;
            private bool isRec;

            internal Prog(int id, string[] cmds)
            {
                this.cmds = cmds;
                
                regs = new Dictionary<char, long>{{'p', id}};
                RecBuff = new Queue<long>();
                it = 0;
                sendCount = 0;
                isRec = false;
            }

            internal bool IsRec
            {
                get { return isRec && RecBuff.Count == 0; } 
            }

            internal bool IsFinished
            {
                get { return (it < 0 || it >= cmds.Length); }
            }
            
            internal int SendCount
            {
                get { return sendCount; }
            }
            
            internal void ProcessCmds(Queue<long> recBuff)
            {
                while (!IsFinished)
                {
                    string cmd = cmds[it];
                
                    string ins = cmd.Substring(0, 3);

                    long rv;
                    char r = cmd[4];
                    
                    if (char.IsLetter(r))
                    {
                        regs.TryGetValue(r, out rv);
                    }
                    else
                    {
                        rv = int.Parse(r.ToString());
                        r = char.MinValue;
                    }

                    if (ins == "snd")
                    {
                        recBuff.Enqueue(rv);
                        sendCount++;
                    }
                    else if (ins == "set")
                    {
                        regs[r] = GetValue(cmd, regs);
                    }
                    else if (ins == "add")
                    {
                        regs[r] = rv + GetValue(cmd, regs);
                    }
                    else if (ins == "mul")
                    {
                        regs[r] = rv * GetValue(cmd, regs);
                    }
                    else if (ins == "mod")
                    {
                        regs[r] = rv % GetValue(cmd, regs);
                    }
                    else if (ins == "rcv")
                    {
                        if (RecBuff.Count == 0)
                        {
                            isRec = true;
                            break;
                        }
                        else
                        {
                            isRec = false;
                            regs[r] = RecBuff.Dequeue();
                        }
                    }
                    else if (ins == "jgz")
                    {
                        if (rv > 0)
                        {
                            it += GetValue(cmd, regs);
                            continue;
                        }
                    }

                    it++;
                }
            }
        }
        
        internal static void Solve1Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            long f = 0;

            string[] cmds = File.ReadAllLines(filePath);
            var regs = new Dictionary<char, long>();
            
            long i = 0;
            while (i >= 0 && i < cmds.Length)
            {
                string cmd = cmds[i];

                string ins = cmd.Substring(0, 3);

                long rv;
                char r = cmd[4];
                    
                if (char.IsLetter(r))
                {
                    regs.TryGetValue(r, out rv);
                }
                else
                {
                    rv = int.Parse(r.ToString());
                    r = char.MinValue;
                }

                if (ins == "snd")
                {
                    f = rv;
                }
                else if (ins == "set")
                {
                    regs[r] = GetValue(cmd, regs);
                }
                else if (ins == "add")
                {
                    regs[r] = rv + GetValue(cmd, regs);
                }
                else if (ins == "mul")
                {
                    regs[r] = rv * GetValue(cmd, regs);
                }
                else if (ins == "mod")
                {
                    regs[r] = rv % GetValue(cmd, regs);
                }
                else if (ins == "rcv")
                {
                    if (rv > 0) break;
                }
                else if (ins == "jgz")
                {
                    if (rv > 0)
                    {
                        i += GetValue(cmd, regs);
                        continue;
                    }
                }

                i++;
            }
            
            sw.Stop();

            Console.WriteLine($"Value of recovered frequency is '{f}'  in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(string filePath)
        {
            var sw = new Stopwatch();

            sw.Start();

            string[] cmds = File.ReadAllLines(filePath);
            
            var p0 = new Prog(0, cmds);
            var p1 = new Prog(1, cmds);

            while (true)
            {
                p0.ProcessCmds(p1.RecBuff);
                p1.ProcessCmds(p0.RecBuff);

                if (p0.IsFinished && p1.IsFinished || p0.IsRec && p1.IsRec)
                {
                    break;
                }
            }
            
            sw.Stop();

            Console.WriteLine($"P1 sent {p1.SendCount}x in {sw.ElapsedMilliseconds}");
        }

        private static long GetValue(string cmd, Dictionary<char, long> regs)
        {
            long v;
            if (char.IsLetter(cmd[6]))
                regs.TryGetValue(cmd[6], out v);
            else
                v = int.Parse(cmd.Substring(6));

            return v;
        }
    }
}