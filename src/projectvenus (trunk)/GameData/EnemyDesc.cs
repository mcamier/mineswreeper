using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameData
{
    [Serializable]
    public abstract class EnemyBaseDesc
    {
        public Vector2 SpawnLocation;

        public EnemyBaseDesc() { }

        public abstract Object BuildConcreteObject(Object game);
    }

    [Serializable]
    public class EnemyGroupDesc : EnemyBaseDesc
    {
        public List<EnemyBaseDesc> Entities = new List<EnemyBaseDesc>();

        public EnemyGroupDesc() { }

        public override Object BuildConcreteObject(Object game)
        {
            Object[] enemyList = new Object[this.Entities.Count];
            for (int i = 0; i < this.Entities.Count; i++)
            {
                enemyList[i] = this.Entities[i].BuildConcreteObject(game);
            }
            return Activator.CreateInstance(Type.GetType("ProjektVenus.EnemyGroup,ProjektVenus"), game);
        }
    }

    [Serializable]
    public class EnemyDesc : EnemyBaseDesc
    {
        public String EnemyType;

        public EnemyDesc() { }

        public override Object BuildConcreteObject(Object game)
        {
            return Activator.CreateInstance(Type.GetType("ProjektVenus." + EnemyType + ",ProjektVenus"), game);
        }
    }
}
