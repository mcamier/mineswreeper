using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameGenerics.Scenes.Core;
using GameGenerics.Easing;
using GameGenerics.Interpolation;
using GameGenerics.Input;

namespace ProjektVenus
{
    class SplashscreenScene : AbstractGameScene
    {
#region Fields
        ContentManager content;
        Texture2D splashTexture;
        Vector2 position;
        EasingCurve<float> opacity;
        EasingCurve<float> scale;
#endregion

#region Constructors
        public SplashscreenScene(SceneManager sceneManager) : base(sceneManager) { }
#endregion

#region Methods
        protected override void LoadContent()
        {
            if (this.content == null)
                this.content = new ContentManager(SceneManager.Game.Services, "Content");

            this.splashTexture = content.Load<Texture2D>("splashlogo");
            this.opacity = new EasingCurve<float>(new FloatInterpolation(0, 1), content.Load<Curve>("Curves/SplashScreenOpacity"));
            this.scale = new EasingCurve<float>(new FloatInterpolation(0, 1), content.Load<Curve>("Curves/SplashScreenSize"));
            this.opacity.Start(); 
            this.scale.Start();
            this.scale.Stopped += CallMainMenuScene;
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
            this.position = new Vector2(640 / 2, 480/ 2);
            this.opacity.Update(gameTime);
            this.scale.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = SceneManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(splashTexture, position, null, Color.White*this.opacity, 0f, new Vector2(splashTexture.Width/2, splashTexture.Height/2), this.scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        private void CallMainMenuScene(Object sender, EventArgs e) 
        {
            new BackgroundScene(SceneManager).Add();
            new LogoScene(SceneManager).Add();
            this.Remove();
            this.Dispose();
        }

        public override void HandleInput()
        {
            if (InputState.IsPauseGame())
                this.scale.Stop();
        }
#endregion
    }
}
