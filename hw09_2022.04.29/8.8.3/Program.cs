using System;
using System.Linq;

namespace _8._8._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664573_3.txt");
            Point.Print(ClusteringAlgorithms.LloydAlgorithm(input.points, input.k));
        }
    }
}
