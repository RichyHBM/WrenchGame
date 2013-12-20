using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Wrench.src.BaseClasses;
using Wrench.src.Managers;


namespace Wrench.src.States
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainMenuState : AState
    {
        SpriteFont titleFont;
        SpriteFont optionsFont;
        Options currentChoice = Options.Play;

        enum Options
        {
            Play,
            Quit
        }

        public MainMenuState(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Manager.InputManager.HasBeenPressed(Keys.Enter))
            {
                switch(currentChoice)
                {
                    case Options.Play:
                        Manager.StateManager.RemoveState(this);
                        break;
                    case Options.Quit:
                        Game.Exit();
                        break;
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Up))
            {
                switch (currentChoice)
                {
                    case Options.Play:
                        currentChoice = Options.Quit;
                        break;
                    case Options.Quit:
                        currentChoice = Options.Play;
                        break;
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Down))
            {
                switch (currentChoice)
                {
                    case Options.Play:
                        currentChoice = Options.Quit;
                        break;
                    case Options.Quit:
                        currentChoice = Options.Play;
                        break;
                }
            }
                
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(titleFont, "Wrench", new Vector2(10, 10), Color.White);

            if (currentChoice == Options.Play)
            {
                spriteBatch.DrawString(optionsFont, "Play <<<", new Vector2(10, 300), Color.Gold);
                spriteBatch.DrawString(optionsFont, "Quit", new Vector2(10, 410), Color.White);
            }
            else if (currentChoice == Options.Quit)
            {
                spriteBatch.DrawString(optionsFont, "Play", new Vector2(10, 300), Color.White);
                spriteBatch.DrawString(optionsFont, "Quit <<<", new Vector2(10, 410), Color.Gold);
            }
            else{
                spriteBatch.DrawString(optionsFont, "Play", new Vector2(10, 300), Color.White);
                spriteBatch.DrawString(optionsFont, "Quit", new Vector2(10, 410), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Start()
        {
            titleFont = ContentPreImporter.GetFont("LargeFont");
            optionsFont = ContentPreImporter.GetFont("MediumFont");
        }

    }
}
