using System;
using OpenTK;

namespace GravitySimulator
{
    public static class Geometry
    {
        public static Vector2d FromPolar(double mag, double ang)
        {
            double x = mag * Math.Cos(ang);
            double y = mag * Math.Sin(ang);

            return new Vector2d(x, y);
        }

        public static double Angle(Vector2d a)
        {
            return Math.Atan2(a.Y, a.X);
        }

        public static double Angle(Vector2d a, Vector2d b)
        {
            double dX = b.X - a.X;
            double dY = b.Y - a.Y;

            return Math.Atan2(dY, dX);
        }

        public const double RadToDeg = 180.0 / Math.PI;
        public const double DegToRad = Math.PI / 180.0;
    }
}
