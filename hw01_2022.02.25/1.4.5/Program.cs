﻿using System;
using System.Collections.Generic;

namespace _1._4._5
{
    class Program
    {
        static HashSet<string> FrequencyTable(string s, int k, int t)
        {
            Dictionary<string, int> cnt = new Dictionary<string, int>();
            HashSet<string> ans = new HashSet<string>();
            for (int i = 0; i < s.Length - k; i++)
            {
                var sub = s.Substring(i, k);
                if (!cnt.ContainsKey(sub))
                    cnt.Add(sub, 0);
                cnt[sub]++;
            }

            foreach (var key in cnt.Keys)
            {
                if (cnt[key] >= t)
                    ans.Add(key);
            }
            return ans;
        }


        static HashSet<string> FindClumps(string s, int k, int l, int t)
        {
            HashSet<string> ans = new HashSet<string>();
            for (int i = 0; i < s.Length - l; i++)
            {
                var sub = s.Substring(i, l);
                ans.UnionWith(FrequencyTable(sub, k, t));
            }
            
            return ans;
        }

        static void Main(string[] args)
        {
            foreach (var word in FindClumps(
                "CGTTTTCGCCTATGGACAAGGATTTCCAGCCCAGTCTAAGTCTACATACGTAGCTATTGTCAAATTTCTATCAAATTCTGTTCAGAAATTAGGCTAAGTATTCATGCGGAGGTCCCAAGCGTGAAGAAGTTCATGTTGTATTTCTGTTCACCCATTTCACATGTAAATTTAGTCCGCGACTGTGGGGAACCCAGAGTTCTCGCAGCAATAGCGCTATTTGAAGCAGCGCGGCTTTAGATCACTTACCTACTGGGCAAAGTTTAGGACGGCGTTTTTTTAGTACTTTAGTACACTACTTGAAGACCACCCATGGGATCAACGGGCCAACGGGCCCCAAAACGGGCCCCTGTCTATGCTAGCTACGGATGGGATGGGATGTGGTGGGGATGTGGAATATATTGCCCCAGTTTCTACCAGTTTCAGTTCCAGTTTCTAGGGATGGCGGGACCTTCCCATACATCGTATCCTCTTTGGATTCTTTGGATTTGGATCGCGCGCTCAACCGCTCGATGTCGGCTTTGCTGAGTAAGTGCGACGATGCGCATCGCCGCACGCATCGCATCGCATCGCGTTCACACTTACGAGATGCTTTCCACACCGGAGATCACCGGAGGACTTCGGTTAAGTCCTGGATCGTTGTCGAGGACAGAACAATTAAAGGTCAAGAAAGCGGACATAGAAGTCTTTTTGCCGGGCCTTTACGTGCATCGCGTTCGCCTCGACGTAAAATGAAAGTTGGCGTAATCCATCCGCATCCGCGCCCGCGCCGCTTAATGTCATCCCTGCCTACACGTCTACACTACATCCGGCGACGGCGACTCTCCGGCGAGAATCCAAATTATTCTACAGGATGGTGCAGCACTCGCAAATCCCGTAACCCACCTATTAAACCCCGCGAACGTGCGTGGGGTAAAGCAAGGAAGATCGAATGTGGCGTCACATAAAAGGTGCTATAGTTAATTGTACGAGGGACTAAATGATAAGCGATCGAACGTCCCGTACAAGCCTAGTAAACTTGAGTGGATCGCCCGTAAGCGTTGGCAAGGGTAGGCGACGTTGGTTTTTAGACACCATTTACCCCCTCAAAGCGCAAATTAAGCCGATAATATAAACACTTTACATTTTAATAGTATTTAGTACGAAGGCATTACTAGTCGAATAGTAATCTAAAAATGTCAGCTCGTGGATTGCAATGGCCCCCGTAGTGAGTATTGAGGTCCATGCTTGGGACTACTATAAGCACGGTACCCGCCTAGTACCCGGTACCCGCCAGGGCTGCATATTAAGGAGTAGGAGTATAAGGAGTTCTCAAGCTTCGGTCCATCACCCCAAAGGGGAGATAGTGGAAGCTACGACACGTGCCCGTGCTTCCTAACTCAGGCTACGTCGGTCATTTATCGACAGCTACAGACTACTATCGACTATCCCGGATCCCGGATCAATCCCGGAGAATGATCTTAAAAAGCCGGGCTGTCTGAAGATCGCATCGTCCCTCTTATAAAATGGATATGTCGGGATTGATCGGGATTGGGATTTTTCTACTATTCTTCTTCGAGTAAGCTGCAACTTAACGAATACCCCTACTCACCTGCGGCTTCCCAGGGCTCCTTACATTGAGGTAATCAGGTAATCAGGTAATCAGGTAATCAGGTAATCAGGTAATCAGGTAATCAGGTAATCGTTCTCATGTTCTCATGTTCTCATGTTCTCATGTTCTCATGTTCTCATGTTCTCATGTTCTCAT",
                8, 25, 4))
                Console.WriteLine(word);
        }
    }
}
