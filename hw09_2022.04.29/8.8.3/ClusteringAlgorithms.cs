using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._8._3
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

        public static List<Point> LloydAlgorithm(List<Point> points, int k)
        {
            
            List<Point> centers = new(points.Take(k).ToList());
            var clusters = new List<Point>[k];
            double curSumDist = Point.SumDistToClosestCenters(points, centers);
            double prevSumDist = -1;

            while (curSumDist != prevSumDist)
            {
                var new_centers = points
                    .Select(x => new 
                    { 
                        ClusterNum = Point.ClosestCenter(x, centers),
                        point = x
                    })
                    .GroupBy(x => x.ClusterNum)
                    .Select(g => g.Select(p => p.point).ToList())
                    .Select(x => Point.Center(x))
                    .ToList();
                centers = new_centers;
                prevSumDist = curSumDist;
                curSumDist = Point.SumDistToClosestCenters(points, new_centers);
            }
            return centers;
        }
    }
}
