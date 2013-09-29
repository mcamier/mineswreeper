using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameGenerics.Graphics
{
    public class Animation : Sprite
    {
        #region Fields
        private TimeSpan frameTime;
        private int frameCount;
        private int currentFrame;
        private TimeSpan elapsedTime;
        private int frameWidth;
        private int frameHeight;
        private Rectangle sourceRect = new Rectangle();
        private Rectangle destRect = new Rectangle();
        private bool looping;
        private bool active;
        #endregion

        #region Properties
        public bool IsLoop
        {
            get { return this.looping; }
            set { this.looping = value; }
        }

        public bool IsActive
        {
            get { return this.active; }
            set { this.active = value; }
        }

        public int FrameCount
        {
            get { return this.frameCount; }
        }
        
        public TimeSpan FrameTime
        {
            get { return this.frameTime; }
            set { this.frameTime = value; }
        }

        public int CurrentFrame
        {
            get { return this.currentFrame; }
        }

        public new int Width
        {
            get { return this.frameWidth; }
        }

        public new int Height
        {
            get { return this.frameHeight; }
        }
        #endregion

        #region Constructors
        public Animation(Game game, String spritePath, int frameCount, TimeSpan frameTime, int frameWidth, int frameHeight, bool looping, bool active)
            : base(game, spritePath)
        {
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.looping = looping;
            this.active = active;
        }

        public Animation(Game game, String spritePath, Color color, int frameCount, TimeSpan frameTime, int frameWidth, int frameHeight, bool looping, bool active)
            : base(game, spritePath, color)
        {
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.looping = looping;
            this.active = active;
            this.Color = color;
        }
        #endregion
        
        #region Initialize & Loadcontent
        public override void Initialize()
        {
            elapsedTime = TimeSpan.Zero;
            currentFrame = 0;
            base.Initialize();
        }
        #endregion

        #region Update & Draw 
        public override void Update(GameTime gameTime)
        {
            if (this.IsActive == false)
                return;

            this.elapsedTime += TimeSpan.FromMilliseconds(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (this.elapsedTime > this.frameTime)
            {
                this.currentFrame++;
                if (this.currentFrame == frameCount)
                {
                    this.currentFrame = 0;
                    if (this.IsLoop == false)
                        this.IsActive = false;
                }
                this.elapsedTime = TimeSpan.Zero;
            }

            sourceRect = new Rectangle(currentFrame * Width, 0, Width, Height);

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destRect = new Rectangle((int)Position.X - (int)(this.Width * this.Scale) / 2,
            (int)Position.Y - (int)(this.Height * this.Scale) / 2,
            (int)(this.Width * this.Scale),
            (int)(this.Height * this.Scale));
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.IsActive)
            {
                SpriteBatch.Begin();
                SpriteBatch.Draw(this.texture, destRect, sourceRect, this.Color);
                SpriteBatch.End();
            }
        }
        #endregion
    }
}
