using System;
using Microsoft.Xna.Framework;

namespace GameGenerics.Interpolation
{
    public class Vector2Interpolation : IInterpolation<Vector2>
    {
        Vector2 min;
        Vector2 max;

        public Vector2 Min
        {
            get { return this.min; }
            set { this.min = value; }
        }

        public Vector2 Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public Vector2Interpolation(Vector2 min, Vector2 max)
        {
            this.max = max;
            this.min = min;
        }

        public Vector2 Interpolate(float amount)
        {
            return Vector2.Lerp(this.min, this.max, amount);
        }
    }
}
