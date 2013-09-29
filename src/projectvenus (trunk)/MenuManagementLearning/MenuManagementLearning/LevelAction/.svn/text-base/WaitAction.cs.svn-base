using System;
using Microsoft.Xna.Framework;

namespace ProjektVenus
{
    public class WaitAction : LevelActionBase
    {
        private TimeSpan duration;
        private TimeSpan elapsed;

        public WaitAction(Level parent, TimeSpan duration)
            : base(parent)
        {
            this.duration = duration;
            this.elapsed = TimeSpan.Zero;
        }

        private void Done(Object sender, EventArgs o)
        {
            //this.finished = true;
            Console.WriteLine("Timer is done " + duration);
        }

        public override void Update(GameTime gameTime)
        {
            this.elapsed += gameTime.ElapsedGameTime;
            if (this.elapsed >= this.duration)
            {
                Console.WriteLine("Timer is done " + duration);
                this.finished = true;

            }
        }
    }
}
