using System;

namespace HalalAssignement.Solvers.GP
{
    public class NodeGenerator
    {
        private Random gen;

        public double MaxConstantValue { get; set; } = 10;
        public double MinConstantValue { get; set; } = 2;
        public string[] PossibleInputVars { get; set; }

        public NodeGenerator(Random gen, string[] possibleInputvars)
        {
            this.gen = gen;
            PossibleInputVars = possibleInputvars;
        }

        public Node GenerateTerminal()
        {
            var roll = gen.NextDouble();
            var constantRange = MaxConstantValue - MinConstantValue;

            var n = new Node();
            n.IsTerminal = true;

            if (roll <= .5)
            {
                n.ConstantValue = gen.NextDouble() * constantRange + MinConstantValue;
            }
            else
            {
                // input
                n.InputId = PossibleInputVars[gen.Next(0, PossibleInputVars.Length - 1)];
            }

            return n;
        }

        public Node GenerateFunction()
        {
            var roll = gen.NextDouble();

            var n = new Node();

            if (roll <= .2)
                n.Operation = new Sum();
            else if (roll <= .4)
                n.Operation = new Sub();
            else if (roll <= .6)
                n.Operation = new Mul();
            else if (roll <= .8)
                n.Operation = new Div();
            else
                n.Operation = new Pow();

            return n;
        }

        public Node Generate()
        {
            var roll = gen.NextDouble();

            if (roll < .5)
                return GenerateFunction();
            else
                return GenerateTerminal();
        }
    }
}