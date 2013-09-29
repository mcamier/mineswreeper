using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Graphics
{
    class Camera2D
    {
        #region Fields
        protected float zoom;
        public Matrix tranform;
        public Vector2 position;
        protected float rotation;
        Game game;
        #endregion

        #region Properties
        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; if (this.zoom < 0.1f) this.zoom = 0.1f; }
        }

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        #endregion

        #region Constructors
        public Camera2D(Game game)
        {
            this.zoom = 1.0f;
            this.rotation = 0.0f;
            this.position = new Vector2(game.GraphicsDevice.Viewport.X / 2, game.GraphicsDevice.Viewport.Y / 2);
            this.game = game;
        }
        #endregion

        #region Methods
        public void Move(Vector2 amount)
        {
            this.position += amount;
        }

        public Matrix getTransformation(GraphicsDevice graphicsDevice)
        {
            this.tranform = Matrix.CreateTranslation(new Vector3(this.position.X - graphicsDevice.Viewport.Width * 0.5f, this.position.Y - graphicsDevice.Viewport.Height * 0.5f, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return tranform;
        }
        #endregion
    }
}
