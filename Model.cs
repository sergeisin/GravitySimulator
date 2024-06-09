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
                this.objects = objects;
                Count = objects.Length;
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
                var lst = new List<CircleTrack>(objects.Length);
                foreach (var obj in objects)
                    lst.Add(new CircleTrack(obj.Position.Mag, obj.Position.AngRad));

                circles = lst.ToArray();
            }

            RenderObjects = new Vector2d[Count];
            for (int i = 0; i < Count; i++)
            {
                RenderObjects[i] = new Vector2d();
            }
        }

        public void Advance()
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].Position = circles[i].NextPosition(DT);

                // Invert Y
                RenderObjects[i].X =  objects[i].Position.X;
                RenderObjects[i].Y = -objects[i].Position.Y;
            }
        }

        /// <summary>
        /// In scene 
        /// </summary>
        public Vector2d[] RenderObjects { get; private set; }

        public double DT { get; set; }

        public int Count { get; private set; }

        private PhyObject[] objects;
        private CircleTrack[] circles;
        private const int MaxObjects = 8;
    }
}
