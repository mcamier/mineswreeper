using System;
using Microsoft.Xna.Framework;

namespace GameGenerics.Interpolation
{
    public class Vector3Interpolation : IInterpolation<Vector3>
    {
        Vector3 min;
        Vector3 max;

        public Vector3 Min
        {
            get { return this.min; }
            set { this.min = value; }
        }

        public Vector3 Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public Vector3Interpolation(Vector3 min, Vector3 max)
        {
            this.max = max;
            this.min = min;
        }

        public Vector3 Interpolate(float amount)
        {
            return Vector3.Lerp(this.min, this.max, amount);
        }
    }
}
