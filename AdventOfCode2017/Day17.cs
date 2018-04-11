using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2017
{
    internal class Day17
    {
//        private class DoubleLinkedItem
//        {
//            internal readonly int Value;
//            internal DoubleLinkedItem Next;
//            internal DoubleLinkedItem Prev;
//            
//            public DoubleLinkedItem(int value)
//            {
//                Value = value;
//            }
//        }
//        
//        private class DoubleLinkedList
//        {
//            private DoubleLinkedItem r;
//            private DoubleLinkedItem c;
//            private int count = 0;
//
//            internal int Count
//            {
//                get { return count; }
//            }
//
//            internal DoubleLinkedItem Current
//            {
//                get { return c; }
//            }
//            
//            internal void Add(int offset, int value)
//            {
//                if (offset > 0)
//                {
//                    int i = offset + 1;
//                    while (--i > 0) c = c.Next;
//                }
//                else if (offset < 0)
//                {
//                    int i = offset * -1 + 1;
//                    while (--i > 0) c = c.Prev;
//                }
//                
//                var n = new DoubleLinkedItem(value);
//                if (c != null)
//                {
//                    n.Next = c.Next;
//                    n.Prev = c;
//                    if (c.Next != null) c.Next.Prev = n;
//                    c.Next = n;
//                }
//                c = n;
//                
//                count++;
//            }
//        }
        
        internal static void Solve1Half(int steps, int maxValue)
        {
            var sw = new Stopwatch();

            sw.Start();

            int p = 0;
            var l = new List<int>{0};
            
            for (int i = 1; i <= maxValue; i++)
            {
                p = (p + steps) % l.Count + 1;

                l.Insert(p, i);
            }

            sw.Stop();

            Console.WriteLine($"Value after {maxValue} is '{l[(p + 1) % l.Count]}' in {sw.ElapsedMilliseconds}");
        }

        internal static void Solve2Half(int steps, int maxValue)
        {
            var sw = new Stopwatch();

            sw.Start();

            int p = 0;
            
            //var l = new DoubleLinkedList();
            //l.Add(o, 0);
            
            int n = 0;
            //var f = l.Current;
            
            for (int i = 1; i <= maxValue; i++)
            {
                int c = i;
                //int c = l.Count;
                
                int r = steps % c;
                int o = (c - p > r ? r : -(c - r));
                p += o + 1;
                
                if (p == 1) n = i;
                //l.Add(o, i);
            }
 
            sw.Stop();

            Console.WriteLine($"Value after 0 is '{n}' in {sw.ElapsedMilliseconds}");
            //Console.WriteLine($"Value after 0 is '{f.Next.Value}' in {sw.ElapsedMilliseconds}");
        }
    }
}