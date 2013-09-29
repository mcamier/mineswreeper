using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameGenerics.Debug
{
    public class FPS : DrawableGameComponent
    {
        float elapseTime;
        int frameCounter;
        int fps;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public FPS(Game game) : base(game) 
        {
            elapseTime = 0f;
            frameCounter = 0;
            fps = 0;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteBatch = new SpriteBatch(GraphicsDevice);	
            font = this.Game.Content.Load<SpriteFont>("Fonts/debug");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            elapseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter++;

            if (elapseTime > 1)
            {
                fps = frameCounter;
                frameCounter = 0;
                elapseTime = 0;
            }

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "dev version 0.1", new Vector2(5, 0), Color.Red);
            spriteBatch.DrawString(font, "FPS : " + ((int)fps).ToString(), new Vector2(5, font.MeasureString("d").Y), Color.Red);

            spriteBatch.DrawString(font, gameTime.ElapsedGameTime.TotalSeconds + " ms", new Vector2(5, font.MeasureString("d").Y * 3), Color.Red);
            spriteBatch.DrawString(font, this.Game.GraphicsDevice.PresentationParameters.BackBufferWidth + "x" + this.Game.GraphicsDevice.PresentationParameters.BackBufferHeight, new Vector2(5, (font.MeasureString("d").Y) * 2), Color.Red);

            //spriteBatch.DrawString(font, "Work in progress, the final graphics and gameplay may change", new Vector2(5, this.Game.GraphicsDevice.PresentationParameters.BackBufferHeight - font.MeasureString("D").Y), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
