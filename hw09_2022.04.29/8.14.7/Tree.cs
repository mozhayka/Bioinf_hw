using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._14._7
{
    class Tree
    {
        readonly List<List<int>> edges;
        readonly Dictionary<int, Dictionary<int, double>> distance;
        public int N { get; set; }

        public Tree(int n)
        {
            N = n;
            edges = new();
            distance = new();
            for (int i = 0; i < n; i++)
            {
                edges.Add(new());
                distance[i] = new();
            }
        }

        public void AddEdge(int from, int to, double dist)
        {
            //Console.WriteLine($"Add edge {from} {to} {dist}");
            edges[from].Add(to);
            edges[to].Add(from);
            distance[from][to] = dist;
            distance[to][from] = dist;
        }

        public void Print()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            for (int from = 0; from < N; from++)
            {
                foreach (var to in edges[from])
                {
                    Console.WriteLine($"{from}->{to}:" +
                        $"{string.Format("{0:f2}", distance[from][to])}");
                }
            }
        }
    }
}
