using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameGenerics.Scenes.Core;

namespace ProjektVenus
{
    public class CreditScene : AbstractMenuScene
    {
        public CreditScene(SceneManager sceneManager, String title)
            : base(sceneManager, title)
        {
            var backMenuItem = new TextMenuItem(this, "Retour");
            backMenuItem.ItemSelected += backMenuItemSelected;
            MenuItems.Add(backMenuItem);
        }

        public void backMenuItemSelected(Object sender, EventArgs e)
        {
            this.Remove();
        }
    }
}
