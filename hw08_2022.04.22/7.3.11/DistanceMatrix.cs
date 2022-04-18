using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._3._11
{
    class DistanceMatrix
    {
        private readonly int[,] D;
        private readonly int n;

        public DistanceMatrix(int[,] matrix, int n)
        {
            D = matrix;
            this.n = n;
        }

        public int LimbLength(int j)
        {
            int dist = (int) 1e9;
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
    }
}
