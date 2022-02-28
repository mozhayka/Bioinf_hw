using System;

namespace _2._4._9
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    namespace _2._2._8
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

        class Program
        {
            static int SumDistance(string[] text, string sub)
            {
                int ans = 0;
                foreach (string word in text)
                {
                    ans += DNA.Distance(sub, word);
                }
                return ans;
            }

            static string MedianString(string[] text, int k)
            {
                int min_dist = k * text.Length + 1;
                string ans = "";
                foreach (var word in DNA.AllDNA(k))
                {
                    int dist = SumDistance(text, word);
                    if (SumDistance(text, word) < min_dist)
                    {
                        ans = word;
                        min_dist = dist;
                    }
                }
                return ans;
            }

            static void Input(string filename = "dataset.txt")
            {
                filename = "../../../" + filename;
                using (var f = new StreamReader(filename))
                {
                    int k = Int32.Parse(f.ReadLine());
                    string[] text = f.ReadLine().Split(' ');
                    Console.WriteLine(MedianString(text, k));
                }
            }

            static void Main(string[] args)
            {
                Input("dataset_664452_9.txt");
            }
        }
    }

}
