using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._6._2
{
    class Input
    {
        public readonly int m, k;
        public List<Point> points;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var str = f.ReadLine().Split(' ');
                k = int.Parse(str[0]);
                m = int.Parse(str[1]);
                points = new();
                ReadPoints(f, m);
            }
        }

        private void ReadPoints(StreamReader f, int dim)
        {
            while (!f.EndOfStream)
            {
                var row = f.ReadLine()
                    .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                points.Add(new Point(row));
            }
        }
    }
}
