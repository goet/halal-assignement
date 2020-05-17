using HalalAssignement.Problems;
using HalalAssignement.Solvers;
using HalalAssignement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HalalAssignement.Implementations
{
    public class TravellingSalesmanWithRandomOptimization
    {
        public double Result { get; set; }

        private TravellingSalesman problem;
        private RandomOptimization<List<Town>> solver;

        private Random gen;

        public void Init(Random gen)
        {
            this.gen = gen;

            problem = new TravellingSalesman();
            problem.LoadTownsFromFile(DataDir.Towns);

            solver = new RandomOptimization<List<Town>>(gen);
            solver.ProblemSpace = problem.Towns;
            solver.CycleLimit = 1000;
            solver.Dice = 3;
            solver.Max = 300;

            solver.GenerateRandomStart = (towns) =>
            {
                var route = new List<Town>();

                foreach (var town in solver.ProblemSpace)
                {
                    var index = gen.Next(0, route.Count);
                    route.Insert(index, town);
                }

                return route;
            };

            solver.TakeRandomStepWithinRange = (route, epsilon) =>
            {
                var alternativeRoute = new List<Town>(route);

                // take a random city, put it next to a close one
                var choice = alternativeRoute[gen.Next(0, alternativeRoute.Count)];
                alternativeRoute.Remove(choice);

                var inRange = route.Where(x => x.Distance(choice) <= epsilon).ToList();
                inRange.Remove(choice);
                var index = alternativeRoute.IndexOf(inRange.First());
                alternativeRoute.Insert(index, choice);

                return alternativeRoute;
            };

            double acceptableLength = 10;
            solver.StopCondition = (route) =>
            {
                return problem.Objective(route) < acceptableLength;
            };

            solver.Fitness = (route) =>
            {
                return problem.Objective(route);
            };
        }


        private void SwapRandom(List<Town> alternativeRoute)
        {
            int i0 = gen.Next(0, alternativeRoute.Count);
            int i1 = gen.Next(0, alternativeRoute.Count);
            while (i0 == i1)
                i1 = gen.Next(0, alternativeRoute.Count);

            var temp = alternativeRoute[i1];
            alternativeRoute[i1] = alternativeRoute[i0];
            alternativeRoute[i0] = temp;
        }

        public double Run()
        {
            var finalRoute = solver.Run();
            Result = problem.Objective(finalRoute);
            return Result;
        }
    }
}
