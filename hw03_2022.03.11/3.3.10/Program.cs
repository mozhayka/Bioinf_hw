using System;
using System.IO;

namespace _3._3._10
{
    class GraphDna
    {
        public static bool IsEdge(string from, string to)
        {
            return from.Substring(1) == to.Substring(0, to.Length - 1);
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
            data = new Input("dataset_664468_10.txt");
            foreach (var from in data.text)
            {
                bool printed = false;
                foreach (var to in data.text)
                {
                    if (GraphDna.IsEdge(from, to))
                    {
                        if (!printed)
                        {
                            printed = true;
                            Console.Write($"{from}:");
                        }
                        Console.Write($" {to}");
                    }
                }
                if (printed)
                    Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
