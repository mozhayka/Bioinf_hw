using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._5._6
{
    public class Permutation
    {
        readonly List<string> permutation;

        public Permutation(List<string> permutation)
        {
            this.permutation = permutation;
        }

        //6.5.6
        public int NumberOfBreakpoints()
        {
            int ans = 0;
            if (ElToInt(permutation[0]) != 1)
                ans++;
            for (int i = 1; i < permutation.Count; i++)
            {
                if (ElToInt(permutation[i]) - ElToInt(permutation[i - 1]) != 1)
                    ans++;
            }
            if (ElToInt(permutation[^1]) != permutation.Count)
                ans++;

            return ans;
        }

        //6.4.4
        public List<string> GreedySort()
        {
            List<string> ans = new();
            string next = String.Join(' ', permutation);

            while (next != null)
            {
                next = GreedySwap();
                ans.Add(next);
            }
            return ans;
        }

        private string GreedySwap()
        {
            int i = 0;
            while (i < permutation.Count && permutation[i] == $"+{i + 1}")
                i++;
            if (i == permutation.Count)
                return null;

            if (permutation[i] == $"+{i + 1}")
            {
                permutation[i] = $"-{i + 1}";
            }
            else
            {
                Reverse(i);
            }
            return String.Join(' ', permutation);
        }

        private void Reverse(int from)
        {
            int to = from;

            while (from + 1 != int.Parse(permutation[to][1..]))
                to++;

            Reverse(from, to);
        }

        private void Reverse(int from, int to)
        {
            for (int i = 0; i <= (to - from) / 2; i++)
            {
                var fst = permutation[from + i];
                var snd = permutation[to - i];

                permutation[from + i] = ChangeSign(snd);
                permutation[to - i] = ChangeSign(fst);
            }
        }

        private string ChangeSign(string el)
        {
            return (el[0] == '+' ? '-' : '+') + el[1..];
        }

        static public int ElToInt(string el)
        {
            return int.Parse(el);
        }

        static public string IntToEl(int i)
        {
            return (i > 0) ? $"+{i}" : $"-{i}";

        }
    }
}
