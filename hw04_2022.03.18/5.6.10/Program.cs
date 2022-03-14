using System;
using System.Collections.Generic;
using System.IO;

namespace _5._6._10
{
    class Input
    {
        public readonly int[,] down, right;
        public readonly int n, m;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var nm = f.ReadLine().Split(' ');
                n = Int32.Parse(nm[0]);
                m = Int32.Parse(nm[1]);

                down = ReadMatrix(n, m + 1, f);
                f.ReadLine();
                right = ReadMatrix(n + 1, m, f);
            }
        }

        private int[,] ReadMatrix(int rows, int cols, StreamReader f)
        {
            var ans = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                var str = f.ReadLine().Split(' ');
                for (int j = 0; j < cols; j++)
                {
                    ans[i, j] = Int32.Parse(str[j]);
                }
            }
            return ans;
        }
    }

    class Program
    {
        static Input data;

        static int ManhattanTourist(int n, int m, int[,] Down, int[,] Right)
        {
            var len = new int[n + 1, m + 1];
            len[0, 0] = 0;
            for (int i = 1; i <= n; i++)
                len[i, 0] = len[i - 1, 0] + Down[i - 1, 0];
            for (int j = 1; j <= m; j++)
                len[0, j] = len[0, j - 1] + Right[0, j - 1];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    len[i, j] = Math.Max(len[i - 1, j] + Down[i - 1, j],
                        len[i, j - 1] + Right[i, j - 1]);
                }
            }
            return len[n, m];
        }

        static void Do()
        {
            data = new Input("dataset_664514_10.txt");
            Console.WriteLine(ManhattanTourist(data.n, data.m, data.down, data.right));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
