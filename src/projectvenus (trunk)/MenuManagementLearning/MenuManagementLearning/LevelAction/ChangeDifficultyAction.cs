namespace ProjektVenus
{
    class ChangeDifficultyAction : LevelActionBase
    {
        private int difficulty;

        public ChangeDifficultyAction(Level parent, int difficulty)
            : base(parent)
        {
            this.difficulty = difficulty;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.level.Difficulty = this.difficulty;
            this.finished = true;
        }
    }
}
