using System;

namespace GravitySimulator
{
    public class CircleTrack
    {
        public CircleTrack(double radius, double phase = 0f)
        {
            time_ = phase;
            radius_ = radius;
        }

        public Vector2d NextPosition(double dt)
        {
            time_ += dt;

            double x = radius_ * Math.Cos(time_);
            double y = radius_ * Math.Sin(time_);

            return new Vector2d(x, y);
        }

        private double radius_;
        private double time_;
    }
}
