using System;
using Microsoft.Xna.Framework;

namespace ProjektVenus
{
    public abstract class LevelActionBase
    {
        #region Fields
        public Level level;
        protected bool finished;
        #endregion


        #region Properties
        public bool IsFinished
        {
            get { return this.finished; }
            protected set { this.finished = value; }
        }
        #endregion


        #region Constructors
        public LevelActionBase(Level parent)
        {
            this.level = parent;
            this.finished = false;
        }
        #endregion

        public virtual void Initialize()
        {
            this.LoadContent();
        }
        protected virtual void LoadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}