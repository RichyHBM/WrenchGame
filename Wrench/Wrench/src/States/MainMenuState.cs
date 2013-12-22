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
        string difficultyString;

        enum Options
        {
            Play,
            Quit
        }

        public MainMenuState(Game game)
            : base(game)
        {
            switch (GlobalSettings.Difficulty)
            {
                case GlobalSettings.DifficultyEnum.Easy:
                    difficultyString = "Easy >";
                    break;
                case GlobalSettings.DifficultyEnum.Medium:
                    difficultyString = "< Medium >";
                    break;
                case GlobalSettings.DifficultyEnum.Hard:
                    difficultyString = "< Hard";
                    break;
            }
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
            if (Manager.InputManager.HasBeenPressed(Keys.Enter) || Manager.InputManager.HasBeenPressed(Buttons.A))
            {
                switch(currentChoice)
                {
                    case Options.Play:
                        Manager.StateManager.PushState(new LevelSelectionState(Game));
                        break;
                    case Options.Quit:
                        Game.Exit();
                        break;
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Up) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickUp))
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

            if (Manager.InputManager.HasBeenPressed(Keys.Down) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickDown))
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

            if (Manager.InputManager.HasBeenPressed(Keys.Left) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickLeft))
            {
                if (currentChoice == Options.Play)
                {
                    switch (GlobalSettings.Difficulty)
                    { 
                        case GlobalSettings.DifficultyEnum.Medium:
                            GlobalSettings.Difficulty = GlobalSettings.DifficultyEnum.Easy;
                            difficultyString = "Easy >";
                            break;
                        case GlobalSettings.DifficultyEnum.Hard:
                            GlobalSettings.Difficulty = GlobalSettings.DifficultyEnum.Medium;
                            difficultyString = "< Medium >";
                            break;
                    }
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Right) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickRight))
            {
                if (currentChoice == Options.Play)
                {
                    switch (GlobalSettings.Difficulty)
                    {
                        case GlobalSettings.DifficultyEnum.Easy:
                            GlobalSettings.Difficulty = GlobalSettings.DifficultyEnum.Medium;
                            difficultyString = "< Medium >";
                            break;
                        case GlobalSettings.DifficultyEnum.Medium:
                            GlobalSettings.Difficulty = GlobalSettings.DifficultyEnum.Hard;
                            difficultyString = "< Hard";
                            break;
                    }
                }
            }
                
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backdrop, Game.GraphicsDevice.Viewport.Bounds, Color.Black);
            //Font has weird top spacing, so draw at -50
            spriteBatch.DrawString(titleFont, "Wrench", new Vector2(10, -50), Color.White);

            if (currentChoice == Options.Play)
            {
                spriteBatch.DrawString(optionsFont, "Play  " + difficultyString, new Vector2(10, 300), Color.Gold);
                spriteBatch.DrawString(optionsFont, "Quit", new Vector2(10, 410), Color.White);
            }
            else if (currentChoice == Options.Quit)
            {
                spriteBatch.DrawString(optionsFont, "Play", new Vector2(10, 300), Color.White);
                spriteBatch.DrawString(optionsFont, "Quit", new Vector2(10, 410), Color.Gold);
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
