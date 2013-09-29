﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameGenerics.Graphics
{
    class ParallaxingBackground : DrawableGameComponent
    {
        #region Fields
        private string texturePath;
        private Texture2D texture;
        private Vector2[] positions;
        private int screenWidth;
        private int screenHeight;
        private int speed;
        private SpriteBatch spriteBatch;
        #endregion

        #region Constructors
        public ParallaxingBackground(Game game, String texturePath, int screenWidth, int screenHeight, int speed)
            : base(game)
        {
            this.texturePath = texturePath;
            this.speed = speed;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }
        #endregion

        #region Initialize & LoadContent
        public override void Initialize()
        {
            // If we divide the screen with the texture width then we can determine the number of tiles need.
            // We add 1 to it so that we won't have a gap in the tiling
            positions = new Vector2[screenWidth / texture.Width + 1];

            // Set the initial positions of the parallaxing background
            for (int i = 0; i < positions.Length; i++)
            {
                // We need the tiles to be side by side to create a tiling effect
                positions[i] = new Vector2(i * texture.Width, 0);
            }
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            this.texture = this.Game.Content.Load<Texture2D>(texturePath);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            texture.Dispose();
            base.UnloadContent();
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            int i = 0;

            // Update the positions of the background
            for (i = 0; i < positions.Length; i++)
            {
                // Update the position of the screen by adding the speed
                positions[i].X += speed;
                // If the speed has the background moving to the left
                if (speed <= 0)
                {
                    // Check the texture is out of view then put that texture at the end of the screen
                    if (positions[i].X <= -texture.Width)
                    {
                        positions[i].X = texture.Width * (positions.Length - 1);
                    }
                }

                // If the speed has the background moving to the right
                else
                {
                    // Check if the texture is out of view then position it to the start of the screen
                    if (positions[i].X >= texture.Width * (positions.Length - 1))
                    {
                        positions[i].X = -texture.Width;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            int i = 0;
            for (i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }

            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        #endregion
    }
}
