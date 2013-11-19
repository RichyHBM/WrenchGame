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

            Vector3 playerPos = Vector3.Zero;
            for (int y = 0; y < levelRaw.Depth; y++)
                for (int x = 0; x < levelRaw.Width; x++)
                    if (levelRaw.GetAt(x, y) == 'p')
                        playerPos = new Vector3(x - (levelRaw.Width / 2) + 0.5f, 0, y - (levelRaw.Depth / 2) + 0.5f);
            //Corner of the box if front left, so to place player in right place we need to add .5 to the left and .5 to the front

            player = new Player(game, playerPos);
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
