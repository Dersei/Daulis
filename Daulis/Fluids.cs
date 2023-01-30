using System;
using System.Numerics;

namespace Daulis
{
    public static class Fluids
    {
        public const int Width = 80;
        public const int Height = 25;
        public const int ParticleBufferSize = Width * Height;
        public static readonly TimeSpan FrameSleep = TimeSpan.FromMilliseconds(100);
        public const string Lookup = @" '`-.|//,\|\_\/#";

        public const int OutputBufferSize = ParticleBufferSize + Height;

        public delegate void ParticleFunction(Complex delta, double distance, ref Particle from,
            Particle to);

        public const int Gravity = 1;
        public const int Pressure = 8;
        public const int Viscosity = 8;
    }
}