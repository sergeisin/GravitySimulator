using System;

namespace GravitySimulator
{
    public static class Geometry
    {
        public static double Distance(Vector2d a, Vector2d b)
        {
            double dX = b.X - a.X;
            double dY = b.Y - a.Y;

            return Math.Sqrt(dX * dX + dY * dY);
        }

        public static double SquareOfDistance(Vector2d a, Vector2d b)
        {
            double dX = b.X - a.X;
            double dY = b.Y - a.Y;

            return dX * dX + dY * dY;
        }

        public static double Angle(Vector2d a, Vector2d b)
        {
            double dX = b.X - a.X;
            double dY = b.Y - a.Y;

            return Math.Atan2(dY, dX);
        }

        public static double AngDeg(Vector2d a, Vector2d b)
        {
            return Angle(a, b) * RadToDeg;
        }

        public const double RadToDeg = 180.0 / Math.PI;
        public const double DegToRad = Math.PI / 180.0;
    }
}
