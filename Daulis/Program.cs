using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Daulis
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsoleSetup.Start();
            
            char[] buf = new char[Fluids.OutputBufferSize];

            Particle[] particles = new Particle[Fluids.ParticleBufferSize];
            int len = Read.ReadParticles(particles, "Input/tanada.txt");
            do
            {
                Calc.UpdateParticleDynamics(particles, len);
                Write.WriteToBuffer(buf, particles, len);
                Write.OutputBuffer(buf);
                Thread.Sleep(Fluids.FrameSleep);
            } while (Calc.UpdatePosition(particles, len));
        }
    }
}