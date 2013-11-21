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
using Wrench.src.Helpers;


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
        HolsteredGun gun;
        SpriteFont font;

        int health = 100;

        public Player(Game game, Vector3 pos)
            : base(game)
        {
            this.position = pos;
            headPosition = new Vector3(0, 0.6f, 0);
            RotationSpeed = 0.2f;
            ForwardSpeed = 10f;
            
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(boxMin, boxMax);
            font = Game.Content.Load<SpriteFont>("TextFont");
            gun = new HolsteredGun(game, game.Content.Load<Texture2D>("Textures/gun"), new Vector2(0.1f));
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

            amountOfRotation += Manager.InputManager.MouseChange.X * RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Manager.InputManager.IsDown(Keys.W))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation);
                Vector3 v = new Vector3(0, 0, -ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                velocity += v;
            }
            if (Manager.InputManager.IsDown(Keys.S))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation);
                Vector3 v = new Vector3(0, 0, ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                velocity += v;
            }
            if (Manager.InputManager.IsDown(Keys.A))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation + MathHelper.ToRadians(90));
                Vector3 v = new Vector3(0, 0, -ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                velocity += v;
            }
            if (Manager.InputManager.IsDown(Keys.D))
            {
                Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation + MathHelper.ToRadians(90));
                Vector3 v = new Vector3(0, 0, ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                v = Vector3.Transform(v, forwardMovement);
                velocity += v;
            }

            MoveForward(gameTime);

            Vector3 cameraPosition = position + headPosition;
            rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 transformedReference = Vector3.Transform(cameraReference, rotationMatrix);
            Vector3 cameraLookat = cameraPosition + transformedReference;
#if VIEWDEBUG
            Manager.MatrixManager.SetPosition(cameraPosition + new Vector3(1,2,1));
            Manager.MatrixManager.SetLookAt(cameraPosition);
#else
            Manager.MatrixManager.SetPosition(cameraPosition);
            Manager.MatrixManager.SetLookAt(cameraLookat);
#endif
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);
            gun.SetPositionRotation(position, amountOfRotation);
            gun.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Backup(GameTime gameTime)
        {
            position = lastPosition;
            velocity = -velocity;
            Matrix rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 cameraPosition = position + headPosition;
            rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 transformedReference = Vector3.Transform(cameraReference, rotationMatrix);
            Vector3 cameraLookat = cameraPosition + transformedReference;
#if VIEWDEBUG
            Manager.MatrixManager.SetPosition(cameraPosition + new Vector3(1,2,1));
            Manager.MatrixManager.SetLookAt(cameraPosition);
#else
            Manager.MatrixManager.SetPosition(cameraPosition);
            Manager.MatrixManager.SetLookAt(cameraLookat);
#endif
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);
            gun.SetPositionRotation(position, amountOfRotation);
            gun.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteBatch sp = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            sp.Begin();
            sp.DrawString(font, "Life: " + health, new Vector2(10, 10), Color.Black);
            sp.End();

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

#if DEBUG
            Vector3 lineStart = position + new Vector3(0, 0.5f, 0);
            Matrix rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 transformedReference = Vector3.Transform(Vector3.Forward * 0.3f, rotationMatrix);
            Helpers.DebugShapeRenderer.AddLine(lineStart, lineStart + transformedReference, Color.Purple);
            Helpers.DebugShapeRenderer.AddBoundingBox(boundingBox, Color.Red);
#endif
            gun.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
