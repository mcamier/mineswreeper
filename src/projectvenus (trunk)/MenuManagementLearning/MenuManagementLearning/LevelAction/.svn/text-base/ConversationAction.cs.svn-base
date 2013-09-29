
namespace ProjektVenus
{
    public class ConversationAction : LevelActionBase
    {
        private Conversation conversation;

        public ConversationAction(Level parent, Conversation item)
            : base(parent)
        {
            this.conversation = item;
            this.conversation.Initialize();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.conversation.HandleInput();
            if (this.conversation.Done) this.finished = true;
            this.conversation.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.conversation.Draw(gameTime);
        }
    }
}
