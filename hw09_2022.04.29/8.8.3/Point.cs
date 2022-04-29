using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._8._3
{
    class Point
    {
        List<double> coordinates;

        public Point(List<string> coord)
        {

            coordinates = coord.Select(x =>
            double.Parse(x, CultureInfo.InvariantCulture))
                .ToList();
        }

        public Point(List<double> coord)
        {

            coordinates = coord;
        }

        public static double Dist(Point a, Point b)
        {
            var ans = Math.Sqrt(a.coordinates
                .Zip(b.coordinates, (x, y) => (x - y) * (x - y))
                .Sum());
            return ans;
        }

        public static double Dist(Point a, List<Point> b)
        {
            var ans = b.Select(x => Dist(a, x)).Min();
            return ans;
        }

        public static Point Center(List<Point> points)
        {
            var ans = points
                .Select(x => x.coordinates)
                .Aggregate((c1, c2) => 
                    c1.Zip(c2, (x, y) => x + y)
                    .ToList())
                .Select(x => x / points.Count)
                .ToList();
            
            return new Point(ans);
        }

        public static int ClosestCenter(Point p, List<Point> centers)
        {
            int ans = -1;
            double minDist = 1e9;
            for (int i = 0; i < centers.Count; i++)
            {
                var dist = Dist(p, centers[i]);
                if (dist < minDist)
                {
                    ans = i;
                    minDist = dist;
                }
            }
            return ans;
        }

        public static double SumDistToClosestCenters(List<Point> points, List<Point> centers)
        {
            var ans = points
                .Select(x => Dist(x, centers[ClosestCenter(x, centers)]))
                .Sum();
            return ans;
        }

        public static void Print(List<Point> points)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            points.ForEach(x => Console.WriteLine(x));
        }

        public override string ToString()
        {
            return string.Join(' ',
                coordinates.Select(x =>
                string.Format("{0:f3}", x)));
        }
    }
}
