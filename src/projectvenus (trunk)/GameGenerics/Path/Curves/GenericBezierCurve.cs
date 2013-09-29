using Microsoft.Xna.Framework;
using System;

namespace GameGenerics.Path.Curves
{
    public class GenericBezierCurve : BezierCurve
    {
        #region Fields
        Vector2[] p;
        int[,] bins;
        #endregion

        #region Properties
        public Vector2[] P 
        {
            get { return this.p; }
            set { this.p = value; }
        }
        #endregion

        #region Constructors
        public GenericBezierCurve(params Vector2[] p)
        {
            this.p = p;
            bins = new int[P.Length + 1, P.Length + 1];
            this.BuildBinomialArray((byte)P.Length, (byte)P.Length);
        }
        #endregion

        #region Methods
        public override Vector2 PositionAt(float t)
        {
            Vector2 result = Vector2.Zero;

            for (int i = 0; i < P.Length; i++)
            {  
                result.X += (float)(bins[P.Length-1, i] * Math.Pow(t, i) * Math.Pow((1 - t), P.Length-1 - i) * P[i].X);
                result.Y += (float)(bins[P.Length-1, i] * Math.Pow(t, i) * Math.Pow((1 - t), P.Length-1 - i) * P[i].Y);
            }
            return result;
        }

        private void BuildBinomialArray(byte n, byte k)
        {
            for(byte i = 0 ; i <=n ; i++)
            {
                for (byte j = 0; j <= Math.Min(i,k); j++)
                {
                    if (j == 0 || j == i)
                        bins[i,j] = 1;
                    else
                    {
                        bins[i, j] = bins[i-1, j-1] + bins[i-1, j];
                    }
                }
            }
        }
        #endregion
    }
}
