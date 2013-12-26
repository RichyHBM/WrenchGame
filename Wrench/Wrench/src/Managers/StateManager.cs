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


namespace Wrench.src.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    // Manages all the different states the game can be in
    public class StateManager : IManager
    {
        List<AState> states = new List<AState>();
        protected Game game;

        //Add a new state and use that one from now on
        public void PushState(AState state)
        {
            if (states.Count != 0)
                states.Last().Pause();

            state.Initialize();
            state.Start();
            state.Resume();

            states.Add(state);
        }
        //Removes the last state
        public void RemoveState()
        {
            RemoveState(states.Last());
        }

        public void RemoveState(AState state)
        {
            state.Pause();
            state.Stop();
            states.Remove(state);
            if (states.Count == 0)
                game.Exit();
            states.Last().Resume();
        }

        public void Initialize(Game game)
        {
            this.game = game;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            states.Last().Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (AState state in states)
            {
                game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

                state.Draw(gameTime);
            }
        }
    }
}

