using HalalAssignement.Problems;
using HalalAssignement.Solvers;
using System;

namespace HalalAssignement.Implementations
{
    public class FunctionApproxWithGP
    {
        private GeneticProgramming gp;
        private FunctionApproximation functionA;

        public double Result { get; set; }

        public void Init(string path, Random gen)
        {
            functionA = new FunctionApproximation();
            functionA.LoadKnownValuesFromFile(path);

            int headSize = 30;
            gp = new GeneticProgramming(gen, headSize, new string[] { "x" });
            gp.PopSize = 100000;
            gp.ElitismCount = 10;
            gp.MateCount = 10;
            gp.MutationCount = 1000;
            gp.Inputs = functionA.knownValues;
        }

        public double Solve(int maxCycles)
        {
            var result = gp.Solve(maxCycles);
            Result = result;
            return result;
        }
    }
}