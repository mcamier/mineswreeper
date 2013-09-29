using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Graphics
{
    public class ParallaxSideScroller : Base2DGameComponent
    {
        #region Fields
        bool constraintOnX;
        bool constraintOnY;
        List<ScrollingLayer> layers;

        private SamplerState samplerState;
        #endregion

         
        #region Properties
        public bool ConstraintOnX
        {
            get { return this.constraintOnX; }
        }

        public bool ConstraintOnY
        {
            get { return this.constraintOnY; }
        }
        #endregion


        #region Constructors
        public ParallaxSideScroller(Game game, bool constraintOnX, bool constraintOnY) : base(game) 
        {
            this.layers = new List<ScrollingLayer>();

            this.constraintOnX = constraintOnX;
            this.constraintOnY = constraintOnY;

            this.samplerState = new SamplerState();
            this.samplerState.AddressU = TextureAddressMode.Wrap;
            this.samplerState.AddressV = TextureAddressMode.Wrap;
            this.samplerState.Filter = TextureFilter.Point;
        }
        #endregion

        public void Add(string asset)
        {
            // Add the layer
            this.layers.Add(new ScrollingLayer(this, this.Content.Load<Texture2D>(asset), 1f / (float)Math.Pow(2, this.layers.Count)));

            // Sort the layers by Depth property
            this.layers.Sort(delegate(ScrollingLayer a, ScrollingLayer b)
            {
                return a.Depth > b.Depth ? 1 : a.Depth < b.Depth ? -1 : 0;
            });
        }

        public void MoveBy(Vector2 direction)
        {
            foreach (ScrollingLayer layer in this.layers)
            {
                layer.MoveBy(direction);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, this.samplerState, DepthStencilState.None, RasterizerState.CullNone);
            foreach(ScrollingLayer layer in this.layers)
            {
                this.SpriteBatch.Draw(layer.Texture, layer.Position, new Rectangle(0, 0, layer.Width, layer.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            }
            this.SpriteBatch.End();
        }
    }
}
