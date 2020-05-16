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
    }

    public class Sub : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() - op2.CalculateValue();
        }
    }

    public class Mul : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() * op2.CalculateValue();
        }
    }

    public class Div : IOperator
    {
        public double Apply(Node op1, Node op2)
        {
            return op1.CalculateValue() / op2.CalculateValue();
        }
    }
}