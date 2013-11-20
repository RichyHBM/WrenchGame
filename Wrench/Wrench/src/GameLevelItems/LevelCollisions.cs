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
using CustomAssets;


namespace Wrench.src.GameLevelItems
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LevelCollisions : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Level level;
        List<BoundingBox> levelBoxes = new List<BoundingBox>();

        public LevelCollisions(Game game, Level lev)
            : base(game)
        {
            level = lev;
            for (int y = 0; y < level.Depth; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    if (level.GetAt(x, y) == '#')
                    {
                        levelBoxes.Add(new BoundingBox(
                                new Vector3(0.0f + x, 1.0f, 1.0f + y),
                                new Vector3(1.0f + x, 0.0f, 0.0f + y)
                            ));// (MapMesh.WallMeshAt(x, y));
                    }
                }
            }
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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BoundingBox box in levelBoxes)
            {
                Helpers.DebugShapeRenderer.AddBoundingBox(box, Color.Blue);
            }
            base.Draw(gameTime);
        }
    }
}
