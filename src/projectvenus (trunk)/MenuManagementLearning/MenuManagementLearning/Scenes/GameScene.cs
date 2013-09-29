using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GameGenerics.Scenes.Core;
using ProjektVenus;
using GameGenerics.Input;


namespace GameGenerics.Scenes
{
    class GameScene : AbstractGameScene
    {/*
        #region Fields
        SpriteBatch spriteBatch;
        Player player;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;
        float playerMoveSpeed;

        Texture2D mainBackground;
        ParallaxingBackground layer1Background;
        ParallaxingBackground layer2Background;

        Texture2D enemyTexture;
        List<Enemy> enemies;

        // The rate at which the enemies appear
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;

        // A random number generator
        Random random;

        Camera2d cam;

        Texture2D projectileTexture;
        List<Projectile> projectiles;

        // The rate of fire of the player laser
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        //Number that holds the player score
        int score;
        // The font used to display UI elements
        SpriteFont font;
        #endregion

        #region Constructor
        public GameScene(SceneManager sceneManager) : base(sceneManager) 
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Initialize and LoadContent
        public override void Initialize()
        {

            player = new Player();
            playerMoveSpeed = 6f;
            layer1Background = new ParallaxingBackground();
            layer2Background = new ParallaxingBackground();
            base.Initialize();

            // Initialize the enemies list
            enemies = new List<Enemy>();

            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator
            random = new Random();
            projectiles = new List<Projectile>();

            // Set the laser to fire every quarter second
            fireTime = TimeSpan.FromSeconds(.15f);
            score = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AnimationOld playerAnimation = new AnimationOld();
            Texture2D playerTexture = Content.Load<Texture2D>("Animations/shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 25, Color.White, 1f, true);
            //playerAnimation.Initialize(playerTexture, Vector2.Zero, 58, 38, 12, 25, Color.White, 1f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            player.Initialize(playerAnimation, playerPosition);

            layer1Background.Initialize(Content, "bgLayer1", GraphicsDevice.Viewport.Width, -1);
            layer2Background.Initialize(Content, "bgLayer2", GraphicsDevice.Viewport.Width, -2);
            mainBackground = Content.Load<Texture2D>("mainbackground");

            enemyTexture = Content.Load<Texture2D>("Animations/mineAnimation");
            cam = new Camera2d(SceneManager.Game);
            projectileTexture = Content.Load<Texture2D>("laser");
            font = Content.Load<SpriteFont>("Fonts/gameFont");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        #endregion

        #region Update And Draw
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherSceneHasFocus, bool coveredByOtherScene)
        {

            if (!this.otherSceneHasFocus)
            {
                previousGamePadState = currentGamePadState;
                previousKeyboardState = currentKeyboardState;

                currentKeyboardState = Keyboard.GetState();
                currentGamePadState = GamePad.GetState(PlayerIndex.One);

                player.Update(gameTime);
                this.UpdatePlayer(gameTime);
                layer1Background.Update();
                layer2Background.Update();
                UpdateEnemies(gameTime);
                UpdateCollision();
                UpdateProjectiles();

                if (currentKeyboardState.IsKeyDown(Keys.S))
                    cam.Position += new Vector2(0, 4);
                if (currentKeyboardState.IsKeyDown(Keys.Z))
                    cam.Position += new Vector2(0, -4);
                if (currentKeyboardState.IsKeyDown(Keys.Q))
                    cam.Position += new Vector2(-4, 0);
                if (currentKeyboardState.IsKeyDown(Keys.D))
                    cam.Position += new Vector2(4, 0);

                if (currentKeyboardState.IsKeyDown(Keys.A))
                    cam.Rotation += 0.1f;
                if (currentKeyboardState.IsKeyDown(Keys.E))
                    cam.Rotation -= 0.1f;
                if (currentKeyboardState.IsKeyDown(Keys.L))
                    cam.Zoom += 0.01f;
                if (currentKeyboardState.IsKeyDown(Keys.M))
                    cam.Zoom -= 0.01f;
            }
            if (gameTime.TotalGameTime - previousFireTime > fireTime)
            {
                // Reset our current time
                previousFireTime = gameTime.TotalGameTime;

                // Add the projectile, but add it to the front and center of the player
                AddProjectile(player.Position + new Vector2(player.Width / 2, 0));
            }
            base.Update(gameTime, otherSceneHasFocus, coveredByOtherScene);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend, 
                        SamplerState.PointClamp,
                        null,
                        null,
                        null,
                        cam.getTransformation(this.GraphicsDevice));
           
            spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            layer1Background.Draw(spriteBatch);
            layer2Background.Draw(spriteBatch);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
             
            spriteBatch.Draw(textureViewport, new Rectangle(0, 0, SceneManager.GraphicsDevice.Viewport.Bounds.Width, SceneManager.GraphicsDevice.Viewport.Bounds.Height), Color.White);
            spriteBatch.End();


            spriteBatch.Begin(); 
            spriteBatch.DrawString(font, "score: " + score, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
            spriteBatch.DrawString(font, "health: " + player.Health, new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);
            spriteBatch.End();
          
            
            Viewport original = SceneManager.GraphicsDevice.Viewport;
            SceneManager.GraphicsDevice.Viewport = this.viewportGameplay;
            spriteBatch.Begin();
            //spriteBatch.Draw(textureViewport, Vector2.Zero, Color.White);
            spriteBatch.Draw(textureViewport, new Rectangle(0, 0, 400, 400), Color.White);
            spriteBatch.End();
            SceneManager.GraphicsDevice.Viewport = original;
        
              
            base.Draw(gameTime);
        }
        #endregion

        #region Methods
        private void AddProjectile(Vector2 position)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(GraphicsDevice.Viewport, projectileTexture, position);
            projectiles.Add(projectile);
        }

        private void AddEnemy()
        {
            // Create the animation object
            AnimationOld enemyAnimation = new AnimationOld();

            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);


            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // Create an enemy
            Enemy enemy = new Enemy();

            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list
            enemies.Add(enemy);
        }

        private void UpdateCollision()
        {
            Rectangle rectangle1;
            Rectangle rectangle2;

            rectangle1 = new Rectangle((int)this.player.Position.X, (int)this.player.Position.Y, this.player.PlayerAnimation.FrameWidth, this.player.PlayerAnimation.FrameHeight);
            for (int i = 0; i < enemies.Count; i++)
            {
                rectangle2 = new Rectangle((int)this.enemies[i].Position.X, (int)this.enemies[i].Position.Y, this.enemies[i].Width, this.enemies[i].Height);
                if (rectangle1.Intersects(rectangle2))
                {
                    player.Health -= enemies[i].Damage;
                    enemies[i].Health = 0;

                    if (player.Health <= 0)
                        player.Active = false;
                }
            }
            // Projectile vs Enemy Collision
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other
                    rectangle1 = new Rectangle((int)projectiles[i].Position.X -
                    projectiles[i].Width / 2, (int)projectiles[i].Position.Y -
                    projectiles[i].Height / 2, projectiles[i].Width, projectiles[i].Height);

                    rectangle2 = new Rectangle((int)enemies[j].Position.X - enemies[j].Width / 2,
                    (int)enemies[j].Position.Y - enemies[j].Height / 2,
                    enemies[j].Width, enemies[j].Height);

                    // Determine if the two objects collided with each other
                    if (rectangle1.Intersects(rectangle2))
                    {
                        enemies[j].Health -= projectiles[i].Damage;
                        projectiles[i].Active = false;
                    }
                }
            }
        }

        private void UpdateProjectiles()
        {
            // Update the Projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].Active == false)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            // Spawn a new enemy enemy every 1.5 seconds
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                // Add an Enemy
                AddEnemy();
            }

            // Update the Enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                if (enemies[i].Active == false)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (InputState.IsPauseGame())
            {
                new PauseGameMenu(this.SceneManager).Add();
            }
        }
        #endregion
*/
    }
}