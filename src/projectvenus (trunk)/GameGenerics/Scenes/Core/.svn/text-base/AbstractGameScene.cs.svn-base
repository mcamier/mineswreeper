using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Scenes.Core
{
    public enum SceneState 
    {
        Appears,
        Active,
        Disappears,
        Hidden
    }

    public class AbstractGameScene : DrawableGameComponent
    {
    #region Fields
        bool isPopup;
        bool isExiting;
        TimeSpan appearsDelay = TimeSpan.Zero;
        TimeSpan disappearsDelay = TimeSpan.Zero;
        private float transitionPosition = 1;
        SceneState sceneState = SceneState.Appears;
        protected bool otherSceneHasFocus;
        SceneManager sceneManager;
        private SpriteBatch spriteBatch;
        private bool forcedDrawing = false;

    #endregion

    #region Properties
        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }

        public ContentManager Content
        {
            get { return this.Game.Content; }
        }

        public bool ForcedDrawing
        {
            get;
            set;
        }

        public bool IsPopup
        {
            get { return this.isPopup; }
            protected set { this.isPopup = value; }
        }

        public SceneManager SceneManager
        {
            get { return this.sceneManager; }
        }

         protected TimeSpan AppearsDelay
        {
            set { this.appearsDelay = value; }
        }

        protected TimeSpan DisappearsDelay
        {
            set { this.disappearsDelay = value; }	
        }
        
        protected float TransitionPosition
        {
            get { return this.transitionPosition; }
        }

        public SceneState SceneState
        {	
            get { return this.sceneState; }
        }

        public bool IsExiting
        {
            set { this.isExiting = value; }	
            protected get { return this.isExiting; }
        }

        public bool IsActive 
        {
            get { return !otherSceneHasFocus && (this.sceneState == SceneState.Active || this.sceneState == SceneState.Appears); }
        }

        public float TransitionAlpha
        {
            get { return 1f - this.transitionPosition; }
        }
    #endregion

    #region Constructors
        protected AbstractGameScene(SceneManager sceneMgr)
            : base(sceneMgr.Game)
        {
            this.sceneManager = sceneMgr;
        }
    #endregion

    #region Methods
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.sceneManager.Game.GraphicsDevice);
            base.LoadContent();
        }

        public virtual void Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene) 
        {
            this.otherSceneHasFocus = otherSceneHasFocus;

            if (this.isExiting) 
            {
                this.sceneState = SceneState.Disappears;

                if (!UpdateTransition(gameTime, this.disappearsDelay, 1))
                    this.sceneManager.RemoveScene(this);
            }
            else if (coveredByOtherScene)
            {
                this.sceneState = UpdateTransition(gameTime, this.disappearsDelay, 1) ? SceneState.Disappears : SceneState.Hidden;
            }
            else
            {
                this.sceneState = UpdateTransition(gameTime, this.appearsDelay, -1) ? SceneState.Appears : SceneState.Active;
            }
        }

        protected bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            float transitionDelta = time == TimeSpan.Zero
                            ? 1
                            : (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);

            this.transitionPosition += transitionDelta * direction;

            bool endTransition = ((direction < 0) && (this.transitionPosition <= 0)) || ((direction > 0) && (this.transitionPosition >= 1));
	        
            if (endTransition) this.transitionPosition = MathHelper.Clamp(this.transitionPosition, 0, 1);
	            return !endTransition;
        }

        public virtual void HandleInput() { }

        public void Remove()
        {
            if (this.disappearsDelay == TimeSpan.Zero)
                this.sceneManager.RemoveScene(this);
            else
                this.isExiting = true;
        }

        public void Add()
        {
            this.sceneManager.AddScene(this);
        }
    #endregion
    }
}
