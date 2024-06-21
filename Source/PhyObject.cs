using System;
using OpenTK;

namespace GravitySimulator
{
    public class PhyObject
    {
        public PhyObject(double mass, Vector2d initialPosition, Vector2d initialVeocity)
        {
            Mass = mass;
            Position = initialPosition;
            Velocity = initialVeocity;
        }

        public Vector2d Velocity { get; set; }
        public Vector2d Position { get; set; }
        public double Mass { get; }
    }
}
