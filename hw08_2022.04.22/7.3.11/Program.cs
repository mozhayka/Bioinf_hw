using System;

namespace _7._3._11
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664549_11.txt");
            DistanceMatrix m = new(input.matrix, input.n);
            Console.WriteLine(m.LimbLength(input.j));
        }
    }
}
