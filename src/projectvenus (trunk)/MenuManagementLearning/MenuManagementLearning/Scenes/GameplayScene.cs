using GameGenerics.Graphics;
using GameGenerics.Input;
using GameGenerics.Scenes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjektVenus
{
    public class GameplayScene : AbstractGameScene
    {
        #region Fields
        private Level currentLevel;
        private HUD currentHUD;

        private RenderTarget2D levelRender;
        private SamplerState samplerState;
        private Effect effect;
        #endregion


        #region Constructor
        public GameplayScene(SceneManager sceneManager, Level level)
            : base(sceneManager)
        {
            this.ForcedDrawing = true;
            this.currentLevel = level;
            this.currentHUD = new HUD();
        }
        #endregion


        #region Initialize & LoadContent
        public override void Initialize()
        {
            this.currentLevel.Initialize();
            //this.currentHUD.Initialize();
            this.levelRender = new RenderTarget2D(this.Game.GraphicsDevice, 640, 480);
            this.samplerState = new SamplerState();
            this.samplerState.AddressU = TextureAddressMode.Wrap;
            this.samplerState.AddressV = TextureAddressMode.Wrap;
            this.samplerState.Filter = TextureFilter.Point;

            effect = Content.Load<Effect>("Effect1");
            base.Initialize();
        }
        #endregion


        #region Update & Draw
        public override void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
            if (!coveredByOtherScene)
            {
                this.currentLevel.Update(gameTime);
                //this.currentHUD.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw the level in the renderTarget
            this.GraphicsDevice.SetRenderTarget(this.levelRender);
            this.currentLevel.Draw(gameTime);
            this.GraphicsDevice.SetRenderTarget(null);
            // draw the render
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);
            //effect.CurrentTechnique.Passes[0].Apply();
            this.SpriteBatch.Draw(this.levelRender, new Rectangle(0, 0, this.Game.Window.ClientBounds.Width, this.Game.Window.ClientBounds.Height), Color.White);
            //this.currentLevel.Draw(gameTime);
            this.SpriteBatch.End();

            //this.currentHUD.Draw(gameTime);
            base.Draw(gameTime);
        }
        #endregion


        #region Methods
        public override void HandleInput()
        {
            if (InputState.IsPauseGame())
            {
                new PauseGameMenu(this.SceneManager).Add();
            }

            base.HandleInput();
        }
        #endregion
    }
}
