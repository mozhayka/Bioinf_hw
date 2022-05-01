using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._14._7
{
    class Input
    {
        public int n { get; }
        public double[,] matrix { get; private set; }

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                n = int.Parse(f.ReadLine());
                ReadMatrix(f, n);
            }
        }

        private void ReadMatrix(StreamReader f, int n)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                var row = f.ReadLine().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = double.Parse(row[j]);
                }
            }
        }
    }
}
