using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameGenerics.Scenes.Core;
using GameGenerics.Input;
using GameGenerics.Timers;

namespace ProjektVenus
{
    class LogoScene : AbstractGameScene
    {
        SpriteBatch spriteBatch;
        ContentManager content;
        SpriteFont font;
        String pressStart = "PRESS START";
        int currentLetter;
        float delayLetter = 80;
        Timer timer;

        public LogoScene(SceneManager sceneManager)
            : base(sceneManager)
        { 
        
        }

        public override void Initialize()
        {
            //  this.content = new ContentManager(SceneManager.Game.Services, "Content");
            this.spriteBatch = SceneManager.SpriteBatch;
            base.Initialize();
            this.timer = new Timer(TimeSpan.FromMilliseconds(delayLetter), UpdateString,true);
        }

        private void UpdateString(Object sender, EventArgs e)
        {
            currentLetter++;
            if (currentLetter >= this.pressStart.Length)
            {
                this.timer.Stop();
                this.timer = null;
            }
        }

        protected override void LoadContent()
        {
            if (this.content == null)
                this.content = new ContentManager(SceneManager.Game.Services, "Content");

            this.font = content.Load<SpriteFont>("Fonts/Gamefont");

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            this.content.Unload();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            if(this.timer != null)
                this.timer.Update(gameTime);
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
        }

        public override void Draw(GameTime gameTime)
        {
            String text = pressStart.Substring(0, currentLetter);
            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(this.font, text, new Vector2(640/2 - this.font.MeasureString(text).X/2, 480/3), Color.White);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void HandleInput()
        {
            if(InputState.IsPressedOnce(InputActions.Accept)) 
            {
                new MainMenuScene(SceneManager, "Main Menu").Add();
            }

            base.HandleInput();
        }
    }
}
