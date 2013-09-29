using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Content;

namespace GameData
{
    [Serializable]
    public abstract class LevelActionDesc
    {
        public LevelActionDesc() { }

        public abstract Object BuildConcreteObject(Object game, Object level);
        
    }

    [Serializable]
    public class EnemyVagueActionDesc : LevelActionDesc
    {
        [ContentSerializer(SharedResource=true)]
        public List<EnemyBaseDesc> Enemies;

        public EnemyVagueActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            List<Object> enemyList = new List<Object>();
            foreach(EnemyBaseDesc e in this.Enemies)
            {
                enemyList.Add(e.BuildConcreteObject(game));
            }

            return Activator.CreateInstance(Type.GetType("ProjektVenus.EnemyVagueAction,ProjektVenus"), level, enemyList); ;
        }
    }

    [Serializable]
    public class WaitActionDesc : LevelActionDesc
    {
        public TimeSpan Duration;

        public WaitActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            return Activator.CreateInstance(Type.GetType("ProjektVenus.WaitAction,ProjektVenus"), level, this.Duration); ;
        }
    }

    [Serializable]
    public class ConversationActionDesc : LevelActionDesc
    {
        public String FilePath;

        public ConversationActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            Object conversation = Activator.CreateInstance(Type.GetType("ProjektVenus.Conversation,ProjektVenus"), game, this.FilePath);
            return Activator.CreateInstance(Type.GetType("ProjektVenus.ConversationAction,ProjektVenus"), level, conversation); ;
        }
    }

    [Serializable]
    public class ChangeMusicActionDesc : LevelActionDesc
    {
        public ChangeMusicActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            return Activator.CreateInstance(Type.GetType("ProjektVenus.ChangeMusicAction,ProjektVenus"), level); ;
        }
    }

    [Serializable]
    public class ChangeDifficultyActionDesc : LevelActionDesc
    {

        public ChangeDifficultyActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            return Activator.CreateInstance(Type.GetType("ProjektVenus.ChangeDifficultyAction,ProjektVenus"), level); ;
        }
    }

    [Serializable]
    public class ChangeBackgroundActionDesc : LevelActionDesc
    {

        public ChangeBackgroundActionDesc() {}

        public override Object BuildConcreteObject(Object game, Object level)
        {
            return Activator.CreateInstance(Type.GetType("ProjektVenus.ChangeBackgroundAction,ProjektVenus"), level); ;
        }
    }
}
