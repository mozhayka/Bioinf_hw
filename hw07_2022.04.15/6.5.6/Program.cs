using System;

namespace _6._5._6
{
    class Program
    {
        static void Main(string[] args)
        {
            Input input = new("dataset_664533_6.txt");
            Permutation perm = new(input.permutation);
            Console.WriteLine(perm.NumberOfBreakpoints());
        }
    }
}
