using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
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
            List<Point> centers = BetterInitializer(points, k);
            //List<Point> centers = new(points.Take(k).ToList());
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

        public static List<Point> BetterInitializer(List<Point> points, int k)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new(seed);

            List<Point> centers = new() { points[rand.Next(k)] };
            for (int i = 1; i < k; i++)
            {
                centers.Add(RandomPoint(points, centers));
            }
            return centers;
        }

        private static Point RandomPoint(List<Point> points, List<Point> centers)
        {
            int n = points.Count;
            var probFrom = new double[n];
            var probTo = new double[n];
            double sumDist = 0;
            for (int i = 0; i < n; i++)
            {
                var point = points[i];
                double dist = Point.SquareDist(point, centers);
                probFrom[i] = sumDist;
                probTo[i] = sumDist + dist;
                sumDist += dist;
            }
            Random rand = new();
            double prob = rand.NextDouble() * sumDist;
            for (int i = 0; i < n; i++)
            {
                if (probFrom[i] <= prob && prob < probTo[i])
                {
                    return points[i];
                }
            }
            throw new Exception("Random didn't work");
        }
    }
}
