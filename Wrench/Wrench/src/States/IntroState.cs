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
        public IntroState(Game game)
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
                Manager.StateManager.RemoveState(this);
            base.Update(gameTime);
        }

        public override void Resume()
        {
            System.Console.WriteLine("Intro Resume");
        }

        public override void Pause()
        {
            System.Console.WriteLine("Intro Paused");
        }

        public override void Start()
        {

        }

        public override void Stop()
        {

        }
    }
}
