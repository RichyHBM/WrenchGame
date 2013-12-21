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
using Wrench.src.Helpers;
using Wrench.src.GameLevelItems;


namespace Wrench.src.States
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GamePlayState : AState
    {
        GameLevel level;

        public GamePlayState(Game game)
            : base(game)
        {
            level = new GameLevel(game);
            level.Initialize();
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
            if (Manager.InputManager.HasBeenPressed(Keys.Escape))
                Manager.StateManager.PushState(new PauseState(Game, this));

            level.Update(gameTime);

            Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backdrop, Game.GraphicsDevice.Viewport.Bounds, Color.Black);
            spriteBatch.End();
            Game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Game.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            level.Draw(gameTime);
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
        }

        public override void Stop()
        {

        }
    }
}
