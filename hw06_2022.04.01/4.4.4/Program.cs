using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _4._4._4
{
    class Input
    {
        public readonly string dna;
        public readonly Dictionary<char, int> encode = new();

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                dna = f.ReadLine();
                ReadScore();
            }
        }

        private void ReadScore()
        {
            using (var f = new StreamReader("../../../integer_mass_table.txt"))
            {
                while(!f.EndOfStream)
                {
                    var str = f.ReadLine().Split(' ');
                    char key = str[0][0];
                    int val = int.Parse(str[1]);
                    encode[key] = val;
                }
            }
        }
    }
    class Program
    {
        static Input data;
        static List<int> ans = new();
        static List<int> pref, suf;

        static void Precalc()
        {
            pref = new() { 0 };
            suf = new() { 0 };
            for (int i = 0; i < data.dna.Length; i++)
            {
                pref.Add(pref[i] + data.encode[data.dna[i]]);
                suf.Add(suf[i] + data.encode[data.dna[^(i + 1)]]);
            }
        }

        static int Mass(int from, int len)
        {
            
            if (from + len <= data.dna.Length)
            {
                return pref[from + len] - pref[from];
            }
            else
            {
                return suf[data.dna.Length - from] + pref[from + len - data.dna.Length];
            }
        }

        static void AllSubsMass()
        {
            Precalc();
            ans.Add(0);
            ans.Add(pref[data.dna.Length]);
            for (int len = 1; len < data.dna.Length; len++)
            {
                for (int pos = 0; pos < data.dna.Length; pos++)
                {
                    ans.Add(Mass(pos, len));
                }
            }
        }

        static void Do()
        {
            data = new Input("dataset_664492_4.txt");
            AllSubsMass();
            ans.Sort();
            ans.ForEach(x => Console.Write($"{x} "));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
