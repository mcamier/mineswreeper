using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ProjektVenus
{
    public abstract class CharacterEntity : MoveableObject
    {
        #region Fields
        private int health;
        private Vector2 velocity;
        #endregion


        #region Properties
        public Vector2 Velocity 
        { 
            get { return this.velocity; }
            set { this.velocity = value; }
        }
        public int Health
        {
            get { return this.health; }
            set { 
                this.health = value;
                if (this.Health <= 0)
                {
                    if (this.EventDeadCharacter != null)
                        this.EventDeadCharacter(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        #region Events
        public event EventHandler EventDeadCharacter;
        #endregion


        #region Constructors
        public CharacterEntity(Game game, Texture2D texture) : base(game, texture) {}
        public CharacterEntity(Game game) : base(game) { }
        #endregion


        #region Initialize
        public override void Initialize()
        {
            this.health = 100;
            this.Velocity = new Vector2(100f, 90f);
            this.CollisionDetected += new EventMoveableObjectCollisionHandler(OnCollision);
            base.Initialize();
        }
        #endregion


        #region Update & Draw
        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);
            this.SpriteBatch.Draw(this.Texture, this.Position, Color.White);
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion


        #region Methods
        public virtual void OnCollision(MoveableObject sender, MoveableObject CollidingObject)
        {
            int damage = (CollidingObject is Bullet) ? ((Bullet)CollidingObject).Damage : 999;
            ((CharacterEntity)sender).Health -= damage;
        }
        #endregion
    }
}