using System;

namespace HalalAssignement.Solvers.GP
{
    public class NodeGenerator
    {
        private Random gen;

        public double MaxConstantValue { get; set; } = 100;
        public double MinConstantValue { get; set; } = 2;
        public string[] PossibleInputVars { get; set; }

        public NodeGenerator(Random gen)
        {
            this.gen = gen;
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

            if (roll <= .25)
                n.Operation = new Sum();
            else if (roll <= .5)
                n.Operation = new Sub();
            else if (roll <= .75)
                n.Operation = new Mul();
            else
                n.Operation = new Div();

            return n;
        }
    }
}