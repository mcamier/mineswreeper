using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjektVenus
{
    public class BulletManager : DrawableGameComponent, IEnumerable
    {
        #region Fields
        List<Bullet> bulletList = new List<Bullet>();
        List<Bullet> bulletToUpdate= new List<Bullet>();
        static readonly int margin = 100;
        #endregion


        #region Constructors
        public BulletManager(Game game) : base(game)
        { 
        
        }
        #endregion


        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            // improve algorithm
            this.bulletToUpdate.Clear();

            foreach (Bullet b in this.bulletList)
                this.bulletToUpdate.Add(b);

            foreach (Bullet b in bulletToUpdate)
            {
                if ((b.Position.X < 0 - margin) ||
                    (b.Position.Y < 0 - margin) ||
                    (b.Position.X > this.Game.GraphicsDevice.PresentationParameters.BackBufferWidth + margin) ||
                    (b.Position.Y > this.Game.GraphicsDevice.PresentationParameters.BackBufferHeight + margin))
                {
                    this.bulletList.Remove(b);
                }
                else 
                {
                    b.Update(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime) 
        {
            foreach(Bullet b in bulletList) 
            {
                b.Draw(gameTime);
            }
        }
        #endregion


        #region Methods
        public void AddBullet(Bullet bullet)
        {
            // Bad way to achieve bullet's initialization
            bullet.Initialize();
            this.bulletList.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            this.bulletList.Remove(bullet);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                yield return bulletList[i];
            }
        }
        #endregion
    }
}
