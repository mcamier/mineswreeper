using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace GameGenerics.Graphics
{
    public class Sprite : Base2DGameComponent
    {
        #region Fields
        private Vector2 position;
        private float scale;
        private float rotation;
        private Color color;
        private String spritePath;
        protected Texture2D texture;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public int Width
        {
            get { return this.texture.Width; }
        }

        public int Height
        {
            get { return this.texture.Height; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
        }
        #endregion

        #region Constructors
        public Sprite(Game game, String spritePath)
            : base(game)
        {
            this.position = Vector2.Zero;
            this.spritePath = spritePath;
            this.scale = 1f;
            this.rotation = 0f;
            this.color = Color.White;
        }

        public Sprite(Game game, String spritePath, Color color)
            : base(game)
        {
            this.position = Vector2.Zero;
            this.spritePath = spritePath;
            this.scale = 1f;
            this.rotation = 0f;
            this.color = color;
        }
        #endregion

        #region Initialize & LoadContent
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.texture = Content.Load<Texture2D>(this.spritePath);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            this.texture.Dispose();
            base.UnloadContent();
        }
        #endregion

        #region Update & Draw
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(this.texture, this.position, this.Color);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        #endregion
    }
}
