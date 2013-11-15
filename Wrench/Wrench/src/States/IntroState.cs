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
    public class IntroState : AState
    {
        Texture2D background;
        public IntroState(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Manager.InputManager.HasBeenPressed(Keys.Enter))
                Manager.StateManager.RemoveState(this);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, Game.GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Resume()
        {

        }

        public override void Pause()
        {

        }

        public override void Start()
        {
            background = Game.Content.Load<Texture2D>("Textures/Intro");
        }

        public override void Stop()
        {

        }
    }
}