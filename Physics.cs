namespace GravitySimulator
{
    public class Physics
    {
        public const double G = 6.67430e-11;

        public static Vector2d[] CalcForces(PhyObject[] objects)
        {
            var forces = new Vector2d[objects.Length];

            for (int i = 0; i < objects.Length; i++)
                forces[i] = new Vector2d();

            for (int i = 0; i < objects.Length - 1; i++)
            {
                for (int j = i + 1; j < objects.Length; j++)
                {
                    Vector2d force = GetForce(objects[i], objects[j]);

                    forces[i].Add(force);
                    forces[j].Sub(force);
                }
            }

            return forces;
        }

        public static Vector2d GetForce(PhyObject a, PhyObject b)
        {
            double distance = Geometry.SquareOfDistance(a.Position, b.Position);
            double forceMag = G * a.Mass * b.Mass / distance;
            double forceAng = Geometry.Angle(a.Position, b.Position);

            return Vector2d.FromPolar(forceMag, forceAng);
        }
    }
}
