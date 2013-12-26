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


namespace Wrench.src.BaseClasses
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary> 
    //Any state the game can be in
    public abstract class AState : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected Texture2D backdrop;

        public AState(Game game)
            : base(game)
        {
            //Blank texture
            backdrop = ContentPreImporter.GetTexture("backDrop");
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

        public virtual void Resume() { }
        public virtual void Pause() { }
        public virtual void Start() { }
        public virtual void Stop() { }

    }
}
