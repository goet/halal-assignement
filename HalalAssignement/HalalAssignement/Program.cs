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

            var boundary = new BoundaryWithHillClimb();
            Console.WriteLine("initialising problem: smallest boundary polygon\n\twith solution:stochastic hill climbing");
            boundary.Init(DataDir.Points, gen);
            boundary.Run();
            Console.WriteLine($"smallest boundary polygon result: {boundary.Result}\n\toriginal:{boundary.OriginalLength}");


            //Console.WriteLine("======");
            //var functionApprox = new FunctionApproxWithGP();
            //Console.WriteLine("initialising problem: function approximation\n\twith solution:genetic programming");
            //functionApprox.Init(DataDir.FuncAppr, gen);
            //Console.WriteLine("running gp");
            //functionApprox.Solve(100);
            //Console.WriteLine($"smallest error in final generation: {functionApprox.Result}");


            //Console.WriteLine("======");
            //var travellingSalesman = new TravellingSalesmanWithRandomOptimization();

            //Console.WriteLine("initialising problem: travelling salesman\n\twith solution:random optimization");
            //travellingSalesman.Init(gen);

            //Console.WriteLine("running solution");
            //travellingSalesman.Run();

            //Console.WriteLine("======");
            //Console.WriteLine($"length of the shortest path found: {travellingSalesman.Result}");
        }
    }
}
