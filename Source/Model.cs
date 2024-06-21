using System;
using System.Collections.Generic;
using OpenTK;

namespace GravitySimulator
{
    internal class Model
    {
        public Model(PhyObject[] objects, double deltaT, bool isDummyModel = true)
        {
            if (objects != null)
            {
                this.objects = objects;
                Count = objects.Length;
            }

            if (objects.Length > MaxObjects)
            {
                throw new ArgumentException("Too many objects");
            }

            DT = deltaT;

            _isDummyModel = isDummyModel;
            if (!isDummyModel)
            {
                throw new NotImplementedException("Ooops!");
            }
            else
            {
                var lst = new List<CircleTrack>(objects.Length);
                foreach (var obj in objects)
                    lst.Add(new CircleTrack(obj.Position.Length, Geometry.Angle(obj.Position)));

                circles = lst.ToArray();
            }

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
            if (_isDummyModel)
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].Position = circles[i].NextPosition(DT);

                    ObjectsPos[i].X = objects[i].Position.X;
                    ObjectsPos[i].Y = objects[i].Position.Y;
                }
            }
            else
            {
                // Step 1 - Calc forces
                // Step 2 - Calc velocity
                // Step 3 - Calc positions
            }
        }

        public Vector2d[] ObjectsPos { get; private set; }

        public double DT { get; set; }

        public int Count { get; private set; }

        private PhyObject[] objects;
        private CircleTrack[] circles;
        private const int MaxObjects = 8;


        private bool _isDummyModel;
    }
}
