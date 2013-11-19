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
using Wrench.src.Managers;


namespace Wrench.src.GameObjects
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : GameObject
    {
        //its position will always be on the floor, so we need an offset to 'elevate' the camera
        Vector3 headPosition;
        //In this game the player can only rotate around y
        float amountOfRotation = 0;
        //The position the player looks at when not rotated
        Vector3 cameraReference = Vector3.Forward;

        public Player(Game game, Vector3 pos)
            : base(game)
        {
            this.position = pos;
            headPosition = new Vector3(0, 0.8f, 0);
            RotationSpeed = 2f;
            ForwardSpeed = 3f;
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
            Matrix rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 cameraPosition = Position + headPosition;
            if (Manager.InputManager.IsDown(Keys.Left))
            {
                amountOfRotation += RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Manager.InputManager.IsDown(Keys.Right))
            {
                amountOfRotation -= RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Manager.InputManager.IsDown(Keys.Up))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation);
                Vector3 v = new Vector3(0, 0, -ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                position.Z += v.Z;
                position.X += v.X;
            }
            if (Manager.InputManager.IsDown(Keys.Down))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation);
                Vector3 v = new Vector3(0, 0, ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                position.Z += v.Z;
                position.X += v.X;
            }

            cameraPosition = position + headPosition;
            rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 transformedReference = Vector3.Transform(cameraReference, rotationMatrix);
            Vector3 cameraLookat = cameraPosition + transformedReference;
            Manager.MatrixManager.SetPosition(cameraPosition);
            Manager.MatrixManager.SetLookAt(cameraLookat);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
