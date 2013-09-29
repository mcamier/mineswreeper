using System;
using Microsoft.Xna.Framework;
using GameGenerics.Timers;
using GameGenerics.Interpolation;

namespace GameGenerics.Easing
{
    public class EasingCurve<T> : Timer
    {
        #region Fields
        private IInterpolation<T> interpolation;
        private Curve curve;
        private bool isLoop;
        #endregion

        #region Constructors
        public EasingCurve(IInterpolation<T> interpolation, Curve curve)
            : base(TimeSpan.FromSeconds(curve.Keys[curve.Keys.Count - 1].Position))
        {
            this.interpolation = interpolation;
            this.curve = curve;
            this.isLoop = false;
        }

        public EasingCurve(IInterpolation<T> interpolation, Curve curve, bool isLoop)
            : base(TimeSpan.FromSeconds(curve.Keys[curve.Keys.Count - 1].Position))
        {
            this.interpolation = interpolation;
            this.curve = curve;
            this.isLoop = isLoop;
        }

        #endregion

        #region Properties
        public T Value
        {
            get { return this.interpolation.Interpolate(this.curve.Evaluate((float)this.Elapsed.TotalSeconds)); }
        }

        public bool IsLoop
        {
            get { return this.isLoop; }
            set { this.isLoop = value; }
        }

        #endregion

        #region Methods
        public static implicit operator T(EasingCurve<T> m)
        {
            return m.Value;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if(this.Elapsed > TimeSpan.FromSeconds(curve.Keys[curve.Keys.Count - 1].Position) ) {
                if (!IsLoop)
                {
                    this.Stop();
                }
            }
            
        }

        #endregion
    }
}