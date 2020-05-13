using System;
using System.Linq;

namespace Daulis
{
    public class Write
    {
        public static void BufferToAscii(char[] buf)
        {
            for (int i = 0; i < Fluids.OutputBufferSize - 1; i++)
            {
                int j = i;

                if ((i + 1) % Fluids.Width == 0)
                {
                    buf[j] = '\n';
                    continue;
                }

                int offset = buf[j];
                buf[j] = Fluids.Lookup[offset];
            }
        }

        public static void WriteToBuffer(char[] buf, Particle[] particles, int len)
        {
            Array.Clear(buf, 0, Fluids.OutputBufferSize);

            for (int i = 0; i < len; i++)
            {
                Particle particle = particles[i];

                int x = (int) -particle.Position.Imaginary;
                int y = (int) (particle.Position.Real / 2); // Particle height is 2.

                // Particles are of height 2, need to be able to write to bottom
                // right.
                if (x < 0 || x >= Fluids.Width - 1 || y < 0 || y >= Fluids.Height - 2)
                    continue;

                int curr = x + Fluids.Width * y;
                int right = curr + 1,
                    below = curr + Fluids.Width,
                    belowRight = below + 1;


                buf[curr] |= (char) 8;
                buf[right] |= (char) 4;
                buf[below] |= (char) 2;
                buf[belowRight] = (char) 1;
            }

            BufferToAscii(buf);
        }

        public static void OutputBuffer(char[] buf)
        {
            Console.SetCursorPosition(0,0);
            Console.Write(buf);
        }
    }
}