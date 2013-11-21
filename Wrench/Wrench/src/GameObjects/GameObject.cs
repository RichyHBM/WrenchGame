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


namespace Wrench.src.GameObjects
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameObject : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Vector3 position;
        protected Vector3 velocity;
        protected BoundingBox boundingBox;
        protected Vector3 boxMin;
        protected Vector3 boxMax;
        public Vector3 lastPosition;

        public Vector3 Position { get { return position; } private set { } }
        public Vector3 Velocity { get { return velocity; } private set { } }
        public BoundingBox BoundingBox { get { return boundingBox; } private set { } }

        public float RotationSpeed { get; protected set; }
        public float ForwardSpeed { get; protected set; }

        public GameObject(Game game)
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
            velocity += -velocity * ForwardSpeed * 5 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }


        public void MoveForward(GameTime gameTime)
        {
            lastPosition = position;
            position += velocity * ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public virtual void Backup(GameTime gameTime)
        {
            position = lastPosition;
        }
        public void ReverseVelocity()
        {
            velocity.X = -velocity.X;
        }
    }
}
