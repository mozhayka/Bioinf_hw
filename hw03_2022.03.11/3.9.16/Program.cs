using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _3._9._16
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

        public static bool IsPairEdge(string from, string to, int k)
        {
            return IsEdge(from.Substring(0, k), to.Substring(0, k)) &&
                IsEdge(from.Substring(k + 1, k), to.Substring(k + 1, k));
        }

        public static Dictionary<string, List<string>> GetPairGraph(List<string> v, int k)
        {
            Dictionary<string, List<string>> graph = new();
            foreach (var from in v)
            {
                graph[from] = new();
                foreach (var to in v)
                {
                    if (IsPairEdge(from, to, k))
                        graph[from].Add(to);
                }
            }
            return graph;
        }
    }

    class Graph<T>
    {
        Dictionary<T, List<T>> graph;
        Dictionary<T, int> balance = new();
        Dictionary<T, HashSet<T>> used = new();
        List<T> path = new();

        public Graph(Dictionary<T, List<T>> graph)
        {
            this.graph = graph;
            CalcBalance();
            foreach (var key in balance.Keys)
                used[key] = new();
        }

        private void CalcBalance()
        {
            foreach (var from in graph.Keys)
            {
                ChangeBalance(from, graph[from].Count);

                foreach (var to in graph[from])
                    ChangeBalance(to, -1);
            }
        }

        private void ChangeBalance(T v, int diff)
        {
            if (!balance.ContainsKey(v))
                balance[v] = diff;
            else
                balance[v] += diff;
        }

        private T FindStart()
        {
            foreach (var key in balance.Keys)
            {
                if (balance[key] == 1)
                    return key;
            }
            return balance.ElementAt(0).Key;
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
        public readonly int k, d;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var kd = f.ReadLine().Split(' ');
                k = Int32.Parse(kd[0]);
                d = Int32.Parse(kd[1]);
                Graph = GraphDna.GetPairGraph(f.ReadLine().Split(' ').ToList(), k);
            }
        }

        private List<string> GenVertex(int len)
        {
            if (len <= 0)
                return new List<string> { "" };
            List<string> ans = new List<string>();
            var words = GenVertex(len - 1);
            foreach (var word in words)
            {
                ans.Add(word + '1');
                ans.Add(word + '0');
            }
            return ans;
        }
    }

    class Program
    {
        static Input data;

        static void Do()
        {
            data = new Input("dataset_664474_16.txt");
            Graph<string> graph = new(data.Graph);
            var path = graph.EulerianPath();

            int k = data.k, d = data.d;
            string fst = "", snd = "";
            fst += path[0].Substring(0, k - 1);
            path.ToList().ForEach(x => fst += x.Substring(k - 1, 1));

            snd += path[0].Substring(k + 1, k - 1);
            path.ToList().ForEach(x => snd += x.Substring(2 * k, 1));

            Console.WriteLine(fst + snd.Substring(snd.Length - k - d, k + d));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
