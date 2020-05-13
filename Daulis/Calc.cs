using System.Numerics;

namespace Daulis
{
    public static class Calc
    {
        private static Complex Kernel(double distance)
        {
            return (distance / 2 - 1) * (distance / 2 - 1);
        }

        private static void PairwiseSpline(Particle[] particles, int len,
            Fluids.ParticleFunction updateNearby)
        {
            for (int i = 0; i < len; i++)
            {
                Particle from = particles[i];

                for (int j = 0; j < len; j++)
                {
                    Particle to = particles[j];

                    Complex delta = from.Position - to.Position;
                    double distance = Complex.Abs(delta);

                    if (distance > 2)
                        continue;

                    updateNearby(delta, distance, ref from, to);
                }
            }
        }

        private static void UpdateDensity(Complex delta, double distance, ref Particle from,
            Particle to)
        {
            from.Density += Kernel(distance).Magnitude;
        }

        private static void UpdateForce(Complex delta, double distance, ref Particle from,
            Particle to)
        {
            Complex force = (from.Density + to.Density - 3) * delta * Fluids.Pressure -
                            (from.Velocity - to.Velocity) * Fluids.Viscosity;
            
            from.Force += (force / from.Density) * Kernel(distance);
        }

        private static void InitParticles(Particle[] particles, int len)
        {
            for(int i = 0; i < len; i++) {
                Particle particle = particles[i];

                particle.Force = Fluids.Gravity;
                particle.Density = particle.IsWall ? 9 : 0;
            }
        }
        
        // Use the magic of Smoothed-particle hydrodynamics.
        public static void UpdateParticleDynamics(Particle[] particles, int len)
        {
            InitParticles(particles, len);

            PairwiseSpline(particles, len, UpdateDensity);
            PairwiseSpline(particles, len, UpdateForce);
        }

        public static bool UpdatePosition(Particle[] particles, int len)
        {
            var isAnythingMoving = false;
            for (int i = 0; i < len; i++) {
                Particle particle = particles[i];

                if (particle.IsWall)
                    continue;

                particle.Velocity += particle.Force / 10;
                particle.Position += particle.Velocity;
                isAnythingMoving = true;
            }

            return isAnythingMoving;
        }
    }
}