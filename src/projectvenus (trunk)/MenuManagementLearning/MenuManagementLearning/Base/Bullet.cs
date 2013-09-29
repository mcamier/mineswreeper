using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{
    public class Bullet : MoveableObject
    {
        #region Fields
        private BulletManager bulletManager;
        private int damage;
        public Vector2 Velocity { get; set; }
        public Vector2 Direction;
        private float angle;
        #endregion


        #region Properties
        public int Damage
        {
            get { return this.damage; }
            private set { this.damage = value; }
        }

        public float Angle
        {
            get { return this.angle; }
        }
        #endregion


        #region Constructors
        public Bullet(BulletManager bulletManager, Vector2 position, Texture2D texture)
            : this(bulletManager, position, new Vector2(400f, 400f), new Vector2(0, 1), texture) { }

        public Bullet(BulletManager bulletManager, Vector2 position, Vector2 velocity, Vector2 direction, Texture2D texture)
            : base(bulletManager.Game, texture)
        {
            this.bulletManager = bulletManager;
            this.Position = position;
            this.Velocity = velocity;
            this.Direction = direction;
            this.Texture = texture;
            this.Damage = 10;
            // TODO : Modify calculus
            this.angle = (direction.X * direction.Y + 1 * 0) / ((float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y));
            int sign = (direction.X * direction.Y - 0 * 1) > 0 ? 1 : -1;
            this.angle *= sign;
            /*
            Color[] bits = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(bits);
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    if (x == 0) bits[y * texture.Width + x] = Color.Red;
                    else if (x == texture.Width - 1) bits[y * texture.Width + x] = Color.Red;
                    else if (y == 0) bits[y * texture.Width + x] = Color.Red;
                    else if (y == texture.Height - 1) bits[y * texture.Width + x] = Color.Red;
                }
            }
            texture.SetData<Color>(bits);*/
        }
        #endregion

        public override void Initialize()
        {
            this.CollisionDetected += (MoveableObject sender, MoveableObject CollidingObject) =>
            {
                this.bulletManager.RemoveBullet(this);
            };
            base.Initialize();
        }


        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * (this.Velocity.Y * this.Direction.Y);
            this.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * (this.Velocity.X * this.Direction.X);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(this.Texture, this.Position, null, Color.White, angle, Vector2.Zero, 1f, SpriteEffects.None, 1);
            this.SpriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
