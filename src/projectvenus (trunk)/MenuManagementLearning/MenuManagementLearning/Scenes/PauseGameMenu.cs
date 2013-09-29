using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameGenerics.Scenes.Core;

namespace ProjektVenus
{
    class PauseGameMenu : AbstractMenuScene
    {
        Texture2D blank;

        public PauseGameMenu(SceneManager sceneManager) : base(sceneManager, "Pause") 
        {
            var resumeGameMenuItem = new TextMenuItem(this, "Reprendre");
            var backMenuItem = new TextMenuItem(this, "Retour ecran titre");
            var exitMenuItem = new TextMenuItem(this, "Quitter");

            resumeGameMenuItem.ItemSelected += resumeGameMenuItemSelected;
            backMenuItem.ItemSelected += backMenuItemSelected;
            exitMenuItem.ItemSelected += exitMenuItemSelected;

            MenuItems.Add(resumeGameMenuItem);
            MenuItems.Add(backMenuItem);
            MenuItems.Add(exitMenuItem);
            this.IsPopup = false;
        }

        protected override void LoadContent()
        {
            ContentManager content = new ContentManager(SceneManager.Game.Services, "Content");
            this.blank = content.Load<Texture2D>("blank");

            base.LoadContent();
        }

        public void resumeGameMenuItemSelected(Object sender, EventArgs e) {
            this.Remove();
        }
        public void exitMenuItemSelected(Object sender, EventArgs e)
        {
            SceneManager.Game.Exit();
        }
        public void backMenuItemSelected(Object sender, EventArgs e)
        {
            SceneManager.Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.SceneState == SceneState.Active || this.SceneState == SceneState.Appears)
            {
                SpriteBatch spriteBatch = SceneManager.SpriteBatch;
                spriteBatch.Begin();
                spriteBatch.Draw(this.blank, new Rectangle(0, 0, this.GraphicsDevice.PresentationParameters.BackBufferWidth, this.GraphicsDevice.PresentationParameters.BackBufferHeight), new Color(.3f, .3f, .3f, .7f));
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
