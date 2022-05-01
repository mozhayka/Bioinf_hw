using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._14._7
{
    class DistanceMatrix
    {
        private readonly double[,] D;
        private readonly int n;
        Tree tree;

        public DistanceMatrix(double[,] matrix, int n)
        {
            D = matrix;
            this.n = n;
            tree = new(n);
        }

        public double LimbLength(int j)
        {
            double dist = 1e9;
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

        private static double LimbLength(double[,] D, int j, out int x, out int y)
        {
            double dist = 1e9;
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

        double[] age;
        int[] cnt;
        public void UPGMA()
        {
            double[,] newD = new double[2 * n - 1, 2 * n - 1];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    newD[i, j] = D[i, j];

            Tree t = new(2 * n - 1);
            HashSet<int> clusters = new();

            age = new double[2 * n - 1];
            cnt = new int[2 * n - 1];
            var cluster = new List<int>[2 * n - 1];

            for (int i = 0; i < n; i++)
            {
                age[i] = 0;
                cnt[i] = 1;
                cluster[i] = new List<int> { i + 1 };
                clusters.Add(i);
            }

            for (int i = 0; i < n - 1; i++)
            {
                int c_new = n + i;
                FindClosest(newD, clusters, out int c_i, out int c_j);
                cnt[c_new] = cnt[c_i] + cnt[c_j];
                age[c_new] = newD[c_i, c_j] / 2;

                cluster[c_new] = cluster[c_i].Union(cluster[c_j]).ToList();
                string result = string.Join(" ", cluster[c_new]);
                Console.WriteLine(result);

                clusters.Remove(c_i);
                clusters.Remove(c_j);
                CalcDistances(ref newD, clusters, c_new, c_i, c_j);

                t.AddEdge(c_new, c_i, age[c_new] - age[c_i]);
                t.AddEdge(c_new, c_j, age[c_new] - age[c_j]);
                clusters.Add(c_new);
            }
            // t.Print();
        }

        private void CalcDistances(ref double[,] D, HashSet<int> clusters, int c_new, int c_i, int c_j)
        {
            foreach (var c in clusters)
            {
                D[c_new, c] = (D[c_i, c] * cnt[c_i] + D[c_j, c] * cnt[c_j]) / (cnt[c_j] + cnt[c_i]);
                D[c, c_new] = D[c_new, c];
            }
        }

        private void FindClosest(double[,] D, HashSet<int> clusters, out int ci, out int cj)
        {
            ci = -1;
            cj = -1;
            double min_dist = 1e9;
            foreach (var c1 in clusters)
            {
                foreach (var c2 in clusters)
                {
                    if (c1 == c2)
                        continue;
                    if (min_dist > D[c1, c2])
                    {
                        min_dist = D[c1, c2];
                        ci = c1;
                        cj = c2;
                    }
                }
            }

        }
    }
}
