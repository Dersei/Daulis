using System.Numerics;

namespace Daulis
{
    public class Particle
    {
        public Complex Position;
        public bool IsWall;
        public double Density;
        public Complex Force;
        public Complex Velocity;
    }
}