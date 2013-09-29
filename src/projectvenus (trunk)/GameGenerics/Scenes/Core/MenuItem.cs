using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameGenerics.Scenes.Core
{
    public class MenuItem
    {
        readonly String label;
        private bool allowFocus;
        private const float Scale = 0.8f;
        Vector2 position;
        private float _selectionFade;

        public String Label
        {
            get { return this.label; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public bool AllowFocus
        {
            get { return this.allowFocus; }
            set { this.allowFocus = value; }
        }

        public MenuItem(String label) : this(label, true)
        {}

        public MenuItem(String label, bool allowFocus)
        {
            this.label = label;
            this.allowFocus = allowFocus;
        }

        public delegate void ItemSelectEventHandler(Object o, EventArgs e);
        public event ItemSelectEventHandler Selected;

        public void OnSelectItem()
        {
            if (this.Selected != null)
                this.Selected(this, EventArgs.Empty);
        }

        public void Update(bool isSelected, GameTime gameTime)	
        {	
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;
	
            _selectionFade = isSelected 
	
                ? Math.Min(_selectionFade + fadeSpeed, 1) 
	
                : Math.Max(_selectionFade - fadeSpeed, 0);
	
        }
	
	
        public void Draw(AbstractMenuScene scene, bool isSelected, GameTime gameTime)
	
        {
            Color color;
            if (this.allowFocus)
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
            float scale = Scale + pulsate * 0.05f * _selectionFade;	
            color *= scene.TransitionAlpha;
            SceneManager sceneManager = scene.SceneManager;
            SpriteBatch spriteBatch = sceneManager.SpriteBatch;
            SpriteFont font = sceneManager.Font;
            Vector2 origin = new Vector2(0, font.LineSpacing / 2f);
            spriteBatch.DrawString(font, this.label, this.position, color, 0, origin, scale, SpriteEffects.None, 0);
        }

        public static int GetHeight(AbstractMenuScene scene)	
        {
            return (int)(scene.SceneManager.Font.LineSpacing * Scale);	
        }
	
        public int GetWidth(AbstractMenuScene scene)	
        {	
            return (int)(scene.SceneManager.Font.MeasureString(this.label).X * Scale);	
        }
    }
}
