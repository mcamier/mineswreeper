using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Scenes.Core
{
    public class GraphicMenuItem : AbstractMenuItem
    {
        #region Fields
        private Texture2D texture;
        #endregion


        #region Properties
        public Texture2D Texture
        {
            get { return this.texture; }
        }
        public override float Width
        {
            get { return this.texture.Width; }
        }

        public override float Height
        {
            get { return this.texture.Height; }
        }
        #endregion


        #region Constructors
        public GraphicMenuItem(AbstractMenuScene parent, Texture2D texture) : base(parent)
        {
            this.texture = texture;
        }
        #endregion

        public override void Draw(bool isSelected, GameTime gameTime) { }
    }
}
