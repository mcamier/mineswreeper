using System;
using Microsoft.Xna.Framework;

namespace GameGenerics.Interpolation
{
    public class FloatInterpolation : IInterpolation<float>
    {
        float min;
        float max;

        public float Min
        {
            get { return this.min; }
            set { this.min = value; }
        }

        public float Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public FloatInterpolation()
        {
            this.max = 1;
            this.min = 0;
        }

        public FloatInterpolation(float min, float max)
        {
            this.max = max;
            this.min = min;
        }

        public float Interpolate(float value)
        {
            return this.min + (this.max - this.min) * value;
        }
    }
}
