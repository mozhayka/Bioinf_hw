using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3._8._6
{
    class Graph<T>
    {
        Dictionary<T, List<T>> graph;
        Dictionary<T, int> balance;
        Dictionary<T, HashSet<T>> used = new();
        List<T> path = new();

        public Graph(Dictionary<T, List<T>> graph, Dictionary<T, int> balance)
        {
            this.graph = graph;
            this.balance = balance;
            foreach (var key in balance.Keys)
                used[key] = new();
        }

        private T FindStart()
        {
            foreach (var key in balance.Keys)
            {
                if (balance[key] == 1)
                    return key;
            }
            return balance.Keys.GetEnumerator().Current;
        }

        public List<T> EulerianPath()
        {
            var from = FindStart();
            path.Add(from);
            
            while (true)
            {
                bool endWithBreak = false;
                foreach (var to in graph[from])
                {
                    if (!used[from].Contains(to))
                    {
                        used[from].Add(to);
                        path.Add(to);
                        from = to;
                        endWithBreak = true;
                        break;
                    }
                }
                if (!endWithBreak)
                    break;
            }
            while (UpgradePath())
            { }
            return path;
        }

        private bool UpgradePath()
        {
            //List<T> ans = curPath;
            for (int i = 0; i < path.Count; i++)
            {
                T cur = path[i];
                if (used[cur].Count == graph[cur].Count)
                    continue;

                List<T> newCycle = FindCycle(cur);
                path = path.Take(i + 1).Concat(newCycle).Concat(path.Skip(i + 1)).ToList();
                return true;
            }
            return false;
        }

        private List<T> FindCycle(T start)
        {
            var from = start;
            List<T> ans = new();

            while (true)
            {
                foreach (var to in graph[from])
                {
                    if (!used[from].Contains(to))
                    {
                        used[from].Add(to);
                        ans.Add(to);
                        from = to;
                        break;
                    }
                }
                if (from.Equals(start))
                    break;
            }
            return ans;
        }
    }

    class Input
    {
        public Dictionary<string, List<string>> Graph { get; } = new();
        public Dictionary<string, int> Balance { get; } = new();

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                while (!f.EndOfStream)
                {
                    var s = f.ReadLine().Split(' ');
                    var from = s[0].Substring(0, s[0].Length - 1);

                    ChangeBalance(from, s.Length - 1);
                    if (!Graph.ContainsKey(from))
                        Graph[from] = new();

                    for (int i = 1; i < s.Length; i++)
                    {
                        ChangeBalance(s[i], -1);
                        Graph[from].Add(s[i]);
                        if (!Graph.ContainsKey(s[i]))
                            Graph[s[i]] = new();
                    }
                }
            }
        }

        private void ChangeBalance(string v, int diff)
        {
            if (!Balance.ContainsKey(v))
                Balance[v] = diff;
            else
                Balance[v] += diff;
        }
    }

    class Program
    {
        static Input data;

        static void Do()
        {
            data = new Input("dataset_664473_6.txt");
            Graph<string> graph = new(data.Graph, data.Balance);
            graph.EulerianPath().ForEach(x => Console.Write($"{x} "));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
