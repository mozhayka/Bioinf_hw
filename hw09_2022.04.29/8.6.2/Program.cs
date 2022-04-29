using System;

namespace _8._6._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664571_2.txt");
            Point.Print(ClusteringAlgorithms.FarthestFirstTraversal(input.points, input.k));
        }
    }
}
