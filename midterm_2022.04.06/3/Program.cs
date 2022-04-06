﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//https://github.com/mozhayka/Bioinf_hw/blob/master/hw06_2022.04.01/4.9.7/Program.cs
namespace _4._9._7
{
    class Input
    {
        public readonly List<int> spectrum;
        public readonly Dictionary<char, int> encode = new();
        public readonly int n, m, k;

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
                k = int.Parse(f.ReadLine());
                n = int.Parse(f.ReadLine());
                m = int.Parse(f.ReadLine());
                spectrum = f.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            }
        }

    }

    class Graph<T>
    {
        Dictionary<T, List<T>> graph;
        Dictionary<T, int> balance;
        Dictionary<T, HashSet<T>> used = new();
        List<T> path = new();

        public Graph(Dictionary<T, List<T>> graph, Dictionary<T, int> balance)
        {
            this.graph = graph;
            this.balance = balance;
            foreach (var key in balance.Keys)
                used[key] = new();
        }

        private T FindStart()
        {
            foreach (var key in balance.Keys)
            {
                if (balance[key] == 1)
                    return key;
            }
            return balance.Keys.GetEnumerator().Current;
        }

        public List<T> EulerianPath()
        {
            var from = FindStart();
            path.Add(from);

            while (true)
            {
                bool endWithBreak = false;
                foreach (var to in graph[from])
                {
                    if (!used[from].Contains(to))
                    {
                        used[from].Add(to);
                        path.Add(to);
                        from = to;
                        endWithBreak = true;
                        break;
                    }
                }
                if (!endWithBreak)
                    break;
            }
            while (UpgradePath())
            { }
            return path;
        }

        private bool UpgradePath()
        {
            //List<T> ans = curPath;
            for (int i = 0; i < path.Count; i++)
            {
                T cur = path[i];
                if (used[cur].Count == graph[cur].Count)
                    continue;

                List<T> newCycle = FindCycle(cur);
                path = path.Take(i + 1).Concat(newCycle).Concat(path.Skip(i + 1)).ToList();
                return true;
            }
            return false;
        }

        private List<T> FindCycle(T start)
        {
            var from = start;
            List<T> ans = new();

            while (true)
            {
                foreach (var to in graph[from])
                {
                    if (!used[from].Contains(to))
                    {
                        used[from].Add(to);
                        ans.Add(to);
                        from = to;
                        break;
                    }
                }
                if (from.Equals(start))
                    break;
            }
            return ans;
        }
    }

    class GraphDna
    {
        public static bool IsEdge(string from, string to)
        {
            from.SkipWhile(c => c != '-');
            from.Substring(1);

            return from == to.Substring(0, from.Length);
        }

        public static Dictionary<string, List<string>> GetGraph(List<string> v)
        {
            Dictionary<string, List<string>> graph = new();
            foreach (var from in v)
            {
                graph[from] = new();
                foreach (var to in v)
                {
                    if (IsEdge(from, to))
                        graph[from].Add(to);
                }
            }
            return graph;
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

            //List<int> pepSpect = CalcNoncyclospecturm(peptide);
            List<int> pepSpect = CalcCyclospecturm(peptide);
            pepSpect.Sort();
            return Score(pepSpect, spectrum);
        }

        static int LinearScore(string peptide, List<int> spectrum)
        {
            List<int> pepSpect = CalcNoncyclospecturm(peptide);
            //List<int> pepSpect = CalcCyclospecturm(peptide);
            pepSpect.Sort();
            return Score(pepSpect, spectrum);
        }

        static int Mass(string peptide)
        {
            return StringToPeptide(peptide).Sum();
        }

        static string LeaderboardCyclopeptideSequencing(List<int> spectrum, int n)
        {
            int maxLen = (int)Math.Ceiling(Math.Sqrt(spectrum.Count - 1));
            int spectrumMass = spectrum[^1];
            int ScoreLeaderPeptide = 0;
            HashSet<string> Leaderboard = new() { "" };
            string LeaderPeptide = "";
            int len = 0;
            while (Leaderboard.Count > 0)// && len <= maxLen)
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
                len++;
            }
            return LeaderPeptide;
        }

        static HashSet<string> Trim(HashSet<string> Leaderboard, List<int> spectrum, int n)
        {
            var board = Leaderboard
                .Select(g => new { Name = g, Score = LinearScore(g, spectrum) })
                .OrderByDescending(x => x.Score)
                .ToList();
            var minScore = board[n - 1].Score;
            return board
                .TakeWhile(g => g.Score >= minScore )
                .Select(g => g.Name)
                .ToList()
                .ToHashSet();
        }

        static HashSet<string> Expand(HashSet<string> candidate)
        {
            HashSet<string> ans = new();
            foreach (var str in candidate)
                ans.UnionWith(ExpandOne(str));
            return ans;
        }

        static List<int> alphabet;

        static HashSet<string> ExpandOne(string str)
        {
            HashSet<string> ans = new();
            foreach (var val in alphabet)
                ans.Add(str != "" ? str + $"-{val}" : val.ToString());
            return ans;
        }

        static List<int> SpectralConvolutionProblem(List<int> spectrum)
        {
            List<int> ans = new();
            for (int i = 0; i < spectrum.Count; i++)
            {
                for (int j = i + 1; j < spectrum.Count; j++)
                {
                    if (spectrum[j] - spectrum[i] != 0)
                        ans.Add(spectrum[j] - spectrum[i]);
                }
            }
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

        static void CreateAlphabet(List<int> spectrum, int m)
        {
            var conv = SpectralConvolutionProblem(spectrum);
            var GroupedLetters = conv
                .Where(x => x >= 57 && x <= 200)
                .GroupBy(x => x)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();
            var lastCount = GroupedLetters[m - 1].Count;
            alphabet = GroupedLetters
                .TakeWhile(x => x.Count >= lastCount)
                .Select(m => m.Name)
                .ToList();
        }

        static HashSet<string> GenerateAllkmers(int k)
        {
            HashSet<string> set = new() { "" };
            for (int i = 0; i < k; i++)
                set = Expand(set);
            return set;
        }

        static public Dictionary<string, List<string>> Graph = new();
        static public Dictionary<string, int> Balance = new();

        static private void CalcBalance()
        {
            foreach (var from in Graph.Keys)
            {
                ChangeBalance(from, Graph[from].Count);

                foreach (var to in Graph[from])
                    ChangeBalance(to, -1);
            }
        }

        static private void ChangeBalance(string v, int diff)
        {
            if (!Balance.ContainsKey(v))
                Balance[v] = diff;
            else
                Balance[v] += diff;
        }

        static void MidtermTask()
        {
            data = new Input("dataset.txt");
            CreateAlphabet(data.spectrum, data.m);
            var kmers = Trim(GenerateAllkmers(data.k), data.spectrum, data.n);
            Graph = GraphDna.GetGraph(kmers.ToList());
            
        }

        static void Do()
        {
            data = new Input("dataset_664497_7.txt");
            CreateAlphabet(data.spectrum, data.m);
            //Console.WriteLine(Score("99-71-137-57-72-57", data.spectrum));
            //Console.WriteLine(Score("71-99-57-72-57-66-71", data.spectrum));
            Console.WriteLine(LeaderboardCyclopeptideSequencing(data.spectrum, data.n));
        }

        static void Main(string[] args)
        {
            MidtermTask();
        }
    }
}