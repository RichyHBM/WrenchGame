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


namespace Wrench.src.Helpers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Billboard : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Texture2D texture;
        protected VertexPositionTexture[] vertices;
        protected BasicEffect effect;
        protected Matrix rotation = Matrix.Identity;
        protected Matrix translation = Matrix.Identity;
        public Billboard(Game game, Texture2D texture, Vector2 size)
            : base(game)
        {
            this.texture = texture;

            vertices = new VertexPositionTexture[6]{
                new VertexPositionTexture(new Vector3(-0.5f * size.X, 0.0f * size.Y, 0.0f), new Vector2(0, 1)),
                new VertexPositionTexture(new Vector3(-0.5f * size.X, 1.0f * size.Y, 0.0f), new Vector2(0, 0)),
                new VertexPositionTexture(new Vector3(0.5f * size.X, 1.0f * size.Y, 0.0f), new Vector2(1, 0)),
                new VertexPositionTexture(new Vector3(-0.5f * size.X, 0.0f * size.Y, 0.0f), new Vector2(0, 1)),
                new VertexPositionTexture(new Vector3(0.5f * size.X, 1.0f * size.Y, 0.0f), new Vector2(1, 0)),
                new VertexPositionTexture(new Vector3(0.5f * size.X, 0.0f * size.Y, 0.0f), new Vector2(1, 1))
            };

            effect = new BasicEffect(game.GraphicsDevice);            
            effect.TextureEnabled = true;
            effect.Texture = texture;
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

        public void Move(Vector3 pos)
        {
            Matrix.CreateTranslation(ref pos, out translation);
        }

        public void RotateX(float angle)
        {
            rotation *= Matrix.CreateRotationX(angle);
        }
        public void RotateY(float angle)
        {
            rotation *= Matrix.CreateRotationY(angle);
        }
        public void RotateZ(float angle)
        {
            rotation *= Matrix.CreateRotationZ(angle);
        }

        public void Face(Vector3 lookAt)
        { 
        
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            effect.World = rotation * translation;
            rotation = Matrix.Identity;
            effect.View = Manager.MatrixManager.View;
            effect.Projection = Manager.MatrixManager.Perspective;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            effect.CurrentTechnique.Passes[0].Apply();
            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
            Game.GraphicsDevice.BlendState = BlendState.Opaque;
            base.Draw(gameTime);
        }
    }
}
