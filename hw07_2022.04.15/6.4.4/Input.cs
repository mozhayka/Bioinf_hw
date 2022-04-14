using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._4._4
{
    class Input
    {
        public readonly List<string> permutation;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                permutation = f.ReadLine().Split(' ').ToList();
            }
        }

    }
}
