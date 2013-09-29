using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameGenerics.Scenes.Core
{
    public class TextMenuItem : AbstractMenuItem
    {
        #region Fields
        private String label;
        private float selectionFade = 1;
        private int scale = 1;
        #endregion

        #region Properties
        public int Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        public String Label
        {
            get { return this.label; }
            set { this.label = value; }
        }

        public override float Width
        {
            get { return (int)(this.AbstractMenuScene.SceneManager.Font.MeasureString(this.label).X * Scale); }
        }

        public override float Height
        {
            get { return (int)(this.AbstractMenuScene.SceneManager.Font.LineSpacing * Scale); }
        }
        #endregion

        #region Constructors
        public TextMenuItem(AbstractMenuScene parent, String label)
            : base(parent)
        {
            this.Label = label;
        }
        #endregion


        #region Update & Draw
        public override void Update(bool isSelected, GameTime gameTime)
        {
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            selectionFade = isSelected

                ? Math.Min(selectionFade + fadeSpeed, 1)

                : Math.Max(selectionFade - fadeSpeed, 0);

        }

        public override void Draw(bool isSelected, GameTime gameTime)
        {
            Color color;
            if (this.AllowFocus)
            {
                if (isSelected)
                    color = Color.Black;
                else
                    color = Color.White;
            }
            else
                color = Color.Gray;

            double time = gameTime.TotalGameTime.TotalSeconds;
            float pulsate = (float)Math.Sin(time * 6) + Scale;
            float scale = Scale + pulsate * 0.05f * selectionFade;
            color *= this.AbstractMenuScene.TransitionAlpha;
            SceneManager sceneManager = this.AbstractMenuScene.SceneManager;
            SpriteBatch spriteBatch = sceneManager.SpriteBatch;
            SpriteFont font = sceneManager.Font;
            Vector2 origin = new Vector2(0, font.LineSpacing / 2f);
            spriteBatch.DrawString(font, this.label, this.Position, color, 0, origin, 1, SpriteEffects.None, 0);
            
        }
        #endregion


        #region Methods
        #endregion
    }
}
