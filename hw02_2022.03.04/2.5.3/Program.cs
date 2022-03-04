using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _2._5._3
{
    class DNA
    {
        public static int HammingDistance(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                throw new Exception("length");
            int ans = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                    ans++;
            }
            return ans;
        }

        public static List<string> AllDNA(int len)
        {
            if (len <= 0)
                return new List<string> { "" };
            List<string> ans = new List<string>();
            var words = AllDNA(len - 1);
            foreach (var word in words)
            {
                ans.Add(word + 'A');
                ans.Add(word + 'T');
                ans.Add(word + 'C');
                ans.Add(word + 'G');
            }
            return ans;
        }

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

        public static IEnumerable<string> AllSubstrings(string text, int k)
        {
            for (int i = 0; i <= text.Length - k; i++)
            {
                yield return text.Substring(i, k);
            }
        }

        public static int PatternMatching(string s, string sub, int d)
        {
            int ans = 0;
            for (int i = 0; i <= s.Length - sub.Length; i++)
            {
                if (HammingDistance(sub, s.Substring(i, sub.Length)) <= d)
                    ans++;
            }
            return ans;
        }

        public static int Distance(string Pattern, string Dna)
        {
            int ans = Pattern.Length;
            for (int i = 0; i <= Dna.Length - Pattern.Length; i++)
            {
                ans = Math.Min(ans, HammingDistance(Pattern, Dna.Substring(i, Pattern.Length)));
            }
            return ans;
        }
    }

    class Input
    {
        public readonly Dictionary<char, List<double>> prob = new Dictionary<char, List<double>>();
        public readonly int k;
        public readonly string text;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                text = f.ReadLine();
                k = Int32.Parse(f.ReadLine());
                foreach (char c in new char[] { 'A', 'C', 'G', 'T' })
                    readProbs(f, c);
                
            }
        }

        private void readProbs(StreamReader f, char c)
        {
            var probs = f
                .ReadLine()
                .Split(' ')
                .Select(x => double.Parse(x, System.Globalization.CultureInfo.InvariantCulture))
                .ToList();
            prob.Add(c, probs);
        }
    }

    class Program
    {
        static Input data;

        static double CalcProbability(string sub)
        {
            double ans = 1;
            for (int i = 0; i < data.k; i++)
            {
                ans *= data.prob[sub[i]][i] * 10;
            }
            return ans;
        }

        static string MostProbable()
        {
            string ans = "";
            double maxProb = 0;
            foreach (var sub in DNA.AllSubstrings(data.text, data.k))
            {
                double prob = CalcProbability(sub);
                if(prob > maxProb)
                {
                    ans = sub;
                    maxProb = prob;
                }
            }
            return ans;
        }

        static void Do()
        {
            data = new Input("dataset_664453_3.txt");
            Console.WriteLine(MostProbable());
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
