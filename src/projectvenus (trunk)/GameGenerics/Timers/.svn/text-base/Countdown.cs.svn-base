using System;
using Microsoft.Xna.Framework;
using GameGenerics.Timers;

namespace GameGenerics.Timers
{
    public class Countdown : TimerBase
    {
        #region Fields
        TimeSpan duration;
        #endregion

#region Properties
        public TimeSpan Duration 
        {
            get { return this.Duration; }
            set { this.duration = value; }
        }

        public TimeSpan Left
        {
            get { return this.Duration - this.Elapsed; }
        }
#endregion

#region Construstors
        public Countdown() 
            : base()
        {
            this.duration = TimeSpan.Zero;
        }

        public Countdown(TimeSpan duration)
            : base()
        {
            this.duration = duration;
        }

        public Countdown(TimeSpan duration, EventHandler end)
            : base()
        {
            this.duration = duration;
            this.End += end;
        }

        public Countdown(TimeSpan duration, EventHandler end, bool start)
            : base(start)
        {
            this.duration = duration;
            this.End += end;
        }
#endregion

#region Event
        public event EventHandler End;
#endregion

#region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.Elapsed >= this.Duration)
            {
                this.OnEnded();
                this.Reset();
            }
        }

        protected void OnEnded() 
        {
            if (this.End != null)
                this.End(this, EventArgs.Empty);
        }
#endregion
    }
}
