using System;

namespace _8._14._7
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664579_7.txt");
            DistanceMatrix m = new(input.matrix, input.n);
            m.UPGMA();
        }
    }
}
