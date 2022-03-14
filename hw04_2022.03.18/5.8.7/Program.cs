using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _5._8._7
{
    class Input
    {
        public readonly int from, to;
        public readonly int[,] weights;
        public readonly int INF = (int) 1e9;
        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var ft = f.ReadLine().Split(' ');
                from = Int32.Parse(ft[0]);
                to = Int32.Parse(ft[1]);
                weights = new int[to + 1, to + 1];

                for (int i = 0; i <= to; i++)
                    for (int j = 0; j <= to; j++)
                        weights[i, j] = -INF;

                while (!f.EndOfStream)
                {
                    var str = f.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    weights[str[0], str[1]] = Math.Max(str[2], weights[str[0], str[1]]);
                    
                }
            }
        }

    }

    class Program
    {
        static Input data;
        static int[] prev, len;

        static void LongestPath(int from, int to)
        {
            prev = new int[to + 1];
            len = new int[to + 1];
            prev[from] = -1;
            len[from] = 0;
            for (int i = from + 1; i <= to; i++)
            {
                len[i] = -data.INF;
                prev[i] = -1;
                for (int j = from; j < i; j++)
                {
                    if (len[j] + data.weights[j, i] > len[i])
                    {
                        len[i] = len[j] + data.weights[j, i];
                        prev[i] = j;
                    }
                }
            }
            
        }


        static void Do()
        {
            data = new Input("dataset_664516_72.txt");
            LongestPath(data.from, data.to);
            Console.WriteLine(len[data.to]);
            int cur = data.to;
            List<int> ans = new();
            while (cur != -1)
            {
                ans.Add(cur);
                cur = prev[cur];
            }
            ans.Reverse();
            ans.ForEach(x => Console.Write($"{x} "));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
