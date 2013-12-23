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
using Wrench.src.GameObjects;
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
        protected Effect effect;
        protected Matrix rotation = Matrix.Identity;
        protected Matrix translation = Matrix.Identity;
        protected Vector3 position;
        protected Matrix world = Matrix.Identity;

        protected bool fogEnabled;
        protected float fogStart;
        protected float fogEnd;
        protected Vector3 fogColor;

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

            effect = ContentPreImporter.GetEffect("Billboard");

            fogEnabled = GlobalSettings.FogEnabled;
            fogStart = GlobalSettings.FogStart;
            fogEnd = GlobalSettings.FogEnd;
            fogColor = GlobalSettings.FogColor;
        }

        public void OverrideFog(bool enable, Vector3 color, float start, float end)
        {
            fogEnabled = enable;
            fogStart = start;
            fogEnd = end;
            fogColor = color;
        }

        public void SetTexture(Texture2D t)
        {
            this.texture = t;

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
            position = pos;
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
            rotation *= Matrix.CreateLookAt(position, lookAt, Vector3.Up);
        }

        public void ForceUpdate()
        {
            world = rotation * translation;
            rotation = Matrix.Identity;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            world = rotation * translation;
            rotation = Matrix.Identity;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            effect.Parameters["Texture"].SetValue(texture);
            effect.Parameters["World"].SetValue(world);
            effect.Parameters["View"].SetValue(Manager.MatrixManager.View);
            effect.Parameters["Projection"].SetValue(Manager.MatrixManager.Perspective);

            effect.Parameters["FogStart"].SetValue(fogStart);
            effect.Parameters["FogEnd"].SetValue(fogEnd);
            effect.Parameters["FogColor"].SetValue(new Vector4(fogColor, 255));
            effect.Parameters["FogEnabled"].SetValue(fogEnabled);

            effect.CurrentTechnique.Passes[0].Apply();
            Game.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
            Game.GraphicsDevice.BlendState = BlendState.Opaque;
            base.Draw(gameTime);
        }
    }
}
