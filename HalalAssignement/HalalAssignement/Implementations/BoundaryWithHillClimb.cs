using HalalAssignement.Problems;
using HalalAssignement.Solvers;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HalalAssignement.Implementations
{
    public class BoundaryWithHillClimb
    {
        public Vector2 LowestPoint { get; set; } = new Vector2(-1000, -1000);
        public Vector2 HighestPoint { get; set; } = new Vector2(1000, 1000);

        public double Result { get; set; }

        private SmallestBoundaryPolygon smallestBoundary;
        private HillClimber<List<Vector2>> hillClimb;

        public void Init(string path, Random gen)
        {
            smallestBoundary = new SmallestBoundaryPolygon();
            smallestBoundary.LoadPointsFromFile(path);

            hillClimb = new HillClimber<List<Vector2>>();

            hillClimb.gen = gen;
            hillClimb.Epsilon = Math.PI;
            hillClimb.MaxIterations = 50;

            hillClimb.Fitness = (solution) => smallestBoundary.Objective(solution);
            hillClimb.LowerFitnessIsBetter();

            hillClimb.RandomStart = () =>
            {
                var points = new List<Vector2>();
                double rangeX = HighestPoint.X - LowestPoint.X;
                double rangeY = HighestPoint.Y - LowestPoint.Y;
                foreach (var point in smallestBoundary.points)
                {
                    var x = (gen.NextDouble() * rangeX) + LowestPoint.X;
                    var y = (gen.NextDouble() * rangeY) + LowestPoint.Y;
                    points.Add(new Vector2((float)x, (float)y));
                }
                return points;
            };

            hillClimb.TakeValidStep = (state) =>
            {
                var newState = new List<Vector2>();
                foreach (var point in state)
                {
                    var x = gen.Next(-1, 1);
                    var y = gen.Next(-1, 1);
                    var v = new Vector2(x, y);

                    v = Vector2.Normalize(v);
                    v *= (float)hillClimb.Epsilon;
                    newState.Add(v);
                }
                return newState;
            };
        }

        public double Run()
        {
            var solution = hillClimb.Solve();
            Result = smallestBoundary.Objective(solution);
            return Result;
        }
    }
}
