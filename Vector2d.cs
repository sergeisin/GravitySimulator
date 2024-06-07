using System;

namespace GravitySimulator
{
    // Vector, Complex, 2D-Position, PointD
    // It seems they are the same thing
    public class Vector2d
    {
        public Vector2d(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public static Vector2d FromPolar(double mag, double angle)
        {
            double x = mag * Math.Cos(angle);
            double y = mag * Math.Sin(angle);

            return new Vector2d(x, y);
        }

        public void Add(Vector2d other)
        {
            X += other.X;
            Y += other.Y;
        }

        public void Sub(Vector2d other)
        {
            X -= other.X;
            Y -= other.Y;
        }

        public double Mag
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        public double AngRad
        {
            get { return Math.Atan2(Y, X); }
        }

        public double AngDeg
        {
            get { return AngRad * Geometry.RadToDeg; }
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
