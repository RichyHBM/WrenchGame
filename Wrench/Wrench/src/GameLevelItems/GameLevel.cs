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
using Wrench.src.Managers;


namespace Wrench.src.GameLevelItems
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameLevel : Microsoft.Xna.Framework.DrawableGameComponent
    {
        LevelRenderer levelRend;
        Level levelRaw;
        LevelCollisions levelCollisions;
        List<GameObject> objects = new List<GameObject>();
        private Player player;
        public GameLevel(Game game)
            : base(game)
        {
            levelRaw = ContentPreImporter.GetLevel("level");
            levelRend = new LevelRenderer(game, levelRaw);
            levelCollisions = new LevelCollisions(game, levelRaw);

            Vector3 playerPos = Vector3.Zero;
            for (int y = 0; y < levelRaw.Depth; y++)
                for (int x = 0; x < levelRaw.Width; x++)
                    if (levelRaw.GetAt(x, y) == 'p')
                        player = new Player(game, new Vector3(x + 0.5f, 0, y + 0.5f));
                    else if (levelRaw.GetAt(x, y) == 'e')
                        objects.Add(new Enemy(game, new Vector3(x + 0.5f, 0, y + 0.5f)));
            //Corner of the box if front left, so to place player in right place we need to add .5 to the left and .5 to the front
            objects.Add(player);
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
            foreach (GameObject obj in objects)
            {
                obj.Initialize();
            }

            levelCollisions.Initialize();
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
            foreach (GameObject obj in objects)
            {
                if(obj is Enemy)
                    (obj as Enemy).Update(gameTime, player.Position);
                else
                    obj.Update(gameTime);
            }
            
            levelCollisions.Update(gameTime);

            foreach (GameObject obj in objects)
            {

                if (levelCollisions.IsColliding(obj.BoundingBox))
                {
                    obj.Backup(gameTime);
                    obj.ReverseVelocity();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            levelRend.Draw(gameTime);
            foreach (GameObject obj in objects)
            {
                obj.Draw(gameTime);
            }
            levelCollisions.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
