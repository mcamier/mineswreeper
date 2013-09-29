using System;
using GameGenerics.Easing;
using GameGenerics.Interpolation;
using GameGenerics.Scenes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{
    class BackgroundScene : AbstractGameScene
    {
    #region Fields
        Texture2D skyTexture;
        Texture2D backgroundTexture;
        EasingCurve<float> backgroundPositionEasing;
    #endregion

    #region Properties
    #endregion

    #region Constructors
        public BackgroundScene(SceneManager sceneManager)
            : base(sceneManager)
        {
            this.AppearsDelay = TimeSpan.FromSeconds(0.7);
            this.DisappearsDelay = TimeSpan.FromSeconds(0.7);
        }
    #endregion

    #region Methods
        protected override void LoadContent()
        {
            this.backgroundTexture = this.Content.Load<Texture2D>("pixelart");
            this.skyTexture = this.Content.Load<Texture2D>("sky");
            this.backgroundPositionEasing = new EasingCurve<float>(new FloatInterpolation(-20, 20), this.Content.Load<Curve>("Curves/oscillate"), true);
            this.backgroundPositionEasing.Start();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, false);
            this.backgroundPositionEasing.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = SceneManager.SpriteBatch;
            var fullscene = new Rectangle(-300, -200 - (int)this.backgroundPositionEasing, 1186, 858);

            spriteBatch.Begin();
            spriteBatch.Draw(this.skyTexture, new Rectangle(-250, 0, 875, 452 + (int)this.backgroundPositionEasing), new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(this.backgroundTexture, fullscene, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.End();
        }
    #endregion 
    }
}
