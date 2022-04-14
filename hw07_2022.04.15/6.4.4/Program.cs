using System;

namespace _6._4._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664532_4.txt");
            Permutation perm = new(input.permutation);
            var sorted = perm.GreedySort();

            sorted.ForEach(x => Console.WriteLine(x));
        }
    }
}
