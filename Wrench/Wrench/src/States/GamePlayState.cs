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


namespace Wrench.src.States
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GamePlayState : AState
    {
        Texture2D background;
        private LevelRenderer LevelRend;

        public GamePlayState(Game game)
            : base(game)
        {
            LevelRend = new LevelRenderer(Game, "level");
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            LevelRend.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (Manager.InputManager.HasBeenPressed(Keys.B))
                Manager.StateManager.RemoveState(this);

            LevelRend.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            LevelRend.Draw(gameTime);
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
            background = Game.Content.Load<Texture2D>("Textures/GamePlay");
        }

        public override void Stop()
        {
            
        }
    }
}
