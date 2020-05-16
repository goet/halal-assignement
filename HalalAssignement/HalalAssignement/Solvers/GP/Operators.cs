using System;

namespace HalalAssignement.Solvers.GP
{
    public interface IOperator
    {
        double Apply(Node op1, Node op2);
    }

    public class Sum : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() + op2.CalculateValue();
        }

        public override string ToString()
        {
            return "+";
        }
    }

    public class Sub : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() - op2.CalculateValue();
        }

        public override string ToString()
        {
            return "-";
        }
    }

    public class Mul : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() * op2.CalculateValue();
        }

        public override string ToString()
        {
            return "*";
        }
    }

    public class Div : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            var divisor = op2.CalculateValue();
            if (divisor == 0)
                divisor = 1;
            return op1.CalculateValue() / op2.CalculateValue();
        }

        public override string ToString()
        {
            return "/";
        }
    }

    public class Pow : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return Math.Pow(op1.CalculateValue(), op2.CalculateValue());
        }

        public override string ToString()
        {
            return "^";
        }
    }
}