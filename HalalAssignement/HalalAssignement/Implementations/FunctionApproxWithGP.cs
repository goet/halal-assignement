using HalalAssignement.Problems;
using HalalAssignement.Solvers;
using HalalAssignement.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalalAssignement.Implementations
{
    public class FunctionApproxWithGP
    {
        private GeneticProgramming gp;
        private FunctionApproximation functionA;

        public void Init(Random gen)
        {
            functionA = new FunctionApproximation();
            functionA.LoadKnownValuesFromFile(DataDir.FuncAppr);

            int headSize = 5;
            gp = new GeneticProgramming(gen, 5);
            gp.PopSize = 5000;
            gp.ElitismCount = 500;
            gp.MutationCount = 10;
            gp.Inputs = functionA.knownValues;
        }

        public double Solve(int maxCycles)
        {
            var result = gp.Solve(maxCycles);
            return result;
        }
    }
}
