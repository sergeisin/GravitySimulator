using System;
using System.Collections.Generic;
using OpenTK;

namespace GravitySimulator
{
    internal class Model
    {
        public Model(PhyObject[] objects, double deltaT)
        {
            this.objects = objects;
            Count = objects.Length;

            if (objects.Length > MaxObjects)
            {
                throw new ArgumentException("Too many objects");
            }

            DT = deltaT;

            ObjectsPos = new Vector2d[Count];
            for (int i = 0; i < Count; i++)
            {
                ObjectsPos[i] = new Vector2d()
                {
                    X = objects[i].Position.X,
                    Y = objects[i].Position.Y,
                };
            }
        }

        public void Advance(int cycles)
        {
            // Some time cycles
            for (int c = 0; c < cycles; c++)
            {
                Vector2d[] forces = Physics.CalcForces(objects);

                for (int i = 0; i < Count; i++)
                {
                    PhyObject obj = objects[i];
                    Vector2d force = forces[i];

                    obj.Velocity += force * DT;
                    obj.Position += obj.Velocity * DT;
                }
            }

            // View positions update
            for (int i = 0; i < Count; i++)
            {
                ObjectsPos[i] = new Vector2d()
                {
                    X = objects[i].Position.X,
                    Y = objects[i].Position.Y
                };
            }
        }

        public Vector2d[] ObjectsPos { get; private set; }

        public double DT { get; set; }

        public int Count { get; private set; }

        private PhyObject[] objects;
        private const int MaxObjects = 8;
    }
}
