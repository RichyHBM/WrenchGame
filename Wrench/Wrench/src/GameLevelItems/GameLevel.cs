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
using Wrench.src.States;
using Wrench.src.BaseClasses;
using Wrench.src.GameObjects.Enemies;
using Wrench.src.GameObjects.Pickups;


namespace Wrench.src.GameLevelItems
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    // Main level class, renders level and updates & checks collisions between objects
    public class GameLevel : Microsoft.Xna.Framework.DrawableGameComponent
    {
        LevelRenderer levelRend;
        Level levelRaw;
        LevelCollisions levelCollisions;
        List<GameObject> objects = new List<GameObject>();
        private Player player;
        int enemies = 0;
        SpriteFont font;
        Skybox skybox;
        GamePlayState gameState;

        public GameLevel(Game game, GamePlayState gameState, string levenName)
            : base(game)
        {
            this.gameState = gameState;
            levelRaw = ContentPreImporter.GetLevel(levenName);
            levelRend = new LevelRenderer(game, levelRaw);
            levelCollisions = new LevelCollisions(game, levelRaw);
            font = ContentPreImporter.GetFont("TextFont");

            Vector3 playerPos = Vector3.Zero;
            int enemyFound = 0;
            //Build the game level creating the enemies, player and pickups
            for (int y = 0; y < levelRaw.Depth; y++)
            {
                for (int x = 0; x < levelRaw.Width; x++)
                {
                    if (levelRaw.GetAt(x, y).ToString().ToLower() == "p")
                        player = new Player(game, new Vector3(x, 0, y));
                    else if (levelRaw.GetAt(x, y).ToString().ToLower() == "e")
                    {
                        //Depending on the difficulty only add certain enemies
                        enemyFound++;
                        if ((enemyFound % GlobalSettings.EnemyFrequency) == 0)
                        {
                            objects.Add(new RedHead(game, new Vector3(x, 0, y), levelRaw));
                            enemies++;
                        }
                    }
                    else if (levelRaw.GetAt(x, y).ToString().ToLower() == "g")
                    {
                        enemyFound++;
                        if ((enemyFound % GlobalSettings.EnemyFrequency) == 0)
                        {
                            objects.Add(new GreenGhost(game, new Vector3(x, 0, y), levelRaw));
                            enemies++;
                        }
                    }
                    else if (levelRaw.GetAt(x, y).ToString().ToLower() == "h")
                    {
                        objects.Add(new Health(game, new Vector3(x, 0, y)));
                    }
                }
            }

            skybox = new Skybox(Game, 50, ContentPreImporter.GetTexture("grimmnight_small"));
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
            //Update all objects
            foreach (GameObject obj in objects)
            {
                if (obj is Enemy)
                {
                    Enemy en = obj as Enemy;

                    en.Update(gameTime, player, isInSighten(en, player));
                }
                else if (obj is Pickup)
                {
                    Pickup pick = obj as Pickup;

                    pick.Update(gameTime, player);
                }
                else
                    obj.Update(gameTime);
            }

            levelCollisions.Update(gameTime);

            //Check for collisions against the walls
            foreach (GameObject obj in objects)
            {
                if (levelCollisions.IsColliding(obj.BoundingBox))
                {
                    obj.Backup(gameTime);
                    obj.ReverseVelocity();
                }
            }

            //Hit enemy if its shot
            if (player.Shot)
            {
                foreach (GameObject obj in objects)
                {
                    if (obj is Enemy)
                    {
                        if (isInDirection(player, obj))
                        {
                            obj.Hit(0);
                            break;
                        }
                    }

                }
            }
            //Update the skybox to the players position
            skybox.SetPosition(player.Position);
            //Remove dead objects
            List<GameObject> toRemove = new List<GameObject>();
            foreach (GameObject obj in objects)
                if (obj.Alive == false)
                {
                    toRemove.Add(obj);
                    if (obj is Enemy)
                        enemies--;
                }
            foreach (GameObject obj in toRemove)
                objects.Remove(obj);
            //Change to a win/lose state if required
            if (!player.Alive)
                Manager.StateManager.PushState(new LoseState(Game, gameState));
            if (enemies == 0)
                Manager.StateManager.PushState(new WinState(Game, gameState));


            base.Update(gameTime);
        }

        //Draw all objects
        public override void Draw(GameTime gameTime)
        {
            skybox.Draw(gameTime);
            levelRend.Draw(gameTime);
            foreach (GameObject obj in objects)
            {
                obj.Draw(gameTime);
            }
            levelCollisions.Draw(gameTime);


            SpriteBatch sp = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            sp.Begin();
            sp.DrawString(font, "Enemies: " + enemies, new Vector2(10, 5), Color.Red);
            sp.End();
            //Reset things the spriteback changes
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            base.Draw(gameTime);
        }

        Random ran = new Random();
        //Returns a random empty tile
        private Vector3 GetRandomEmpty()
        {
            Vector3 vec = new Vector3();
            vec.Y = 0;
            do
            {
                vec.X = ran.Next(levelRaw.Width);
                vec.Z = ran.Next(levelRaw.Depth);
            } while (levelRaw.GetAt((int)vec.X, (int)vec.Z) == '#');

            return vec;
        }
        //Checks if 2 objects are in sight using walls
        public bool isInSighten(GameObject one, GameObject two)
        {
            Vector3 pos = one.Position + (Vector3.Up / 2.0f);
            Vector3 dir = two.Position - one.Position;
            dir.Normalize();
            Ray ray = new Ray(pos, dir);
            //Check if the ray intersects with any wall before hitting the other object
            float? enemyDist = ray.Intersects(two.BoundingBox);
            bool inSight = false;
            if (enemyDist != null)
            {
                inSight = true;
                foreach (BoundingBox box in levelCollisions.LevelCollisionBoxes)
                {
                    float? distWall = ray.Intersects(box);
                    if (distWall != null && distWall < enemyDist)
                    {
                        inSight = false;
                        break;
                    }
                }
            }
            return inSight;
        }
        //Checks the same as in sight but only in the direction the first object is facing
        public bool isInDirection(GameObject one, GameObject two)
        {
            Matrix forwardMovement = Matrix.CreateRotationY(one.Rotation);
            Vector3 direction = Vector3.Transform(Vector3.Forward, forwardMovement);
            direction.Normalize();
            Vector3 pos = one.Position + (Vector3.Up / 2.0f);

            Ray ray = new Ray(pos, direction);

            float? enemyDist = ray.Intersects(two.BoundingBox);
            bool inSight = false;
            if (enemyDist != null)
            {
                inSight = true;
                foreach (BoundingBox box in levelCollisions.LevelCollisionBoxes)
                {
                    float? distWall = ray.Intersects(box);
                    if (distWall != null && distWall < enemyDist)
                    {
                        inSight = false;
                        break;
                    }
                }
            }
            return inSight;
        }
    }
}
