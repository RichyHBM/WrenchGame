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


namespace Wrench.src.BaseClasses
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class AState : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        public AState(Game game)
            : base(game)
        {
            spriteBatch = (Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch);
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

        }

        public abstract void Resume();
        public abstract void Pause();
        public abstract void Start();
        public abstract void Stop();

    }
}
