using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameGenerics.Graphics;

namespace ProjektVenus
{
    class Walker : Enemy
    {
        protected override String TexturePath
        {
            get { return "Animations/Enemy_Shape"; }
        }

        #region Constructors
        public Walker(Game game, Texture2D texture) : base(game, texture) 
        {}

        public Walker(Game game) : base(game) { }
        #endregion

        public override void Initialize()
        {
            this.Value = 200;
            this.Health = 140;
            base.Initialize();
        }
    }
}
