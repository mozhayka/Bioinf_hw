using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _5._10._10
{
    class Input
    {
        public readonly int _ = -5;
        public readonly string s1, s2;
        public readonly Dictionary<string, Dictionary<string, int>> score = new();

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {

                s1 = f.ReadLine();
                s2 = f.ReadLine();
            }
            ReadScore();
        }

        private void ReadScore()
        {
            using (var f = new StreamReader("../../../score.txt"))
            {
                string[] ch1 = f.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ch1.Length; i++)
                {
                    var str = f.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    score[str[0]] = new();
                    
                    for (int j = 1; j < str.Length; j++)
                    {
                        score[str[0]][ch1[j - 1]] = int.Parse(str[j]);
                    }
                }
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
                score[i, 0] = 0;
            for (int i = 1; i <= s2.Length; i++)
                score[0, i] = 0;
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    score[i, j] = Math.Max(Math.Max(0, score[i - 1, j] + data._),
                        Math.Max(score[i, j - 1] + data._,
                        score[i - 1, j - 1] + data.score[s1[i - 1].ToString()][s2[j - 1].ToString()]));
                }
            }
        }

        static int[] FindMax()
        {
            int[] ans = new int[2];
            int max = 0;
            for (int i = 1; i <= data.s1.Length; i++)
                for (int j = 1; j <= data.s2.Length; j++)
                    if (score[i, j] > max)
                    {
                        max = score[i, j];
                        ans[0] = i;
                        ans[1] = j;
                    }
            return ans;
        }

        static void Print()
        {
            int[] ij = FindMax();
            int i = ij[0], j = ij[1];
            string s1 = data.s1, s2 = data.s2;
            Console.WriteLine(score[i, j]);
            StringBuilder ans1 = new(), ans2 = new();
            while (i + j > 0)
            {
                if (score[i, j] == 0)
                    break;
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
            data = new Input("dataset_664518_10.txt");
            GAP(data.s1, data.s2);
            Print();
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
