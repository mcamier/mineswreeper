using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameGenerics.Input;

namespace GameGenerics.Scenes.Core
{
    public class AbstractMenuScene : AbstractGameScene
    {
        #region Fields
        private readonly string menuTitle;
        private readonly List<AbstractMenuItem> menuItems = new List<AbstractMenuItem>();
        private int selectedItemIndex;

        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        #endregion


        #region Properties
        public List<AbstractMenuItem> MenuItems
        {
            get { return this.menuItems; }
        }

        public String MenuTitle
        {
            get { return this.menuTitle; }
        }
        #endregion


        #region Constructors
        public AbstractMenuScene(SceneManager sceneManager, string menuTitle)
            : base(sceneManager)
        {
            this.menuTitle = menuTitle;
            this.AppearsDelay = TimeSpan.FromMilliseconds(500);
            this.DisappearsDelay = TimeSpan.FromMilliseconds(300);
            this.graphics = SceneManager.GraphicsDevice;
            this.spriteBatch = SceneManager.SpriteBatch;
            this.font = SceneManager.Font;
        }
        #endregion

        private void OnSelectItem(int itemIndex)
        {
            this.menuItems[itemIndex].OnSelected();
        }

        public override void  Update(GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {
 	        base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);

            if (this.IsActive)
            {
                for (int i = 0; i < this.menuItems.Count; i++)
                {
                    this.menuItems[i].Update((i == this.selectedItemIndex), gameTime);
                }
            }
        }

        private void UpdateMenuItemLocations()
        {
            var transitionOffset = TransitionPosition * TransitionPosition;
            Vector2 position = new Vector2(0f, 150f);

            foreach (AbstractMenuItem menuItem in this.menuItems)
            {
                position.X = SceneManager.GraphicsDevice.Viewport.Width / 2 - menuItem.Width / 2;
                menuItem.Position = position;
                position.Y += 1.0f * menuItem.Height;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            UpdateMenuItemLocations();

            spriteBatch.Begin();
            for (int i = 0; i < this.menuItems.Count; i++)
            {
                AbstractMenuItem menuItem = this.menuItems[i];
                bool isSelected = IsActive && (i == this.selectedItemIndex);
                menuItem.Draw(isSelected, gameTime);
            }

            float transitionOffset = TransitionPosition * TransitionPosition;
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2f, 80);
            Vector2 titleOrigin = font.MeasureString(this.menuTitle) / 2;
            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(
                font, 
                this.menuTitle,
                titlePosition, 
                new Color(192, 192, 192) * TransitionAlpha, 
                0,
                font.MeasureString(this.menuTitle) / 2,
                1, 
                SpriteEffects.None, 
                0);
            spriteBatch.End();
        }

        #region Methods
        private int NextMenuItem()
        {
            int index = (selectedItemIndex + 1 >= this.menuItems.Count) ? 0 : selectedItemIndex + 1;
            while (index != selectedItemIndex)
            {
                if (!this.menuItems[index].AllowFocus)
                {
                    if (index + 1 >= this.menuItems.Count)
                        index = 0;
                    else
                        index++;
                }
                else
                    return index;
            }
            return selectedItemIndex;
        }

        private int PreviousMenuItem()
        {
            int index = (selectedItemIndex - 1 >= 0) ? selectedItemIndex - 1 : this.menuItems.Count - 1;
            while (index != selectedItemIndex)
            {
                if (!this.menuItems[index].AllowFocus)
                {
                    if (index - 1 < 0)
                        index = this.menuItems.Count - 1;
                    else
                        index--;
                }
                else
                    return index;
            }
            return selectedItemIndex;
        }

        public override void HandleInput()
        {
            if (InputState.IsPressedOnce(InputActions.MoveUp))
            {
                selectedItemIndex = PreviousMenuItem();
            }
            if (InputState.IsPressedOnce(InputActions.MoveDown))
            {
                selectedItemIndex = NextMenuItem();
            }

            if (InputState.IsMenuSelect())
                OnSelectItem(this.selectedItemIndex);
            else if (InputState.IsMenuCancel())
                this.OnCancel();
        }
        protected void OnCancel(object sender, EventArgs e)
        {
            OnCancel();
        }

        protected virtual void OnCancel()
        {
            //do something
        }
        #endregion
    }
}
