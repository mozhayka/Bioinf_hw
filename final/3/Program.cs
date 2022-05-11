using System;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset.txt");
            Point.Print(ClusteringAlgorithms.BetterInitializer(input.points, input.k));
            //Point.Print(ClusteringAlgorithms.LloydAlgorithm(input.points, input.k));
        }
    }
}
