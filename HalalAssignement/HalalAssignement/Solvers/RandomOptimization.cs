using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HalalAssignement.Solvers
{
    public class RandomOptimization<T>
    {
        public T ProblemSpace { get; set; }
        public int CycleLimit { get; set; } = 100;
        public int Dice { get; set; }
        public int Max { get; set; }

        // replacable functions
        public Func<T, T> GenerateRandomStart { get; set; }
        public Func<T, double, T> TakeRandomStepWithinRange { get; set; }
        public Predicate<T> StopCondition { get; set; }
        public Func<T, double> Fitness { get; set; }

        private Random gen;

        public T Run()
        {
            int c = 0;
            var p = GenerateRandomStart(ProblemSpace);
            while (!StopCondition(p))
            {
                var q = TakeRandomStepWithinRange(p, Distribution(Dice, Max));

                if (Fitness(q) < Fitness(p))
                    p = q;

                Console.WriteLine($"{c}: path length: {Fitness(p)}");

                if (c++ >= CycleLimit)
                    return p;
            }
            return p;
        }

        public double Distribution(double diceNum, double max)
        {
            var num = 0d;

            for (int i = 0; i < diceNum; i++)
            {
                num += gen.NextDouble() * (max / diceNum);
            }

            return num;
        }

        public RandomOptimization(Random gen)
        {
            this.gen = gen;
        }
    }
}
