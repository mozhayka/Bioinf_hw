using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._3._11
{
    class Input
    {
        public int n { get; }
        public int j { get; }
        public int[,] matrix { get; private set; }

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                n = int.Parse(f.ReadLine());
                j = int.Parse(f.ReadLine());
                ReadMatrix(f, n);
            }
        }

        private void ReadMatrix(StreamReader f, int n)
        {
            matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var row = f.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(row[j]);
                }
            }
        }
    }
}
