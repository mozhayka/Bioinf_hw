﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _5._10._3
{
    class Input
    {
        public readonly int good, bad, _;
        public readonly string s1, s2;
        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var str = f.ReadLine().Split(' ');
                good = int.Parse(str[0]);
                bad = -int.Parse(str[1]);
                _ = -int.Parse(str[2]);

                s1 = f.ReadLine();
                s2 = f.ReadLine();
            }
        }

    }

    class Program
    {
        static Input data;
        static int[,] score;

        static void GAP(string s1, string s2)
        {
            score = new int[s1.Length + 1, s2.Length + 1];
            score[0, 0] = 0;
            for (int i = 1; i <= s1.Length; i++)
                score[i, 0] = i * data._;
            for (int i = 1; i <= s2.Length; i++)
                score[0, i] = i * data._;
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    score[i, j] = Math.Max(score[i - 1, j] + data._,
                        Math.Max(score[i, j - 1] + data._,
                        score[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? data.good : data.bad)));
                }
            }
        }

        static void Print()
        {
            int i = data.s1.Length, j = data.s2.Length;
            string s1 = data.s1, s2 = data.s2;
            Console.WriteLine(score[i, j]);
            StringBuilder ans1 = new(), ans2 = new();
            while (i + j > 0)
            {
                if (score[i, j] == score[i - 1, j] + data._)
                {
                    ans1.Append(s1[i - 1]);
                    ans2.Append('-');
                    i--;
                }
                else if (score[i, j] == score[i, j - 1] + data._)
                {
                    ans1.Append('-');
                    ans2.Append(s2[j - 1]);
                    j--;
                }
                else
                {
                    ans1.Append(s1[i - 1]);
                    ans2.Append(s2[j - 1]);
                    i--;
                    j--;
                }
            }
            var c1 = ans1.ToString().ToCharArray();
            Array.Reverse(c1);
            Console.WriteLine(c1);
            var c2 = ans2.ToString().ToCharArray();
            Array.Reverse(c2);
            Console.WriteLine(c2);
        }

        static void Do()
        {
            data = new Input("dataset_664518_3.txt");
            GAP(data.s1, data.s2);
            Print();
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
