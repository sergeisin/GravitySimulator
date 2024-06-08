using System;
using System.Collections.Generic;

namespace GravitySimulator
{
    internal class Model
    {
        public Model(PhyObject[] objects, double deltaT, bool isDummyModel = true)
        {
            if (objects != null)
            {
                Objects = objects;
            }

            if (objects.Length > MaxObjects)
            {
                throw new ArgumentException("Too many objects");
            }

            DT = deltaT;

            if (!isDummyModel)
            {
                throw new NotImplementedException("Ooops!");
            }
            else
            {
                Random rand = new Random();
                var lst = new List<CircleTrack>(objects.Length);
                foreach (var obj in objects)
                    lst.Add(new CircleTrack(obj.Position.Mag, obj.Position.AngRad));

                circles = lst.ToArray();
            }
        }

        public void Advance()
        {
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].Position = circles[i].NextPosition(DT);
            }
        }

        public Vector2d[] GetPositions()
        {
            throw new NotImplementedException();
        }

        public double DT { get; set; }

        public PhyObject[] Objects { get; private set; }

        private CircleTrack[] circles;
        private const int MaxObjects = 8;
    }
}
