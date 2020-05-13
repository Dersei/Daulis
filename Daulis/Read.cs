using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Daulis
{
    public static class Read
    {
        public static int ReadParticles(Particle[] ps, string fileName)
        {
            Complex pos = 0;
            for (int j = 0; j < ps.Length; j++)
            {
                ps[j] = new Particle();
            }

            int i = 0;
            using var streamReader = new StreamReader(fileName, Encoding.ASCII);
            for (char chr = Convert.ToChar(streamReader.Read());
                !streamReader.EndOfStream;
                chr = Convert.ToChar(streamReader.Read()))
            {
                if (chr == '\n')
                {
                    // Since input particles are of height 2, newline needs
                    // to increment y by 2.
                    pos += 2;
                    pos = pos.Real;

                    continue;
                }

                if (chr > ' ')
                {
                    // We've detected an actual particle. We double up the
                    // height to 2.
                    ps[i].Position = pos;
                    ps[i + 1].Position = pos + 1;

                    // The # character signifies a 'wall' (solid barrier.)
                    ps[i].IsWall = ps[i + 1].IsWall = (chr == '#');

                    i += 2;
                }

                pos -= Complex.ImaginaryOne;
            }

            return i;
        }
    }
}