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
                {
                    ans++;
                    return ans; //
                }
            }
            return ans;
        }
    }

    class Program
    {
        static bool IsGood(string [] text, string sub, int d)
        {
            foreach(string word in text)
            {
                if (DNA.PatternMatching(word, sub, d) == 0)
                    return false;
            }
            return true;
        }

        static List<string> MotifEnumeration(string[] text, int k, int d)
        {
            List<string> ans = new List<string>();
            foreach (var word in DNA.AllDNA(k))
            {
                if (IsGood(text, word, d))
                    ans.Add(word);
            }
            return ans;
        }

        static void Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {

                string[] kd = f.ReadLine().Split(' ');
                int k = Int32.Parse(kd[0]);
                int d = Int32.Parse(kd[1]);
                string[] text = f.ReadLine().Split(' ');
                MotifEnumeration(text, k, d).ForEach(Console.WriteLine);
            }
        }

        static void Main(string[] args)
        {
            Input("dataset_664450_8.txt");
        }
    }
}
