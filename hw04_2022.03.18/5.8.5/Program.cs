using System;
using System.IO;

namespace _5._8._5
{
    class Input
    {
        public string s1, s2;
        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                s1 = f.ReadLine();
                s2 = f.ReadLine();
            }
        }

    }

    class Program
    {
        static Input data;
        static int[,] backtrack;

        static void LCSBackTrack(string s, string t)
        {
            backtrack = new int[s.Length + 1, t.Length + 1];
            for (int i = 0; i <= s.Length; i++)
                backtrack[i, 0] = 0;
            for (int i = 0; i <= t.Length; i++)
                backtrack[0, i] = 0;
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < t.Length; j++)
                {
                    int match = s[i] == t[j] ? 1 : 0;
                    backtrack[i + 1, j + 1] = Math.Max(backtrack[i, j + 1],
                        Math.Max(backtrack[i + 1, j], backtrack[i, j] + match));
                }
            }
        }

        static string OutputLCS(int i, int j)
        {
            if (i == 0 || j == 0)
                return "";
            if (backtrack[i, j] == backtrack[i - 1, j])
                return OutputLCS(i - 1, j);
            if (backtrack[i, j] == backtrack[i, j - 1])
                return OutputLCS(i, j - 1);
            return OutputLCS(i - 1, j - 1) + data.s1[i - 1];
        }

        static void Do()
        {
            data = new Input("dataset_664516_5.txt");
            LCSBackTrack(data.s1, data.s2);
            Console.WriteLine(OutputLCS(data.s1.Length, data.s2.Length));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
