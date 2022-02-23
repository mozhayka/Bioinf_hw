using System;
using System.Collections.Generic;
using System.IO;

namespace _1._8._6
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

        static int PatternMatching(string sub, string s, int d)
        {
            int ans = 0;
            for (int i = 0; i <= s.Length - sub.Length; i++)
            {
                if (HammingDistance(sub, s.Substring(i, sub.Length)) <= d)
                    ans++;
            }
            return ans;
        }

        static void Main(string[] args)
        {
            using (var f = new StreamReader("../../../dataset.txt"))
            {

                string sub = f.ReadLine();
                string s = f.ReadLine();
                var d = Int32.Parse(f.ReadLine());

                //string sub = "GAGG"; 
                //string s = "TTTAGAGCCTTCAGAGG";
                //var d = 2; 
                //StreamWriter w = new StreamWriter("ans.txt");
                Console.WriteLine(PatternMatching(sub, s, d));

            }

        }
    }
}
