using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8._6._2
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

        public static void Print(List<Point> points)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            points.ForEach(x => Console.WriteLine(x));
        }

        public override string ToString()
        {
            return string.Join(' ', 
                coordinates.Select(x =>
                string.Format("{0:f1}", x)));
        }
    }
}
