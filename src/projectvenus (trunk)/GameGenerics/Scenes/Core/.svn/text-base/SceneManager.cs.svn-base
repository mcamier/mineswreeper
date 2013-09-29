using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Scenes.Core
{
    public class SceneManager : DrawableGameComponent
    {
    #region Fields
        private readonly List<AbstractGameScene> scenes = new List<AbstractGameScene>();
        private readonly List<AbstractGameScene> scenesToUpdate = new List<AbstractGameScene>();

        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D blankTexture;

        private bool isInitialized;
    #endregion

    #region Properties
        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
        }
    #endregion

    #region Constructors
        public SceneManager(Game game)
            : base(game)
        {
            this.isInitialized = false;
        }
    #endregion

    #region Methods
        public override void Initialize()
        {
            base.Initialize();
            foreach (AbstractGameScene scene in scenes)
            {
                scene.Initialize();
            }
            this.isInitialized = true;
        }

        protected override void LoadContent()	
        {	
            ContentManager content = Game.Content;	
            this.spriteBatch = new SpriteBatch(GraphicsDevice);	
            this.font = content.Load<SpriteFont>("Fonts/gamefont");	
            this.blankTexture = content.Load<Texture2D>("blank");
        }

        public override void Update(GameTime gameTime)
        {
            this.scenesToUpdate.Clear();

            foreach (AbstractGameScene scene in this.scenes)
                this.scenesToUpdate.Add(scene);

            bool otherSceneHasFocus = !Game.IsActive;
            bool coveredByOtherScene = false;

            while (this.scenesToUpdate.Count > 0)
            {
                AbstractGameScene scene = this.scenesToUpdate[this.scenesToUpdate.Count - 1];
                this.scenesToUpdate.RemoveAt(this.scenesToUpdate.Count - 1);
                scene.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

                if (scene.SceneState == SceneState.Appears || scene.SceneState == SceneState.Active)
                {
                    // Si c'est la première scène, lui donner l'accès aux entrées utilisateur.
                    if (!otherSceneHasFocus)
                    {
                        scene.HandleInput();
                        otherSceneHasFocus = true;
                    }

                    // Si la scène courant n'est pas un pop-up et est active,
                    // informez les scènes suivantes qu'elles sont recouverte.
                    if (!scene.IsPopup)
                        coveredByOtherScene = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (AbstractGameScene scene in scenes)
            {
                if (scene.SceneState == SceneState.Hidden && !scene.ForcedDrawing)
                    continue;

                scene.Draw(gameTime);
                
            }
        }

        public void AddScene(AbstractGameScene scene)
        {
            if (this.isInitialized)
                scene.Initialize();

            scene.IsExiting = false;
            
            this.scenes.Add(scene);
        }

        public void RemoveScene(AbstractGameScene scene)
        {
            this.scenes.Remove(scene);
            this.scenesToUpdate.Remove(scene);
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);
            this.spriteBatch.End();
        }
    #endregion
    }
}
