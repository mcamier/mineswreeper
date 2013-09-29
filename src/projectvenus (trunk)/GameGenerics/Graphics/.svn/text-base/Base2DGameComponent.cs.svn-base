using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameGenerics.Graphics
{
    public class Base2DGameComponent : DrawableGameComponent
    {
        #region Fields
        private SpriteBatch spriteBatch;
        #endregion


        #region Properties
        public ContentManager Content 
        {
            get { return this.Game.Content; }
        }

        public SpriteBatch SpriteBatch 
        {
            get { return this.spriteBatch; }
        }
        #endregion


        #region constructors
        public Base2DGameComponent(Game game) : base(game) { }

        public Base2DGameComponent(Game game, SpriteBatch spriteBatch) : base(game) 
        {
            this.spriteBatch = spriteBatch;
        }
        #endregion


        #region Initialize & LoadContent
        protected override void LoadContent()
        {
            if(this.spriteBatch == null)
                this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            base.LoadContent();
        }
        #endregion

    }
}