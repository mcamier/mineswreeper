using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace ProjektVenus
{
    public class EnemyVagueAction : LevelActionBase
    {
        #region Fields
        private List<EnemyBase> enemyList = new List<EnemyBase>();
        private bool enemyAdded;
        #endregion 


        #region Constructors
        // Tweak for auto use by Activator 
        public EnemyVagueAction(Level parent, List<Object> enemies)
            : base(parent)
        {
            //this.enemyList = enemies;
            for(int i = 0; i < enemies.Count; i++)
            {
                this.enemyList.Add((EnemyBase)enemies[i]);
            }
            this.enemyAdded = false;
        }

        public EnemyVagueAction(Level parent, List<EnemyBase> enemyList)
            : base(parent)
        {
            foreach (EnemyBase e in enemyList)
            {
                this.enemyList.Add(e);
            }
            this.enemyAdded = false;
        }
        #endregion



        public override void Update(GameTime gameTime)
        {
            if (!this.enemyAdded)
            {
                this.enemyAdded = true;
                for (int i = 0; i < this.enemyList.Count; i++)
                {
                    this.level.EnemyPool.Add(this.enemyList[i]);
                }
            }
            if(this.level.EnemyPool.Count == 0)
                this.finished = true;
        }

        public override void Initialize()
        {
            foreach (EnemyBase e in enemyList)
            {
                e.Initialize();
            }
            base.Initialize();
        }
    }
}
