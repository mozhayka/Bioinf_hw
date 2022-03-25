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
            score[i, j] = score[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ?
                data.good : data.bad);
            prev[i, j] = 0;
            for (int k = 0; k < i; k++)
            {
                int s2_gaps_score = score[k, j] + data.gap + (i - k - 1) * data.double_gap;
                if (s2_gaps_score > score[i, j])
                {
                    score[i, j] = s2_gaps_score;
                    prev[i, j] = i - k;
                }
            }

            for (int k = 0; k < j; k++)
            {
                int s1_gaps_score = score[i, k] + data.gap + (j - k - 1) * data.double_gap;
                if (s1_gaps_score > score[i, j])
                {
                    score[i, j] = s1_gaps_score;
                    prev[i, j] = -(j - k);
                }
            }
        }

        static int GAP(string s1, string s2)
        {
            score = new int[s1.Length + 1, s2.Length + 1];
            prev = new int[s1.Length + 1, s2.Length + 1];
            score[0, 0] = 0;
            for (int i = 1; i <= s1.Length; i++)
            {
                prev[i, 0] = i;
                score[i, 0] = data.gap + (i - 1) * data.double_gap;
            }
            for (int i = 1; i <= s2.Length; i++)
            {
                prev[0, i] = -i;
                score[0, i] = data.gap + (i - 1) * data.double_gap;
            }
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
                if (prev[i, j] > 0)
                {
                    for (int k = 0; k < prev[i, j]; k++)
                    {
                        ans1.Append(s1[i - k - 1]);
                        ans2.Append('-');
                    }
                    i -= prev[i, j];
                }
                else if (prev[i, j] < 0)
                {
                    for (int k = 0; k < -prev[i, j]; k++)
                    {
                        ans1.Append('-');
                        ans2.Append(s2[j - k - 1]);
                    }
                    j += prev[i, j];
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
