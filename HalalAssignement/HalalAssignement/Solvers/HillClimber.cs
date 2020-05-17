using System;
using System.Numerics;

namespace HalalAssignement.Solvers
{
    internal class HillClimber<T>
    {
        public Random gen { get; set; }
        public double Epsilon { get; set; }
        public int MaxIterations { get; set; }
        public Func<T, double> Fitness { get; set; }
        public Func<T> RandomStart { get; set; }
        public Func<T, T> TakeValidStep { get; set; }

        private Func<T, T, bool> FitnessEval { get; set; }

        public T Solve()
        {
            var i = 0;
            var p = RandomStart();
            while (i < MaxIterations)
            {
                var q = TakeValidStep(p);
                if (FitnessEval(p, q))
                    p = q;

                Console.WriteLine($"{i}: current fitness: {Fitness(p)}");

                i++;
            }
            return p;
        }

        public void HigherFitnessIsBetter()
        {
            FitnessEval = (p, q) => Fitness(p) < Fitness(q);
        }

        public void LowerFitnessIsBetter()
        {
            FitnessEval = (p, q) => Fitness(p) > (Fitness(q));
        }
    }
}