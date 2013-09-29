using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameGenerics.Path.Curves
{
    public class QuadraticCurve : BezierCurve
    {
        #region Properties
        Vector2 P1 { get; set; }
        Vector2 P2 { get; set; }
        Vector2 C { get; set; }
        #endregion

        #region Constructors
        public QuadraticCurve(Vector2 p1, Vector2 c, Vector2 p2) 
        {
            P1 = p1;
            P2 = p2;
            C = c;
        }
        #endregion

        #region Methods
        public override Vector2 PositionAt(float t)
        {
            t = MathHelper.Clamp(t, 0, 1);
            return new Vector2(
                (float)(Math.Pow((1 - t), 2) * P1.X + 2 * t * (1 - t) * C.X + Math.Pow(t, 2) * P2.X),
                (float)(Math.Pow((1 - t), 2) * P1.Y + 2 * t * (1 - t) * C.Y + Math.Pow(t, 2) * P2.Y) );
        }
        #endregion
    }
}
