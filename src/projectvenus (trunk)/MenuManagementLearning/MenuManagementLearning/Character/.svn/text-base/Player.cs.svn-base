using System;
using GameGenerics.Graphics;
using GameGenerics.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{
    public sealed class Player : CharacterEntity
    {
        #region Constants
        private const int fireDelay = 150;
        private const int fireDelayWithShield = 200;
        private const float speedVerticalWithShield = 80f;
        private const float speedHorizontalWithShield = 110f;
        #endregion


        #region Fields
        private BulletManager bulletManager;
        private float previousFireTime = 0;
        private float power;
        private bool shieldActive;
        private Texture2D shieldTexture;
        private Texture2D fireTexture;
        private SpriteFont font;
        #endregion

        #region Properties
        public BulletManager BulletManager
        {
            get { return this.bulletManager; }
        }
        #endregion


        #region Constructors
        public Player(Game game)
            : base(game)
        {
        }
        #endregion


        #region LoadContent & Initialize
        public override void Initialize()
        {
            this.bulletManager = new BulletManager(this.Game);
            this.shieldActive = false;
            this.power = 100;
            this.Velocity = new Vector2(180f, 180f);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.Texture = this.Game.Content.Load<Texture2D>("Animations/Player_Shape");
            //this.Texture = this.Game.Content.Load<Texture2D>("ship_baron_01");
            this.fireTexture = this.Game.Content.Load<Texture2D>("laser");
            this.shieldTexture = this.Game.Content.Load<Texture2D>("Animations/Shield_Shape");
            this.font = this.Game.Content.Load<SpriteFont>("Fonts/debug");
            base.LoadContent();
        }
        #endregion


        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            // Moving Management
            var currentSpeedVertical = (this.shieldActive) ? speedVerticalWithShield : this.Velocity.Y;
            var currentSpeedHorizontal = (this.shieldActive) ? speedHorizontalWithShield : this.Velocity.X;

            if (InputState.IsPressed(InputActions.MoveUp))
                if (this.Position.Y > 0) this.Y -= currentSpeedVertical * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputState.IsPressed(InputActions.MoveDown))
                if (this.Position.Y < 480 - this.Height) this.Y += currentSpeedVertical * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputState.IsPressed(InputActions.MoveLeft))
                if (this.Position.X > 0) this.X -= currentSpeedHorizontal * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputState.IsPressed(InputActions.MoveRight))
                if (this.Position.X < 640 - this.Width) this.X += currentSpeedHorizontal * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Shield Management
            if (InputState.IsPressed(InputActions.ActiveShield))
            {
                if (this.power > 0)
                {
                    this.shieldActive = true;
                    this.power -= (float)gameTime.ElapsedGameTime.TotalSeconds * 20f;
                }
                else 
                {
                    this.shieldActive = false;
                    if (this.power < 100)
                    {
                        this.power += (float)gameTime.ElapsedGameTime.TotalSeconds * 5f;
                    }
                }
            }
            else
            {
                this.shieldActive = false;
                if (this.power < 100)
                {
                    this.power += (float)gameTime.ElapsedGameTime.TotalSeconds * 5f;
                }
            }
           
            // Firing management
            var delay = (this.shieldActive) ? fireDelayWithShield : fireDelay;
            previousFireTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (InputState.IsPressed(InputActions.Fire))
            {
                if (previousFireTime >= delay)
                {
                    previousFireTime = 0;
                    bulletManager.AddBullet(new Bullet(this.bulletManager, new Vector2(this.Position.X + this.Width / 2, this.Position.Y-5), this.fireTexture));
                    bulletManager.AddBullet(new Bullet(this.bulletManager, new Vector2(this.Position.X + this.Width / 2 - 15, this.Position.Y - 5), this.fireTexture));
                }
            }
            else if (InputState.IsPressed(InputActions.AltFire))
            {
                if (previousFireTime >= delay)
                {
                    previousFireTime = 0;
                    bulletManager.AddBullet(
                        new Bullet(this.bulletManager, new Vector2(this.Position.X + this.Width / 2 - 5, this.Position.Y), new Vector2(400f, 400f), new Vector2(1, 1), this.fireTexture));
                    bulletManager.AddBullet(
                        new Bullet(this.bulletManager, new Vector2(this.Position.X + this.Width / 2 + 5, this.Position.Y), new Vector2(400f, 400f), new Vector2(-1, 1), this.fireTexture));
                }
            }

            bulletManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            bulletManager.Draw(gameTime);

            this.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);
            if (this.shieldActive) 
                this.SpriteBatch.Draw(this.shieldTexture, new Vector2(this.Position.X - 6, this.Position.Y - 6), Color.White);

            this.SpriteBatch.Draw(this.Texture, this.Position, Color.White);

            this.SpriteBatch.DrawString(this.font, "health : " + this.Health, new Vector2(10, 400), Color.Black);
            this.SpriteBatch.DrawString(this.font, "power : " + (int)this.power, new Vector2(10, 420), Color.Black);
            this.SpriteBatch.End();
        }
        #endregion

        #region Methods
        #endregion
    }
}