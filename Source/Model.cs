using System;
using OpenTK;

namespace GravitySimulator
{
    internal class Model
    {
        public Model(PhyObject[] objects, double deltaT, int cycles)
        {
            this.objects = objects;
            Count = objects.Length;

            if (objects.Length > MaxObjects)
            {
                throw new ArgumentException("Too many objects");
            }

            if (cycles < 1)
            {
                throw new ArgumentException("cycles < 1");
            }

            numCycles = cycles;

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

        public void Advance()
        {
            // Some time cycles
            for (int c = 0; c < numCycles; c++)
            {
                Vector2d[] forces = Physics.CalcForces(objects);

                for (int i = 0; i < Count; i++)
                {
                    PhyObject obj = objects[i];

                    Vector2d newPosition = 2.0 * obj.Position - obj.PrevPosition + forces[i] * DT * DT;

                    obj.PrevPosition = obj.Position;
                    obj.Position = newPosition;
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


        private int numCycles;
        private PhyObject[] objects;
        private const int MaxObjects = 8;
    }
}
