using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _4._9._4
{
    class Input
    {
        public readonly List<int> spectrum;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                spectrum = f.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            }
        }

    }

    class Program
    {
        static Input data;

        static List<int> SpectralConvolutionProblem(List<int> spectrum)
        {
            List<int> ans = new();
            for (int i = 0; i < spectrum.Count; i++)
            {
                for (int j = i + 1; j < spectrum.Count; j++)
                {
                    if (spectrum[j] - spectrum[i] != 0)
                        ans.Add(spectrum[j] - spectrum[i]);
                }
            }
            return ans;
        }

        static void Do()
        {
            data = new Input("dataset_664497_4.txt");
            var ans = SpectralConvolutionProblem(data.spectrum);
            ans.Sort();
            ans.ForEach(x => Console.Write($"{x} "));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
