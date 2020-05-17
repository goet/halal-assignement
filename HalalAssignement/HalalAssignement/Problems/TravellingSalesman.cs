using HalalAssignement.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalalAssignement.Problems
{
    public class TravellingSalesman
    {
        public List<Town> Towns { get; set; }

        public double Objective(List<Town> route)
        {
            double sumLength = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                Town t1 = route[i];
                Town t2 = route[i + 1];
                sumLength += Math.Sqrt(Math.Pow(t1.X - t2.X, 2) + Math.Pow(t1.Y - t2.Y, 2));
            }
            return sumLength;
        }

        public void LoadTownsFromFile(string path)
        {
            Towns = new List<Town>();
            foreach (var line in System.IO.File.ReadAllLines(path))
            {
                var split = line.Split('\t');
                var parsed = (x: float.Parse(split[0]), y: float.Parse(split[1]));
                Towns.Add(new Town(parsed.x, parsed.y));
            }
        }

        public void SaveTownsToFile(string path, List<Town> towns)
        {
            var sb = new StringBuilder();
            foreach (var town in towns)
            {
                sb.Append($"{town.X}\t{town.Y}\n");
            }
            System.IO.File.WriteAllText(path, sb.ToString());
        }
    }
}
