using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameGenerics.Path;
using System;

namespace ProjektVenus
{
    public abstract class EnemyBase : CharacterEntity
    {
        #region Fields
        private Vector2[] path;
        protected float speed;
        private float _t;
        private int _currentPath;
        private float _len;
        private Vector2 offset;
        private bool cyclic;
        #endregion


        #region Properties
        public Vector2 SpawnLocation
        { 
            get { return this.offset;}
            set { 
                this.offset = value;
                this.Position = value;
            }
        }
        #endregion


        #region Constructors
        public EnemyBase(Game game, Texture2D texture)
            : base(game, texture)
        {
            this.InitPath();
        }
        
        public EnemyBase(Game game) : base(game) 
        {
            this.InitPath();
        }
        #endregion


        private void InitPath()
        {
            
            this.cyclic = true;
            this.speed = 50f;
            this.path = new Vector2[4];
            this._t = 0;
            this.path[0] = new Vector2(0, 0);
            this.path[1] = new Vector2(100, 0);
            this.path[2] = new Vector2(50, 100);
            this.path[3] = new Vector2(0, 0);
            this.EnterSegment(0);
        }
        public override void Update(GameTime gameTime)
        {
            this.Move((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public void TakeDamage(int amountDamage)
        {
            this.Health -= amountDamage;
        }

        public bool Move(float dt)
        { 
            _t += (dt * this.speed);

            if (this._t >= this._len)
            {
                EnterSegment(this._currentPath + 1);
            }
            
            this.Position = (this._currentPath >= this.path.Length - 1) 
                ? offset
                : offset + this.path[_currentPath] + (this.path[_currentPath + 1] - this.path[_currentPath]) * (_t / _len);
            //  return true as long as I'm still following the path
            return this._currentPath < this.path.Length;
        }

        private void EnterSegment(int seg)
        {
            this._currentPath = seg;
            if (this._currentPath < this.path.Length - 1)
            {
                this._len = (this.path[this._currentPath + 1] - this.path[this._currentPath]).Length();
                this._t = 0;
            }
            else if (this.cyclic)
            {
                this._currentPath = 0;
                this._len = (this.path[this._currentPath + 1] - this.path[this._currentPath]).Length();
                this._t = 0;
            }
        }
    }
}
