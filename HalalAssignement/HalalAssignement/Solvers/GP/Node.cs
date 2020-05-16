﻿namespace HalalAssignement.Solvers.GP
{
    public class Node
    {
        public string InputId { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public double ConstantValue { get; set; }
        public IOperator Operation { get; set; }

        public bool IsTerminal { get; set; }

        public double CalculateValue()
        {
            if (IsTerminal)
                return ConstantValue;

            return Operation.Apply(Left, Right);
        }
    }
}