using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    class Input
    {
        public readonly List<string> permutation;
        public readonly List<string> permutation2;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                permutation = f.ReadLine().Split(' ').ToList();
                permutation2 = f.ReadLine().Split(' ').ToList();
            }
        }

    }
}
