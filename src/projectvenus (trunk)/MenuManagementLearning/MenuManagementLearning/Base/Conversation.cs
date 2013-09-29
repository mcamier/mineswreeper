using System;
using System.Collections.Generic;
using GameData;
using GameGenerics.Input;
using GameGenerics.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjektVenus
{
    public class Conversation : DrawableGameComponent
    {
        #region Fields
        private SpriteBatch spriteBatch;
        private List<Speaker> listSpeakers = new List<Speaker>();
        private String conversationPath;
        private int currentSpeakerIndex = 0;
        private readonly string avatarsFilePath;
        private Dictionary<string,Texture2D> avatars = new Dictionary<string,Texture2D>();

        private Rectangle globalBoxLocation;
        private Rectangle avatarBoxLocation; // Relative to the GlobalBoxLocation
        private Rectangle messageBoxLocation; // Relative to the GlobalBoxLocation
        private SpriteFont font;
        private Color fontColor;
        private String backgroundBoxPath = null;
        private Texture2D textureBackgroundBox;
        float delayLetter = 30;
        Timer timer;
        int currentLetter = 0;

        private string FormattedCurrentMessage;
        private bool messageDrawn = false;
        private bool messageBoxDrawn = true;
        private bool done = false;
        #endregion


        #region Properties
        public Rectangle GlobalBoxLocation
        {
            get { return this.globalBoxLocation; }
            set { this.globalBoxLocation = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(this.globalBoxLocation.X, this.globalBoxLocation.Y); }
            set
            {
                this.globalBoxLocation.X = (int)value.X;
                this.globalBoxLocation.Y = (int)value.Y; 
            }
        }

        public int Width
        {
            get { return this.GlobalBoxLocation.Width; }
        }

        public int Height
        {
            get { return this.GlobalBoxLocation.Height; }
        }

        public bool Done
        {
            get { return this.done; }
        }
        #endregion


        #region Constructors
        public Conversation(Game game, String conversationPath) : base(game)
        {
            this.fontColor = Color.White;
            this.conversationPath = conversationPath;
            this.globalBoxLocation = new Rectangle(0, 0, 500, 120);
            this.avatarBoxLocation = new Rectangle(10, 10, 100, 100);
            this.messageBoxLocation = new Rectangle(120, 10, 360, 90);
            this.avatarsFilePath = "";
        }
        #endregion


        #region Initialize & LoadContent
        public override void Initialize()
        {
            this.timer = new Timer(TimeSpan.FromMilliseconds(delayLetter), UpdateString, true);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.listSpeakers = this.Game.Content.Load<List<Speaker>>(this.conversationPath);
            font = this.Game.Content.Load<SpriteFont>("Fonts/debug");
            if (this.backgroundBoxPath == null)
            {
                this.textureBackgroundBox = new Texture2D(this.Game.GraphicsDevice, this.globalBoxLocation.Width, this.globalBoxLocation.Height);
                Color[] data = new Color[this.globalBoxLocation.Width*this.globalBoxLocation.Height];
                for (int i = 0; i < data.Length; i++) { data[i] = Color.RoyalBlue * 0.5f; }
                this.textureBackgroundBox.SetData(data);
            }
            foreach (Speaker s in listSpeakers)
            {
                if(!this.avatars.ContainsKey(s.avatarIndex.ToString()))
                    this.avatars.Add(s.avatarIndex.ToString(), this.Game.Content.Load<Texture2D>(this.avatarsFilePath + s.avatarIndex));
            }
            this.FormattedCurrentMessage = FormatStringToMessageBox(this.listSpeakers[currentSpeakerIndex].message);
            base.LoadContent();
        }
        #endregion


        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            if (this.timer != null)
                this.timer.Update(gameTime);

            if (!this.messageBoxDrawn)
            { 
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            String text = this.FormattedCurrentMessage.Substring(0, currentLetter);
            Texture2D tempAvatar;
            this.avatars.TryGetValue(this.listSpeakers[currentSpeakerIndex].avatarIndex.ToString(), out tempAvatar);

            this.spriteBatch.Begin();
            if(messageBoxDrawn)
            
            this.spriteBatch.Draw(
                this.textureBackgroundBox, 
                this.globalBoxLocation, 
                Color.White);
            this.spriteBatch.Draw(
                tempAvatar,
                new Rectangle(this.globalBoxLocation.X + this.avatarBoxLocation.X, this.globalBoxLocation.Y + this.avatarBoxLocation.Y, this.avatarBoxLocation.Width, this.avatarBoxLocation.Height),
                null,
                Color.White*0.7f);

            this.spriteBatch.DrawString(
                this.font, 
                text, 
                new Vector2(this.globalBoxLocation.X + this.messageBoxLocation.X, this.globalBoxLocation.Y + this.messageBoxLocation.Y), 
                this.fontColor);
            // test if must draw the next icon
            if (messageDrawn)
            {
                if (this.currentSpeakerIndex + 1 < this.listSpeakers.Count)
                {
                    this.spriteBatch.DrawString(
                        this.font,
                        "Next !",
                        new Vector2(this.Position.X + this.globalBoxLocation.Width - font.MeasureString("Next !").X / 2 - 10, this.Position.Y + this.globalBoxLocation.Height - font.MeasureString("Next !").Y / 2),
                        Color.Black,
                        0f,
                        new Vector2(font.MeasureString("Next !").X / 2, font.MeasureString("Next !").Y / 2),
                        1f,
                        SpriteEffects.None,
                        1);
                }
                else
                {
                    this.spriteBatch.DrawString(
                        this.font,
                        "Close",
                        new Vector2(this.Position.X + this.messageBoxLocation.Width, this.Position.Y + this.messageBoxLocation.Height),
                        Color.Black);
                }
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion


        #region Methods
        private void UpdateString(Object sender, EventArgs e)
        {
            currentLetter++;
            if (currentLetter >= this.FormattedCurrentMessage.Length)
            {
                this.timer.Stop();
                this.messageDrawn = true;
            }
        }

        private string FormatStringToMessageBox(string inputString)
        { 
            string outputString = "";
            string[] words = inputString.Split(' ');
            string ligne = "";
            bool filled = false;

            foreach(string word in words) 
            {
                if (this.font.MeasureString(ligne + word).X > this.messageBoxLocation.Width)
                {
                    if (this.font.MeasureString(outputString + ligne).Y < this.messageBoxLocation.Height)
                    {
                        outputString += ligne + "\n";
                        ligne = "";
                    }
                    else if (!filled)
                    {
                        filled = true;
                        outputString += ligne;
                        ligne = "";
                    }
                }
                ligne += word + " ";
            }
            if (filled)
            {
                this.listSpeakers.Insert(currentSpeakerIndex + 1, new Speaker(this.listSpeakers[currentSpeakerIndex].avatarIndex, ligne));
                return outputString;
            }
            else
                return outputString + ligne;
        }

        public void HandleInput()
        {
            if (InputState.IsPressedOnce(InputActions.NextSpeaker))
            {
                if (this.messageDrawn)
                {
                    if (this.currentSpeakerIndex + 1 < this.listSpeakers.Count)
                    {
                        currentLetter = 0;
                        this.FormattedCurrentMessage = this.FormatStringToMessageBox(this.listSpeakers[++currentSpeakerIndex].message);
                        this.timer.Reset();
                        this.messageDrawn = false;
                        this.timer.Start();
                    }
                    else
                    {
                        //no more speaker, then close conversation
                        this.done = true;
                    }
                }
            }
        }
        #endregion
    }
}
