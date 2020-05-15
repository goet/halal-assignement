using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HalalAssignement.Problems
{
    internal class SmallestBoundaryPolygon
    {
        protected List<Vector2> points = new List<Vector2>();

        protected double DistanceFromLine(Vector2 lp1, Vector2 lp2, Vector2 p)
        {
            return ((lp2.Y - lp1.Y) * p.X - (lp2.X - lp1.X) * p.Y + lp2.X * lp1.Y - lp2.Y * lp1.X) / Math.Sqrt(Math.Pow(lp2.Y - lp1.Y, 2) + Math.Pow(lp2.X - lp1.X, 2));
        }

        protected double OuterDistanceToBoundary(List<Vector2> solution)
        {
            var sumMinDist = 0d;

            foreach (var point in points)
            {
                double minDist = 0d;
                for (int i = 0; i < solution.Count; i++)
                {
                    double actualDist = DistanceFromLine(solution[i], solution[(i + 1) % solution.Count], point);
                    if (i == 0 || actualDist < minDist)
                        minDist = actualDist;
                }

                if (minDist < 0)
                    sumMinDist += -minDist;
            }

            return sumMinDist;
        }

        protected double BoundaryLength(List<Vector2> solution)
        {
            var sumLength = 0d;

            for (int i = 0; i < solution.Count - 1; i++)
            {
                Vector2 p1 = solution[i];
                Vector2 p2 = solution[(i + 1) % solution.Count];
                sumLength += Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y + p2.Y, 2));
            }

            return sumLength;
        }

        protected double Objective(List<Vector2> solution)
        {
            return BoundaryLength(solution);
        }

        protected double Constraint(List<Vector2> solution)
        {
            return -OuterDistanceToBoundary(solution);
        }

        public void LoadPointsFromFile(string path)
        {
            foreach (var line in System.IO.File.ReadAllLines(path))
            {
                var split = line.Split('\t');
                var parsed = (x: float.Parse(split[0]), y: float.Parse(split[1]));
                points.Add(new Vector2(parsed.x, parsed.y));
            }
        }

        public void SavePointsToFile(string path, List<Vector2> points)
        {
            var sb = new StringBuilder();
            foreach (var point in points)
            {
                sb.Append($"{point.X}\t{point.Y}\n");
            }
            System.IO.File.WriteAllText(path, sb.ToString());
        }
    }
}