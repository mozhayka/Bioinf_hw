using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._4._6
{
    class DistanceMatrix
    {
        private readonly int[,] D;
        private readonly int n;
        Tree tree;

        public DistanceMatrix(int[,] matrix, int n)
        {
            D = matrix;
            this.n = n;
            tree = new(n);
        }

        public int LimbLength(int j)
        {
            int dist = (int)1e9;
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (j == i || j == k)
                        continue;
                    dist = Math.Min(dist, (D[i, j] + D[j, k] - D[i, k]) / 2);
                }
            }
            return dist;
        }

        //7.4.6
        public void AdditivePhylogeny()
        {
            AdditivePhylogeny(D);
            tree.Print();
        }

        private static int LimbLength(int[,] D, int j, out int x, out int y)
        {
            int dist = (int)1e9;
            x = -1;
            y = -1;
            int n = D.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (j == i || j == k)
                        continue;
                    if (dist > (D[i, j] + D[j, k] - D[i, k]) / 2)
                    {
                        dist = (D[i, j] + D[j, k] - D[i, k]) / 2;
                        x = k;
                        y = i;
                    }
                }
            }
            return dist;
        }

        private void AdditivePhylogeny(int[,] D)
        {
            int n = D.GetLength(0);
            if (n == 2)
            {
                tree.AddEdge(0, 1, D[0, 1]);
                return;
            }

            int len = LimbLength(D, n - 1, out int from, out int to);
            int distFrom = D[from, n - 1] - len;

            var newD = new int[n - 1, n - 1];
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - 1; j++)
                    newD[i, j] = D[i, j];
            AdditivePhylogeny(newD);
            var v = tree.SplitEdge(from, to, distFrom);

            //Console.WriteLine($"Add edge {v} {n - 1} {len}");
            tree.AddEdge(v, n - 1, len);
        }
    }
}
