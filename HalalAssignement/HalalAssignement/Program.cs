using HalalAssignement.Implementations;
using System;
using System.IO;

namespace HalalAssignement
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new Random();

            var pointsPath = Directory.GetCurrentDirectory() + @"/TestData/Points.txt";

            var boundary = new BoundaryWithHillClimb();
            boundary.Init(pointsPath, gen);
            boundary.Run();
            Console.WriteLine($"smallest boundary polygon result: {boundary.Result}");
        }
    }
}
