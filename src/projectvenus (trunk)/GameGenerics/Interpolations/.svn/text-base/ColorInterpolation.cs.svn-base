using System;
using Microsoft.Xna.Framework;

namespace GameGenerics.Interpolation
{
    public class ColorInterpolation : IInterpolation<Color>
    {
        Color min;
        Color max;

        public Color Min
        {
            get { return this.min; }
            set { this.min = value; }
        }

        public Color Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public ColorInterpolation(Color min, Color max)
        {
            this.max = max;
            this.min = min;
        }

        public Color Interpolate(float amount)
        {
            return Color.Lerp(this.min, this.max, MathHelper.Clamp(amount, 0, 1));
        }
    }
}
