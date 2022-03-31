using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._2._7
{
    class DNA
    {
        public static string Reverse(string s)
        {
            StringBuilder ans = new StringBuilder(s);
            for (int i = 0; i < s.Length; i++)
            {
                ans[s.Length - i - 1] = s[i] switch
                {
                    'A' => 'T',
                    'T' => 'A',
                    'C' => 'G',
                    'G' => 'C',
                    _ => '?'
                };
            }
            return ans.ToString();
        }
    }

    class RNA
    {
        private readonly Dictionary<string, string> encode;
        private int len = 3;

        public RNA(Dictionary<string, string> encode)
        {
            this.encode = encode;
        }

        public string Encode(string str)
        {
            if (str.Length % len != 0)
                throw new Exception("bad len");
            StringBuilder ans = new("");
            for (int i = 0; i < str.Length; i += len)
            {
                ans.Append(encode[str.Substring(i, len)]);
            }
            return ans.ToString();
        }

        public bool IsEncodesTo(string str, string pep)
        {
            return pep == Encode(str) || pep == Encode(DNA.Reverse(str));
        }
    }

    class Input
    {
        public readonly string dna, peptide;
        public readonly Dictionary<string, string> encode = new();

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                dna = f.ReadLine();
                peptide = f.ReadLine();
                ReadScore();
            }
        }

        private void ReadScore()
        {
            using (var f = new StreamReader("../../../RNA_codon_table_1.txt"))
            {
                for (int i = 0; i < 64; i++)
                {
                    var str = f.ReadLine().Split(' ');
                    string key = new string(str[0].Select(x => x == 'U' ? 'T' : x).ToArray());
                    var val = str.Length > 0 ? str[1] : "*";
                    encode[key] = val;
                }
            }
        }
    }
    class Program
    {
        static Input data;
        static List<string> ans = new();
        static RNA rna;
        //static int[,] score;

        static void check(string sub)
        {
            if (rna.IsEncodesTo(sub, data.peptide))
            {
                ans.Add(sub);
            }
        }

        static void ScanDNA(string dna, int len)
        {
            for (int i = 0; i < dna.Length - len; i++)
            {
                check(dna.Substring(i, len));
            }
        }
        static void Do()
        {
            data = new Input("dataset_664490_7.txt");
            rna = new(data.encode);
            ScanDNA(data.dna, 3 * data.peptide.Length);
            ans.ForEach(x => Console.WriteLine(x));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
