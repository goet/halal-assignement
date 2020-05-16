using HalalAssignement.Implementations;
using HalalAssignement.Utils;
using System;
using System.IO;

namespace HalalAssignement
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new Random();

            //var boundary = new BoundaryWithHillClimb();
            //boundary.Init(DataDir.Points, gen);
            //boundary.Run();
            //Console.WriteLine($"smallest boundary polygon result: {boundary.Result}");

            Console.WriteLine("======");
            var functionApprox = new FunctionApproxWithGP();
            Console.WriteLine("initialising gp");
            functionApprox.Init(DataDir.FuncAppr, gen);
            Console.WriteLine("running gp");
            functionApprox.Solve(1000);
            Console.WriteLine($"smallest error in final generation: {functionApprox.Result}");
        }
    }
}
