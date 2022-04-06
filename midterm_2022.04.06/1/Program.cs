using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

// https://github.com/mozhayka/Bioinf_hw/blob/master/hw02_2022.03.04/2.6.9/Program.cs
namespace _2._6._9
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
        public readonly int k, t;
        public readonly string[] text;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                string[] kt = f.ReadLine().Split(' ');
                k = Int32.Parse(kt[0]);
                t = Int32.Parse(kt[1]);
                text = f.ReadLine().Split(' ');
            }
        }

    }

    class Program
    {
        static Input data;

        static BigInteger CalcProbability(List<string> Profile, string sub)
        {
            BigInteger ans = 1;
            int k = sub.Length;
            for (int i = 0; i < k; i++)
            {
                uint prob_i = 1;
                for (int j = 0; j < Profile.Count; j++)
                {
                    if (sub[i] == Profile[j][i])
                        prob_i++;
                }
                ans *= prob_i;
                if (ans < 0)
                {
                    Console.WriteLine(ans);
                }
            }

            return ans;
        }

        static string MostProbable(List<string> Profile, string word)
        {
            string ans = "";
            BigInteger maxProb = 0;
            foreach (var sub in DNA.AllSubstrings(word, data.k))
            {
                BigInteger prob = CalcProbability(Profile, sub);
                if (prob > maxProb)
                {
                    //Console.WriteLine(prob);
                    ans = sub;
                    maxProb = prob;
                }
            }
            return ans;
        }

        static int MaxLetterCnt(Dictionary<char, int> freq)
        {
            return Math.Max(Math.Max(freq['A'], freq['T']), Math.Max(freq['C'], freq['G']));
        }

        static int Score(List<string> Motifs)
        {
            int score = 0;
            int k = data.k;
            Dictionary<char, int> freq = new Dictionary<char, int>();
            for (int i = 0; i < k; i++)
            {
                freq['A'] = 0;
                freq['C'] = 0;
                freq['G'] = 0;
                freq['T'] = 0;
                for (int j = 0; j < Motifs.Count; j++)
                {
                    char c = Motifs[j][i];
                    freq[c]++;
                }
                score += k - MaxLetterCnt(freq);
            }
            return score;
        }

        static List<string> StartMotifs(string[] Dna, int k)
        {
            List<string> ans = new List<string>();
            foreach (var word in Dna)
            {
                ans.Add(word.Substring(0, k));
            }
            return ans;
        }

        static List<string> GreedyMotifSearch()
        {
            string[] Dna = data.text;
            int k = data.k, t = data.t;
            List<string> BestMotifs = StartMotifs(Dna, k);
            foreach (var sub in DNA.AllSubstrings(Dna[0], k))
            {
                List<string> Motifs = new() { sub };
                for (int i = 1; i < t; i++)
                {
                    Motifs.Add(MostProbable(Motifs, Dna[i]));
                }
                if (Score(Motifs) < Score(BestMotifs))
                    BestMotifs = Motifs;
            }
            return BestMotifs;
        }

        static void Do()
        {
            data = new Input("dataset_664454_94.txt");
            GreedyMotifSearch().ForEach(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}