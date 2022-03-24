using System;
using System.IO;
using System.Text;

namespace _5._12._8
{
    class Input
    {
        public readonly int good, bad, gap, double_gap;
        public readonly string s1, s2;
        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                var str = f.ReadLine().Split(' ');
                good = int.Parse(str[0]);
                bad = -int.Parse(str[1]);
                gap = -int.Parse(str[2]);
                double_gap = -int.Parse(str[3]);

                s1 = f.ReadLine();
                s2 = f.ReadLine();
            }
        }
    }

    class Program
    {
        static Input data;
        static int[,] score;
        static int[,] prev;

        static void CalcScore(int i, int j, string s1, string s2)
        {
            int s2_gap_score = score[i - 1, j] +
                (i > 1 && score[i - 2, j] + data.gap == score[i - 1, j] ? 
                data.double_gap : data.gap);
            int s1_gap_score = score[i, j - 1] +
                (j > 1 && score[i, j - 2] + data.gap == score[i, j - 1] ?
                data.double_gap : data.gap);
            int no_gap_score = score[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? 
                data.good : data.bad);

            //Console.WriteLine($"{s2_gap_score} {s1_gap_score} {no_gap_score}");
            if (s1_gap_score >= s2_gap_score && s1_gap_score >= no_gap_score)
            {
                score[i, j] = s1_gap_score;
                prev[i, j] = 1;
                //Console.WriteLine($"{score[i, j]} {data.s1[..i]} {data.s2[..j]}");
                return;
            }

            if (s2_gap_score >= s1_gap_score && s2_gap_score >= no_gap_score)
            {
                score[i, j] = s2_gap_score;
                prev[i, j] = 2;
                //Console.WriteLine($"{score[i, j]} {data.s1[..i]} {data.s2[..j]}"); 
                return;
            }
            score[i, j] = no_gap_score;
            prev[i, j] = 3;
            //Console.WriteLine($"{score[i, j]} {data.s1[..i]} {data.s2[..j]}");
        }

        static int GAP(string s1, string s2)
        {
            score = new int[s1.Length + 1, s2.Length + 1];
            prev = new int[s1.Length + 1, s2.Length + 1];
            score[0, 0] = 0;
            for (int i = 1; i <= s1.Length; i++)
                score[i, 0] = data.gap + (i - 1) * data.double_gap;
            for (int i = 1; i <= s2.Length; i++)
                score[0, i] = data.gap + (i - 1) * data.double_gap;
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    CalcScore(i, j, s1, s2);
                }
            }
            return score[s1.Length, s2.Length];
        }

        static void Print()
        {
            string s1 = data.s1, s2 = data.s2;
            int i = s1.Length, j = s2.Length;
            Console.WriteLine(score[i, j]);
            StringBuilder ans1 = new(), ans2 = new();
            while (i + j > 0)
            {
                if (prev[i, j] == 2)
                {
                    ans1.Append(s1[i - 1]);
                    ans2.Append('-');
                    i--;
                }
                else if (prev[i, j] == 1)
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
            data = new Input("dataset_664520_8.txt");
            GAP(data.s1, data.s2);
            Print();
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
