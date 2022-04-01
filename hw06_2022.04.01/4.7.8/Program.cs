using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._7._8
{
    class Input
    {
        public readonly List<int> spectrum;
        public readonly Dictionary<char, int> encode = new();
        public readonly int n;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                n = int.Parse(f.ReadLine());
                spectrum = f.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
                ReadScore();
            }
        }

        private void ReadScore()
        {
            using (var f = new StreamReader("../../../integer_mass_table.txt"))
            {
                while (!f.EndOfStream)
                {
                    var str = f.ReadLine().Split(' ');
                    char key = str[0][0];
                    int val = int.Parse(str[1]);
                    encode[key] = val;
                }
            }
        }
    }
    class Program
    {
        static Input data;
        static List<List<int>> Candiddate = new();
        static Dictionary<int, List<int>> Spectres = new();

        static List<int> CalcPrefs(List<int> peptide)
        {
            List<int> pref = new() { 0 };
            for (int i = 0; i < peptide.Count; i++)
            {
                pref.Add(pref[i] + peptide[i]);
            }
            return pref;
        }

        static int Mass(List<int> pref, int from, int len)
        {
            return (from + len < pref.Count)
                ? pref[from + len] - pref[from]
                : pref[^1] - pref[from] + pref[from + len - pref.Count + 1];
        }

        static List<int> CalcCyclospecturm(List<int> peptide)
        {
            List<int> pref = CalcPrefs(peptide);
            List<int> cyclospec = new() { 0 };
            cyclospec.Add(pref[^1]);
            for (int pos = 0; pos < peptide.Count; pos++)
                for (int len = 1; len < peptide.Count; len++)
                    cyclospec.Add(Mass(pref, pos, len));
            return cyclospec;
        }

        static List<int> CalcNoncyclospecturm(List<int> peptide)
        {
            List<int> pref = CalcPrefs(peptide);
            List<int> cyclospec = new() { 0 };
            cyclospec.Add(pref[^1]);
            for (int pos = 0; pos < peptide.Count; pos++)
                for (int len = 1; len < peptide.Count - pos; len++)
                    cyclospec.Add(Mass(pref, pos, len));
            return cyclospec;
        }

        static List<int> CalcCyclospecturm(string peptide)
        {
            return CalcCyclospecturm(StringToPeptide(peptide));
        }

        static List<int> CalcNoncyclospecturm(string peptide)
        {
            return CalcNoncyclospecturm(StringToPeptide(peptide));
        }

        static int Score(List<int> pepSpect, List<int> spectrum)
        {
            int i = 0, j = 0, score = 0;
            while (i < pepSpect.Count && j < spectrum.Count)
            { 
                if (pepSpect[i] == spectrum[j])
                {
                    score++;
                    i++;
                    j++;
                }
                else if (pepSpect[i] < spectrum[j])
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }
            return score;
        }

        static int Score(string peptide, List<int> spectrum)
        {
            List<int> pepSpect = CalcNoncyclospecturm(peptide);
            pepSpect.Sort();
            return Score(pepSpect, spectrum);
        }

        static int Mass(string peptide)
        {
            return StringToPeptide(peptide).Sum();
        }

        static string LeaderboardCyclopeptideSequencing(List<int> spectrum, int n)
        {
            int spectrumMass = spectrum[^1];
            int ScoreLeaderPeptide = 0;
            HashSet<string> Leaderboard = new() { "" };
            string LeaderPeptide = "";
            while (Leaderboard.Count > 0)
            {
                Leaderboard = Expand(Leaderboard);
                foreach (var peptide in Leaderboard)
                {
                    int peptideMass = Mass(peptide);
                    if (peptideMass == spectrumMass)
                    {
                        int ScorePeptide = Score(peptide, spectrum);
                        if (ScorePeptide > ScoreLeaderPeptide)
                        {
                            LeaderPeptide = peptide;
                            ScoreLeaderPeptide = ScorePeptide;
                        }
                    }
                    else if (peptideMass > spectrumMass)
                        Leaderboard.Remove(peptide);
                }
                Leaderboard = Trim(Leaderboard, spectrum, n);
            }
            return LeaderPeptide;
        }

        static HashSet<string> Trim(HashSet<string> Leaderboard, List<int> spectrum, int n)
        {
            return Leaderboard.OrderByDescending(x => Score(x, spectrum)).Take(n).ToList().ToHashSet();
        }

        static HashSet<string> Expand(HashSet<string> candidate)
        {
            HashSet<string> ans = new();
            foreach (var str in candidate)
                ans.UnionWith(ExpandOne(str));
            return ans;
        }

        static HashSet<string> ExpandOne(string str)
        {
            HashSet<string> ans = new();
            foreach (var val in data.encode)
                ans.Add(str != "" ? str + $"-{val.Value}" : val.Value.ToString());
            return ans;
        }

        static List<int> StringToPeptide(string str)
        {
            return str.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        }

        static string PeptideToString(List<int> peptide)
        {
            var str = new StringBuilder("");
            foreach (int i in peptide)
                str.Append($"i-");
            return str.ToString().TrimEnd('-');
        }

        static void Do()
        {
            data = new Input("dataset_664495_8.txt");
            Console.WriteLine(LeaderboardCyclopeptideSequencing(data.spectrum, data.n));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
