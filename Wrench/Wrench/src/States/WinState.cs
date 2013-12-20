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
using Wrench.src.Managers;
using Wrench.src.BaseClasses;


namespace Wrench.src.States
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class WinState : AState
    {
        SpriteFont titleFont;
        SpriteFont optionsFont;

        public WinState(Game game)
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
            if (Manager.InputManager.HasBeenPressed(Keys.Escape) || Manager.InputManager.HasBeenPressed(Buttons.Y))
                Game.Exit();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Vector2 center = new Vector2(Game.GraphicsDevice.Viewport.Bounds.Center.X,
                                         Game.GraphicsDevice.Viewport.Bounds.Center.Y);

            spriteBatch.DrawString(titleFont, "You Win", center - (Vector2.UnitY * 50), Color.White, 0.0f, titleFont.MeasureString("You Win") / 2.0f, 1.0f, SpriteEffects.None, 1.0f);
            spriteBatch.DrawString(optionsFont, "Press Escape to quit", center + (Vector2.UnitY * 100), Color.White, 0.0f, optionsFont.MeasureString("Press Escape to quit") / 2.0f, 0.5f, SpriteEffects.None, 1.0f);

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
