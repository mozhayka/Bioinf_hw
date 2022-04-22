using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._4._6
{
    class Tree
    {
        readonly List<List<int>> edges;
        readonly Dictionary<int, Dictionary<int, int>> distance;
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

        public void AddEdge(int from, int to, int dist)
        {
            //Console.WriteLine($"Add edge {from} {to} {dist}");
            edges[from].Add(to);
            edges[to].Add(from);
            distance[from][to] = dist;
            distance[to][from] = dist;
        }

        private void RemoveEdge(int from, int to)
        {
            //Console.WriteLine($"Remove edge {from} {to}");
            edges[from].Remove(to);
            edges[to].Remove(from);
        }

        public int SplitEdge(int from, int to, int distFrom)
        {
            int splitFrom, splitTo;
            var distToSplit = FindEdgeToSplit(from, to, distFrom, out splitFrom, out splitTo);
            int preSplitDist = distFrom - distToSplit;
            int afterSplitDist = distToSplit + distance[splitFrom][splitTo] - distFrom;

            if (preSplitDist != 0)
            {
                RemoveEdge(splitFrom, splitTo);
                N++;
                edges.Add(new());
                distance[N - 1] = new();
                AddEdge(splitFrom, N - 1, preSplitDist);
                AddEdge(N - 1, splitTo, afterSplitDist);

                return N - 1;
            }
            else
            {
                return splitFrom;
            }
        }

        private int FindEdgeToSplit(int from, int to, int distFrom, out int splitFrom, out int splitTo)
        {
            cur_path = new();
            used = new bool[N];
            DFS(from, to);
            int cur_dist = 0;
            path.Reverse();

            for (int i = 0; i < path.Count - 1; i++)
            {
                if (cur_dist <= distFrom && cur_dist + distance[path[i]][path[i + 1]] > distFrom)
                {
                    splitFrom = path[i];
                    splitTo = path[i + 1];
                    return cur_dist;
                }
                cur_dist += distance[path[i]][path[i + 1]];
            }
            throw new Exception($"Couldn't find place to split {cur_dist}");
        }

        private Stack<int> cur_path;
        private List<int> path;
        private bool[] used;

        private void DFS(int start, int end)
        {
            cur_path.Push(start);
            used[start] = true;
            foreach (var to in edges[start])
            {
                if (used[to])
                    continue;
                if (to == end)
                {
                    cur_path.Push(to);
                    path = cur_path.ToList();
                    cur_path.Pop();
                    return;
                }
                DFS(to, end);
            }
            cur_path.Pop();
        }

        public void Print()
        {
            for (int from = 0; from < N; from++)
            {
                foreach (var to in edges[from])
                {
                    Console.WriteLine($"{from}->{to}:{distance[from][to]}");
                }
            }
        }
    }
}
