using System;
using System.Collections.Generic;
using GameData;
using GameGenerics.Debug;
using GameGenerics.Input;
using GameGenerics.Scenes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{

    public class ProjektVenus : Game
    {
        #region Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SceneManager sceneManager;
        #endregion

        #region Constructor
        public ProjektVenus()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

            graphics.SynchronizeWithVerticalRetrace = false;
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;
            Window.AllowUserResizing = true;
            
            this.sceneManager = new SceneManager(this);
            FPS fps = new FPS(this);
            fps.DrawOrder = 1;

            this.Components.Add(new InputState(this));
            this.Components.Add(fps);
            this.Components.Add(sceneManager);
            //new SplashscreenScene(sceneManager).Add();

            List<LevelDesc> levels = Content.Load<List<LevelDesc>>("levels");
            Level loadedLevel = Level.Build(this, levels[0]);
            new GameplayScene(sceneManager, loadedLevel).Add();
            //new GameplayScene(sceneManager, new LevelTest(this)).Add();
            
            // For XBox support
            //this.Components.Add(new GamerServicesComponent(this));
        }
        #endregion

        #region Properties
        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }
        #endregion

        #region Methods
        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 640; // x1,8
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {       
            this.Content.Dispose();
            this.spriteBatch.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputState.IsPressedKeyOnce(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }
            if (InputState.IsPressedKeyOnce(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
            if(InputState.IsPressedKeyOnce(Microsoft.Xna.Framework.Input.Keys.Up)) {
                graphics.PreferredBackBufferWidth = 1200; // x1,8
                graphics.PreferredBackBufferHeight = 900;

                graphics.ApplyChanges();
            }
            if(InputState.IsPressedKeyOnce(Microsoft.Xna.Framework.Input.Keys.Down)) {
                graphics.PreferredBackBufferWidth = 640; // x1,8
                graphics.PreferredBackBufferHeight = 480;
                graphics.ApplyChanges();
            }
            base.Update(gameTime);
        }

   
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
        #endregion
    }
}