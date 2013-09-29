using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameGenerics.Graphics;
using GameData;

namespace ProjektVenus
{
    public class Level : DrawableGameComponent
    {
        #region Fields
        protected Player player;
        protected int difficulty;

        protected List<EnemyBase> enemyPool = new List<EnemyBase>();
        protected BulletManager bulletPool;
        protected BulletManager playerBulletPool;

        protected List<LevelActionBase> walkthrough = new List<LevelActionBase>();
        protected int currentAction;

        protected ParallaxSideScroller scroller;
        protected Vector2 scrollingVelocity;
        //private SoundEffectsPool soundManager;

        public SpriteBatch spriteBatch;
        private SamplerState s;
        #endregion


        #region Properties
        public List<EnemyBase> EnemyPool
        {
            get { return this.enemyPool; }
        }

        public int Difficulty
        {
            get { return this.difficulty; }
            set { this.difficulty = value; }
        }
        #endregion


        #region Constructors
        public Level(Game game) : base(game) {}
        #endregion


        #region Initialize
        public override void Initialize()
        {
            this.spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            this.s = new SamplerState();
            this.s.AddressU = TextureAddressMode.Wrap;
            this.s.AddressV = TextureAddressMode.Wrap;
            this.s.Filter = TextureFilter.Point;

            this.player = new Player(this.Game);
            this.player.Initialize();
            this.player.Position = new Vector2(200, 400);
            this.player.EventDeadCharacter += OnPlayerDeath;


            this.currentAction = 0;
            this.bulletPool = new BulletManager(this.Game);
            this.playerBulletPool = new BulletManager(this.Game);

            this.scroller = new ParallaxSideScroller(this.Game, false, false);
            this.scroller.Initialize();
            this.scrollingVelocity = new Vector2(0, 50f);

            foreach(LevelActionBase action in this.walkthrough)
            {
                action.Initialize();
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.scroller.Add("layer2");
            this.scroller.Add("fond_lambda");
            base.LoadContent();
        }
        #endregion


        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.scroller.MoveBy(scrollingVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.UpdateWalkthrought(gameTime);

            foreach (EnemyBase e in this.EnemyPool)
            {
                
                e.CollidesWith(this.player);
                e.Update(gameTime);
            }
            this.ComputeCollisionEnemysBetweenBullets(gameTime);
            foreach (Bullet b in this.bulletPool)
            {
                this.player.CollidesWith(b);
            }
            this.player.Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.scroller.Draw(gameTime);
            this.player.Draw(gameTime);
            foreach (EnemyBase e in this.enemyPool)
                e.Draw(gameTime);

            this.DrawWalkthrough(gameTime);
            base.Draw(gameTime);
        }
        #endregion


        #region Methods
        private void ComputeCollisionEnemysBetweenBullets(GameTime gameTime)
        {
            List<EnemyBase> enemyToUpdate = new List<EnemyBase>();

            foreach (EnemyBase e in this.enemyPool)
            {
                e.Update(gameTime);
                foreach (Bullet b in this.player.BulletManager)
                {
                    e.CollidesWith(b); 
                }
                if (e.Health > 0)
                {
                    enemyToUpdate.Add(e);
                }
            }

            this.enemyPool.Clear();
            foreach (EnemyBase e in enemyToUpdate)
            {
                this.enemyPool.Add(e);
            }
            enemyToUpdate.Clear();
        }

        private void UpdateWalkthrought(GameTime gameTime)
        {
            if (this.currentAction <= this.walkthrough.Count - 1)
            {
                this.walkthrough[this.currentAction].Update(gameTime);
                // Verification should be done before update ***************************************************************************
                if (this.walkthrough[this.currentAction].IsFinished) this.currentAction++;
            }
        }

        private void DrawWalkthrough(GameTime gameTime)
        {
            if (this.currentAction <= this.walkthrough.Count - 1)
            {
                this.walkthrough[this.currentAction].Draw(gameTime);
            }
        }
        
        public static Level Build(Game game, LevelDesc desc)
        {
            Level level = new Level(game);

            foreach (LevelActionDesc actionDesc in desc.Walkthrough)
            {
                LevelActionBase action = (LevelActionBase)actionDesc.BuildConcreteObject(game, level);
                level.walkthrough.Add(action);
            }
            return level;
        }

        private void OnPlayerDeath(Object sender, EventArgs args)
        {
            Console.WriteLine(sender + " Mort");
        }
        #endregion
    }
}
