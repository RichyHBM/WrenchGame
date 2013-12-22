using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrench.src.BaseClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Wrench.src.Managers;
using Microsoft.Xna.Framework.Input;

namespace Wrench.src.States
{
    public class PauseState : AState
    {
        SpriteFont titleFont;
        SpriteFont optionsFont;
        Options currentChoice = Options.Resume;
        GamePlayState gameState;

        enum Options
        {
            Resume,
            QuitToMenu
        }

        public PauseState(Game game, GamePlayState gameState)
            : base(game)
        {
            this.gameState = gameState;
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
                    case Options.Resume:
                        Manager.StateManager.RemoveState(this);
                        break;
                    case Options.QuitToMenu:
                        Manager.StateManager.RemoveState(gameState);
                        Manager.StateManager.RemoveState(this);
                        break;
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Up) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickUp))
            {
                switch (currentChoice)
                {
                    case Options.Resume:
                        currentChoice = Options.QuitToMenu;
                        break;
                    case Options.QuitToMenu:
                        currentChoice = Options.Resume;
                        break;
                }
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Down) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickDown))
            {
                switch (currentChoice)
                {
                    case Options.Resume:
                        currentChoice = Options.QuitToMenu;
                        break;
                    case Options.QuitToMenu:
                        currentChoice = Options.Resume;
                        break;
                }
            }
                
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backdrop, Game.GraphicsDevice.Viewport.Bounds, new Color(255, 255, 255, 196));
            //Font has weird top spacing, so draw at -50
            spriteBatch.DrawString(titleFont, "Wrench", new Vector2(10, -50), Color.White);

            if (currentChoice == Options.Resume)
            {
                spriteBatch.DrawString(optionsFont, "Resume", new Vector2(10, 300), Color.Gold);
                spriteBatch.DrawString(optionsFont, "Quit to menu", new Vector2(10, 410), Color.White);
            }
            else if (currentChoice == Options.QuitToMenu)
            {
                spriteBatch.DrawString(optionsFont, "Resume", new Vector2(10, 300), Color.White);
                spriteBatch.DrawString(optionsFont, "Quit to menu", new Vector2(10, 410), Color.Gold);
            }
            else{
                spriteBatch.DrawString(optionsFont, "Resume", new Vector2(10, 300), Color.White);
                spriteBatch.DrawString(optionsFont, "Quit to menu", new Vector2(10, 410), Color.White);
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
