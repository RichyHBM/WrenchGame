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
    public class LevelSelectionState : AState
    {
        SpriteFont titleFont;
        SpriteFont optionsFont;
        int selectedLevel = 0;
        List<String> levelNames;

        public LevelSelectionState(Game game)
            : base(game)
        {
            levelNames = ContentPreImporter.LevelNames();
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
            if (Manager.InputManager.HasBeenPressed(Keys.Escape) || Manager.InputManager.HasBeenPressed(Buttons.Y))
            {
                //remove this
                Manager.StateManager.RemoveState(this);
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Enter) || Manager.InputManager.HasBeenPressed(Buttons.A))
            {
                //add gameplay state with chosen level
                //remove this
                Manager.StateManager.PushState(new GamePlayState(Game, levelNames[selectedLevel]));
                Manager.StateManager.RemoveState(this);
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Up) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickUp))
            {
                if (selectedLevel > 0) selectedLevel--;
            }

            if (Manager.InputManager.HasBeenPressed(Keys.Down) || Manager.InputManager.HasBeenPressed(Buttons.LeftThumbstickDown))
            {
                if (selectedLevel < levelNames.Count-1) selectedLevel++;
            }
                
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backdrop, Game.GraphicsDevice.Viewport.Bounds, Color.Black);
            //Font has weird top spacing, so draw at -50
            spriteBatch.DrawString(titleFont, "Levels", new Vector2(10, -50), Color.White);

            if (selectedLevel > 0)
                spriteBatch.DrawString(optionsFont, levelNames[selectedLevel - 1], new Vector2(10, 190), Color.White);
            
            spriteBatch.DrawString(optionsFont, "> " + levelNames[selectedLevel], new Vector2(10, 300), Color.Gold);
            
            if (selectedLevel < levelNames.Count - 1)
                spriteBatch.DrawString(optionsFont, levelNames[selectedLevel + 1], new Vector2(10, 410), Color.White);

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
