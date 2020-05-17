using System;

namespace HalalAssignement.Utils
{
    public class Town
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Town(float x, float y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Town other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
}