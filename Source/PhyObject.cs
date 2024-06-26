using System;
using OpenTK;

namespace GravitySimulator
{
    public class PhyObject
    {
        public PhyObject(double mass, Vector2d initialPosition)
        {
            Mass = mass;
            PrevPosition = initialPosition;
            Position = initialPosition;
        }

        public Vector2d Position { get; set; }
        public Vector2d PrevPosition { get; set; } 
        public double Mass { get; }
    }
}
