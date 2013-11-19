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
using Wrench.src.Helpers;
using CustomAssets;
using Wrench.src.GameObjects;


namespace Wrench.src.GameLevelItems
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameLevel : Microsoft.Xna.Framework.DrawableGameComponent
    {
        LevelRenderer levelRend;
        Level levelRaw;
        Player player;
        public GameLevel(Game game)
            : base(game)
        {
            levelRaw = game.Content.Load<Level>("level");
            levelRend = new LevelRenderer(game, levelRaw);
            player = new Player(game, Vector3.Zero);
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            levelRend.Initialize();
            player.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            levelRend.Update(gameTime);
            player.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            levelRend.Draw(gameTime);
            player.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
