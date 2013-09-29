using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjektVenus
{
    public abstract class Enemy : EnemyBase
    {
        #region Fields
        private int value;
        #endregion

        protected abstract String TexturePath { get; }

        #region Properties
        public int Value
        {
            get { return this.value; }
            protected set { this.value = value; }
        }
        #endregion


        #region Constructors
        public Enemy(Game game, Texture2D texture)
            : base(game, texture)
        { 
        }
        
        public Enemy(Game game) : base(game) { }
        #endregion

        protected override void LoadContent()
        {
            if (this.Texture == null)
                this.Texture = this.Game.Content.Load<Texture2D>(this.TexturePath);

            base.LoadContent();
        }
    }
}
