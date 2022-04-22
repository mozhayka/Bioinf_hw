using System;

namespace _7._6._8
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664552_8.txt");
            DistanceMatrix m = new(input.matrix, input.n);
            m.UPGMA();
        }
    }
}
