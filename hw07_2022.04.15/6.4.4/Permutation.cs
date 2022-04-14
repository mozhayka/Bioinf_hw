using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._4._4
{
    public class Permutation
    {
        readonly List<string> permutation;

        public Permutation(List<string> permutation)
        {
            this.permutation = permutation;
        }

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
            
            while(from + 1 != int.Parse(permutation[to][1..]))
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
    }
}
