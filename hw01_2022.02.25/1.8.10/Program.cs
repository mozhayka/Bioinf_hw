using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _1._8._10
{
    class Program
    {
        static int HammingDistance(string s1, string s2)
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

        static int PatternMatching(string s, string sub, int d)
        {
            int ans = 0;
            for (int i = 0; i <= s.Length - sub.Length; i++)
            {
                if (HammingDistance(sub, s.Substring(i, sub.Length)) <= d)
                    ans++;
            }
            
            return ans;
        }

        static string Reverse(string s)
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

        static int Count(string text, string pat, int d)
        {
            return PatternMatching(text, pat, d) + PatternMatching(text, Reverse(pat), d);
        }

        static List<string> AllWords(int len)
        {
            if (len <= 0)
                return new List<string> { "" };
            List<string> ans = new List<string>();
            var words = AllWords(len - 1);
            foreach (var word in words)
            {
                ans.Add(word + 'A');
                ans.Add(word + 'T');
                ans.Add(word + 'C');
                ans.Add(word + 'G');
            }
            return ans;
        }

        static List<string> FrequentWords(string text, int k, int d)
        {
            int m = 0;
            List<string> ans = new List<string>();
            foreach (var word in AllWords(k))
            {
                int cnt = Count(text, word, d);
                if (cnt > m)
                {
                    m = cnt;
                    ans.Clear();
                    
                }
                if (cnt == m)
                    ans.Add(word);
            }
            return ans;
        }

        static void Main(string[] args)
        {
            using (var f = new StreamReader("../../../dataset_664439_10.txt"))
            {

                string text = f.ReadLine();
                var k = Int32.Parse(f.ReadLine());
                var d = Int32.Parse(f.ReadLine());

                FrequentWords(text, k, d).ForEach(Console.WriteLine);
            }
        }
    }
}
