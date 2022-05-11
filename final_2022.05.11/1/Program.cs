using System;

namespace _1
{
    // https://github.com/mozhayka/Bioinf_hw/tree/master/hw07_2022.04.15/6.4.4
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset2.txt");
            Permutation perm = new(input.permutation, input.permutation2);
            var sorted = perm.GreedySort();

            sorted.ForEach(x => Console.WriteLine(x));
        }
    }
}
