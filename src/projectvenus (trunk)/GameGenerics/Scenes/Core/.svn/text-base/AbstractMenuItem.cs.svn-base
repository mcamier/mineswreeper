using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameGenerics.Scenes.Core
{
    public enum ScreenLocation 
    {
        TOP_RIGHT, TOP_LEFT, TOP_MIDDLE,
        RIGHT, LEFT, CENTER,
        BOTTOM_RIGHT, BOTTOM_LEFT, BOTTOM_MIDDLE,
        FREE
    }
    public abstract class AbstractMenuItem
    {
        #region Fields
        private AbstractMenuScene parent;
        private Vector2 position;
        private bool allowFocus;
        #endregion


        #region Properties
        public AbstractMenuScene AbstractMenuScene
        {
            get { return this.parent; }
            private set { this.parent = value; }
        }

        public bool AllowFocus
        {
            get { return this.allowFocus; }
            set { this.allowFocus = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public float X
        {
            get { return this.position.X; }
            set { this.position.X = value; }
        }

        public float Y
        {
            get { return this.position.Y; }
            set { this.position.Y = value; }
        }

        public abstract float Width
        {
            get;
        }

        public abstract float Height
        {
            get;
        }
        #endregion


        #region Events
        public event EventHandler ItemSelected;
        #endregion


        #region Constructors
        public AbstractMenuItem(AbstractMenuScene parent)
        {
            this.AbstractMenuScene = parent;
            this.allowFocus = true;
        }
        #endregion


        #region Update & Draw
        public virtual void Update(bool isSelected, GameTime gameTime)
        {


        }

        public abstract void Draw(bool isSelected, GameTime gameTime);
        #endregion


        #region Methods
        public void OnSelected() 
        {
            if (this.ItemSelected != null)
                this.ItemSelected(this, EventArgs.Empty);
        }
        #endregion
    }
}

