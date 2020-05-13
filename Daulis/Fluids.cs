using System;
using System.Numerics;

namespace Daulis
{
    public static class Fluids
    {
        public static int Width = 80;
        public static int Height = 25;
        public static int ParticleBufferSize = Width * Height;
        public static int FrameSleepNano = 1000;
        public static string Lookup = " '`-.|//,\\|\\_\\/#";

        public static int OutputBufferSize = ParticleBufferSize + Height;

        public delegate void ParticleFunction(Complex delta, double distance, ref Particle from,
            Particle to);

        public static int Gravity = 1;
        public static int Pressure = 8;
        public static int Viscosity = 8;
    }
}