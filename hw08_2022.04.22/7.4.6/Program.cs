using System;

namespace _7._4._6
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664550_6.txt");
            DistanceMatrix m = new(input.matrix, input.n);
            m.AdditivePhylogeny();
        }
    }
}
