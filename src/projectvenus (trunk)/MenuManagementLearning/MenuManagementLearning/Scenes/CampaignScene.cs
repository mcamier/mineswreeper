using System;
using System.Collections.Generic;
using GameGenerics.Input;
using GameGenerics.Scenes.Core;
using GameData;

namespace ProjektVenus
{
    class CampaignScene : AbstractMenuScene
    {
        #region Fields
        public List<LevelDesc> levels = new List<LevelDesc>();
        #endregion

        #region Constructors
        public CampaignScene(SceneManager sceneManager, String title)
            :base(sceneManager, title) { }
        #endregion

        protected override void LoadContent()
        {
            levels = Content.Load<List<LevelDesc>>("levels");

            foreach (LevelDesc level in this.levels)
            {
                TextMenuItem lvl;
                if (level.Hidden) 
                {
                    string hiddenName = "";
                    for(int i = 0 ; i < level.Name.Length ; i++) hiddenName += '?';
                    lvl = new TextMenuItem(this, hiddenName);
                }
                else
                {
                    lvl = new TextMenuItem(this, level.Name);
                }
                lvl.ItemSelected += LaunchLevel;
                MenuItems.Add(lvl);
            }
        
            base.LoadContent();
        }

        private void LaunchLevel(Object o, EventArgs sender) 
        {
            Level loadedLevel = Level.Build(this.Game, this.levels[0]);
            this.SceneManager.AddScene(new GameplayScene(this.SceneManager, loadedLevel));
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
