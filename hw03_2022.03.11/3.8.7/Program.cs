using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3._8._7
{
    class GraphDna
    {
        public static bool IsEdge(string from, string to)
        {
            return from.Substring(1) == to.Substring(0, to.Length - 1);
        }

        public static Dictionary<string, List<string>> GetGraph(List<string> v)
        {
            Dictionary<string, List<string>> graph = new();
            foreach (var from in v)
            {
                graph[from] = new();
                foreach (var to in v)
                {
                    if (IsEdge(from, to))
                        graph[from].Add(to);
                }
            }
            return graph;
        }
    }

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
                Graph = GraphDna.GetGraph(f.ReadLine().Split(' ').ToList());
                CalcBalance();
            }
        }

        private void CalcBalance()
        {
            foreach (var from in Graph.Keys)
            {
                ChangeBalance(from, Graph[from].Count);

                foreach (var to in Graph[from])
                    ChangeBalance(to, -1);
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
            data = new Input("dataset_664473_7.txt");
            Graph<string> graph = new(data.Graph, data.Balance);
            var path = graph.EulerianPath();
            int k = path[0].Length;
            Console.Write(path[0].Substring(0, k - 1));
            path.ForEach(x => Console.Write($"{x.Substring(k - 1)}"));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}


