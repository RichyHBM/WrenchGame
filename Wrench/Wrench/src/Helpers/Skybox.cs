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
    public class Skybox
    {
        //Skybox parameters
        float size;
        Texture2D texture;
        VertexPositionNormalTexture[] vertices;
        BasicEffect effect;
        Matrix translation = Matrix.Identity;
        Vector3 position = Vector3.Zero;
        Game game;

        public Skybox(Game game, float size, Texture2D texture)
        {
            this.size = size;
            this.texture = texture;
            this.game = game;
            vertices = Skybox.BoxMesh(size);

            effect = new BasicEffect(game.GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = texture;
            // TODO: Construct any child components here
        }
        //Center the box to a new position
        public void SetPosition(Vector3 center)
        {
            Matrix.CreateTranslation(ref center, out translation);
            effect.World = translation;
            effect.View = Manager.MatrixManager.View;
            effect.Projection = Manager.MatrixManager.Perspective;
        }
        //Draw the box
        public void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, vertices.Length / 3);
            game.GraphicsDevice.BlendState = BlendState.Opaque;
        }

        //Create all 6 sides of the box facing inwards
        public static VertexPositionNormalTexture[] BoxMesh(float size)
        {
            List<VertexPositionNormalTexture> nonIndexedCube = new List<VertexPositionNormalTexture>();

            float minus = -0.5f * size;
            float plus = 0.5f * size;
            //size of each texture size
            float sH = 400 / 1200.0f;
            float sW = 400 / 1600.0f;

            Vector3 topLeftFront = new Vector3(minus, plus, plus);
            Vector3 bottomLeftFront = new Vector3(minus, minus, plus);
            Vector3 bottomRightFront = new Vector3(plus, minus, plus);
            Vector3 topRightFront = new Vector3(plus, plus, plus);
            Vector3 topLeftBack = new Vector3(minus, plus, minus);
            Vector3 bottomLeftBack = new Vector3(minus, minus, minus);
            Vector3 bottomRightBack = new Vector3(plus, minus, minus);
            Vector3 topRightBack = new Vector3(plus, plus, minus);

            // front face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Backward, new Vector2(sW * 4, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Backward, new Vector2(sW * 3, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Backward, new Vector2(sW * 3, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Backward, new Vector2(sW * 3, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Backward, new Vector2(sW * 4, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Backward, new Vector2(sW * 4, sH * 2)));

            //*/

            // top face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Down, new Vector2(sW * 1, sH * 0)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Down, new Vector2(sW * 2, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Down, new Vector2(sW * 1, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Down, new Vector2(sW * 2, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Down, new Vector2(sW * 1, sH * 0)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Down, new Vector2(sW * 2, sH * 0)));
            //*/

            // right face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Left, new Vector2(sW * 3, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Left, new Vector2(sW * 2, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Left, new Vector2(sW * 2, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Left, new Vector2(sW * 2, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Left, new Vector2(sW * 3, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Left, new Vector2(sW * 3, sH * 2)));
            //*/
            // bottom face
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Up, new Vector2(sW * 1, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Up, new Vector2(sW * 2, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Up, new Vector2(sW * 1, sH * 3)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Up, new Vector2(sW * 1, sH * 3)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Up, new Vector2(sW * 2, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Up, new Vector2(sW * 2, sH * 3)));

            // left face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Right, new Vector2(sW * 0, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Right, new Vector2(sW * 1, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Right, new Vector2(sW * 1, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Right, new Vector2(sW * 0, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Right, new Vector2(sW * 1, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Right, new Vector2(sW * 0, sH * 2)));

            // back face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Forward, new Vector2(sW * 1, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Forward, new Vector2(sW * 2, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Forward, new Vector2(sW * 1, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Forward, new Vector2(sW * 1, sH * 2)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Forward, new Vector2(sW * 2, sH * 1)));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Forward, new Vector2(sW * 2, sH * 2)));
            //*/
            return nonIndexedCube.ToArray();
        }
    }
}
