using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._6._2
{
    class ClusteringAlgorithms
    {
        public static List<Point> FarthestFirstTraversal(List<Point> points, int k)
        {
            List<Point> ans = new() { points[0] };
            bool[] used = new bool[points.Count];
            used[0] = true;
            for (int i = 1; i < k; i++)
            {
                double maxDist = 0;
                int pointToAdd = 0;
                for (int j = 0; j < points.Count; j++)
                {
                    var dist = Point.Dist(points[j], ans);
                    if (!used[i] && dist > maxDist)
                    {
                        pointToAdd = j;
                        maxDist = dist;
                    }
                }
                used[pointToAdd] = true;
                ans.Add(points[pointToAdd]);
            }
            return ans;
        }
    }
}
