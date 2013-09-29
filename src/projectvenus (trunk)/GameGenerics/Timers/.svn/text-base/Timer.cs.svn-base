using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameGenerics.Timers
{
    public class Timer : TimerBase
    {
#region Fields
        TimeSpan interval;
        TimeSpan passed;
        bool shutdown;
#endregion

#region Properties
        public TimeSpan Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }

        public TimeSpan Passed
        {
            get { return this.passed; }
        }
#endregion
        
#region Constructors
        public Timer()
            : base()
        {
            this.interval = TimeSpan.Zero;
        }

        public Timer(TimeSpan interval)
            : base()
        {
            this.interval = interval;
        }

        public Timer(TimeSpan interval, EventHandler tick)
            : base()
        {
            this.interval = TimeSpan.Zero;
            this.tick += tick;    
        }

        public Timer(TimeSpan interval,EventHandler tick, bool start)
            : base(start)
        {
            this.interval = interval;
            this.tick += tick;    
    }

#endregion

#region Event
        public event EventHandler tick;
#endregion

#region Methods
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if(this.Enabled) this.passed += gameTime.ElapsedGameTime;

            while(this.Passed >= this.Interval)
            {
                this.passed -= this.Interval;
                this.OnTicked();
            }
        }

        public void Shutdown()
        {
            this.shutdown = true;
        }

        protected void OnTicked() 
        {
            if(this.tick != null) {
                this.tick(this, EventArgs.Empty);
            }
            
            if(shutdown)
                this.Stop();
        }
#endregion
    }
}
