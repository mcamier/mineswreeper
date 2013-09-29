using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameGenerics.Timers
{
    public class TimerBase : IUpdateable
    {
#region Fields
        private TimeSpan elapsed;
        bool enabled;
#endregion

#region Properties
        public bool Enabled
        {
            get { return this.enabled; }
            private set 
            {
                this.enabled = value;
                this.OnEnableChange();
            }
        }

        public TimeSpan Elapsed
        {
            get { return this.elapsed; }
        }

        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

#endregion
        
#region Constructors
        public TimerBase()
        {
            this.enabled = false;
            this.elapsed = TimeSpan.Zero;
        }

        public TimerBase(bool start)
        {
            this.enabled = false;
            this.elapsed = TimeSpan.Zero;

            if (start) this.Start();
        }
#endregion

#region Events
        public event EventHandler<System.EventArgs> EnabledChanged;
        public event EventHandler<System.EventArgs> UpdateOrderChanged;
        public event EventHandler Started;
        public event EventHandler Stopped;
        #endregion

#region Methods
        public void Start() 
        {
            this.Enabled = true;
            this.OnStart();
        }

        public void Stop() 
        {
            this.Enabled = false;
            this.OnStop();
        }

        public void Reset() 
        {
            this.Stop();
            this.elapsed = TimeSpan.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.enabled)
                this.elapsed += gameTime.ElapsedGameTime;
        }

        protected void OnEnableChange()
        {
            if (this.EnabledChanged != null)
                this.EnabledChanged(this, EventArgs.Empty);
        }

        protected void OnUpdateOrderChange()
        {
            if (this.UpdateOrderChanged != null)
                this.UpdateOrderChanged(this, EventArgs.Empty);
        }

        protected void OnStart()
        {
            if (this.Started != null)
                this.Started(this, EventArgs.Empty);
        }

        protected void OnStop()
        {
            if (this.Stopped != null)
                this.Stopped(this, EventArgs.Empty);
        }
#endregion
    }
}
