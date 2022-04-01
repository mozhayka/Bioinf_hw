using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _4._6._6
{
    class Input
    {
        public readonly List<int> spectrum;
        public readonly Dictionary<char, int> encode = new();

        public Input(string filename = "dataset.txt")
        {
            filename = "../../../" + filename;
            using (var f = new StreamReader(filename))
            {
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

        static int Contains(List<int> pepSpect, List<int> spectrum)
        {
            if (spectrum.Count < pepSpect.Count)
                return -1;            
            int j = 0;
            for (int i = 0; i < pepSpect.Count; i++, j++)
            {
                while (j < spectrum.Count && pepSpect[i] > spectrum[j])
                    j++;
                if (j >= spectrum.Count || pepSpect[i] != spectrum[j])
                    return -1;
            }            
            if (spectrum.Count == pepSpect.Count)
                return 0;
            else
                return 1;
        }

        static int Compare(string peptide, List<int> spectrum)
        {
            List<int> pepSpect = CalcNoncyclospecturm(peptide);            
            pepSpect.Sort();            
            int contains = Contains(pepSpect, spectrum);
            if (contains == -1)
                return -1;
            if (pepSpect[^1] == spectrum[^1])
            {
                List<int> fullSpect = CalcCyclospecturm(peptide);
                fullSpect.Sort();
                return Contains(fullSpect, spectrum);
            }
            return 1;
        }

        static List<string> CyclopeptideSequencing(List<int> spectrum)
        {
            HashSet<string> Candidate = new() { "" };
            List<string> FinalPeptides = new();
            while (Candidate.Count > 0)
            {
                Candidate = Expand(Candidate);
                foreach(var peptide in Candidate)
                {
                    var cmp = Compare(peptide, spectrum);
                    if (cmp == 0)
                    {
                        FinalPeptides.Add(peptide);
                        Candidate.Remove(peptide);
                    }
                    if (cmp == -1)
                        Candidate.Remove(peptide);
                }
            }
            return FinalPeptides;
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
            data = new Input("dataset_664494_6.txt");
            var ans = CyclopeptideSequencing(data.spectrum);
            ans.ForEach(x => Console.Write($"{x} "));
        }

        static void Main(string[] args)
        {
            Do();
        }
    }
}
