using System;

namespace _3
{
    //https://github.com/mozhayka/Bioinf_hw/tree/master/hw09_2022.04.29/8.8.3
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
