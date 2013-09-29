using Microsoft.Xna.Framework;
using System;

namespace GameGenerics.Path.Curves
{
    public class CubicCurve : BezierCurve
    {
        #region Properties
        Vector2 P1 { get; set; }
        Vector2 P2 { get; set; }
        Vector2 P3 { get; set; }
        Vector2 P4 { get; set; }
        #endregion

        #region Constructors
        public CubicCurve(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) 
        {
            P1 = p1; P2 = p2; P3 = p3; P4 = p4;
        }
        #endregion

        #region Methods
        public override Vector2 PositionAt(float t)
        {
            t = MathHelper.Clamp(t, 0, 1);
            return new Vector2(
                (float)(Math.Pow((1 - t), 3) * P1.X + 3 * t * Math.Pow((1 - t), 2) * P2.X + 3 * Math.Pow(t, 2) * (1 - t) * P3.X + Math.Pow(t, 3) * P4.X),
                (float)(Math.Pow((1 - t), 3) * P1.Y + 3 * t * Math.Pow((1 - t), 2) * P2.Y + 3 * Math.Pow(t, 2) * (1 - t) * P3.Y + Math.Pow(t,3) * P4.Y));
        }
        #endregion
    }
}
