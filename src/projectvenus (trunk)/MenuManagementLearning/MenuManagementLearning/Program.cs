using System;
using Microsoft.Xna.Framework.Media;

namespace ProjektVenus
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (ProjektVenus game = new ProjektVenus())
            {
                game.Run();
            }
        }
    }
#endif
}

