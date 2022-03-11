using System;
using System.Collections.Generic;
using System.IO;

namespace _3._5._8
{
    class GraphDna
    {
        Dictionary<string, List<string>> graph = new(); 
        public static bool IsEdge(string from, string to)
        {
            return from.Substring(1) == to.Substring(0, to.Length - 1);
        }

        public void AddEdge(string from, string to)
        {
            if (!graph.ContainsKey(from))
                graph[from] = new();
            graph[from].Add(to);
        }

        public void PringGraph()
        {
            foreach (var from in graph.Keys)
            {
                Console.Write($"{from}:");
                foreach (var to in graph[from])
                {
                    Console.Write($" {to}");
                }
                Console.WriteLine();
            }
        }
    }

    class Input
    {
        public readonly string[] text;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                text = f.ReadLine().Split(' ');
            }
        }
    }

    class Program
    {
        static Input data;

        static void Do()
        {
            data = new Input("dataset_664470_8.txt");
            GraphDna graph = new();
            foreach (var word in data.text)
            {
                graph.AddEdge(word.Substring(0, word.Length - 1), word.Substring(1));
            }
            graph.PringGraph();
        }

        static void Main(string[] args)
        {
            Do();
        }
    }

}
