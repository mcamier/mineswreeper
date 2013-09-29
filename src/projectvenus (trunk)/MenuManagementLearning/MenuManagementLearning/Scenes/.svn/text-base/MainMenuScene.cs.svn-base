using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameGenerics.Scenes.Core;
using GameGenerics.Input;

namespace ProjektVenus
{
    class MainMenuScene : AbstractMenuScene
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public MainMenuScene(SceneManager sceneManager, String title)
            : base(sceneManager, title)
        {
            var campaignMenuItem = new TextMenuItem(this, "Campaign");
            var optionsMenuItem = new TextMenuItem(this, "Options");
            var creditsMenuItem = new TextMenuItem(this, "Credits");
            var exitMenuItem = new TextMenuItem(this, "Quitter");

            campaignMenuItem.ItemSelected += PlayGameMenuItemSelected;
            exitMenuItem.ItemSelected += ExitMenuItemSelected;
            creditsMenuItem.ItemSelected += CreditMenuItemSelected;

            MenuItems.Add(campaignMenuItem);

            MenuItems.Add(optionsMenuItem);
            MenuItems.Add(creditsMenuItem);
            MenuItems.Add(exitMenuItem);
        }
        #endregion

        private void PlayGameMenuItemSelected(object sender, EventArgs e)
        {
            new CampaignScene(SceneManager, "Campaign").Add();
        }
        private void ExitMenuItemSelected(object sender, EventArgs e)
        {
            SceneManager.Game.Exit();
        }
        private void CreditMenuItemSelected(object sender, EventArgs e)
        {
            new CreditScene(SceneManager, "Credits").Add();
        }

        public override void HandleInput()
        {
            if (InputState.IsPressedOnce(InputActions.Cancel))
            {
                this.Remove();
            }

            base.HandleInput();
        }
    }
}
