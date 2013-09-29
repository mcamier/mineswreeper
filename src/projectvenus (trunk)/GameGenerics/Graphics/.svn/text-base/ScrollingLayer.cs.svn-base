using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Graphics
{
    class ScrollingLayer
    {
        #region Fields
        private ParallaxSideScroller scroller;
        private float depth;
        private Vector2 position;
        private Texture2D texture;
        #endregion


        #region Properties
        public float Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        public Texture2D Texture 
        {
            get { return this.texture; }
        }

        public Vector2 Position
        {
            get { return this.position; }
        }
        
        public int Width
        {
            get { return ((this.scroller.Game.Window.ClientBounds.Width / this.texture.Width) + 2) * this.texture.Width; }
        }

        public int Height
        {
            get { return ((this.scroller.Game.Window.ClientBounds.Height / this.texture.Height) + 2) * this.texture.Height; }
        }

        private Vector2 Middle
        {
            get
            {
                float x = Modulate((this.scroller.Game.Window.ClientBounds.Width - this.texture.Width) / 2f, this.Texture.Width);
                float y = Modulate((this.scroller.Game.Window.ClientBounds.Height - this.texture.Height) / 2f, this.Texture.Height);
                return new Vector2(x, y);
            }
        }
        #endregion


        #region Constructors
        public ScrollingLayer(ParallaxSideScroller scroller, Texture2D texture, float depth)
        {
            this.scroller = scroller;
            this.texture = texture;
            this.depth = depth;
            this.position = this.Middle;
        }
        #endregion

        public void MoveBy(Vector2 direction)
        {
            this.position += direction * this.depth;

            if (this.scroller.ConstraintOnX)
            {
                this.position.X = MathHelper.Clamp(this.Position.X, this.Middle.X + (this.Middle.X * this.depth), this.Middle.X - (this.Middle.X * this.depth));
            }

            if (this.scroller.ConstraintOnY)
            {
                this.position.Y = MathHelper.Clamp(this.Position.Y, this.Middle.Y + (this.Middle.Y * this.depth), this.Middle.Y - (this.Middle.Y * this.depth));
            }

            // Re-position the texture with a modulo
            this.position.X = (this.Position.X - this.Texture.Width) % this.Texture.Width;
            this.position.Y = (this.Position.Y - this.Texture.Height) % this.Texture.Height;
        }

        private static float Modulate(float value, float modulo)
        {
            return ((value - modulo) % modulo);
        }
    }
}
