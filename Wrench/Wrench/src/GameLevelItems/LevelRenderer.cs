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
using Wrench.src.Managers;


namespace Wrench.src.Helpers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LevelRenderer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Level level;
        protected String levelName;
        protected List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
        protected BasicEffect effect;
        protected Texture2D brickTexture;

        public LevelRenderer(Game game, Level levFile)
            : base(game)
        {
            level = levFile;
            brickTexture = Game.Content.Load<Texture2D>("Textures/bricks");
            effect = new BasicEffect(Game.GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            for (int y = 0; y < level.Depth; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    if (level.GetAt(x, y) == '#')
                    {
                        vertices.AddRange(MapMesh.WallMeshAt(x, y));
                    }
                    else if (level.GetAt(x, y) == '.' || level.GetAt(x, y) == 'p')
                    {
                        vertices.AddRange(MapMesh.FloorMeshAt(x, y));
                    }
                }
            }

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
            effect.World = Matrix.CreateTranslation(new Vector3(-(level.Width / 2), 0, -(level.Depth / 2)));
            effect.View = Manager.MatrixManager.View;
            effect.Projection = Manager.MatrixManager.Perspective;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = brickTexture;

            effect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices.ToArray(), 0, vertices.Count / 3, VertexPositionNormalTexture.VertexDeclaration);

            base.Draw(gameTime);
        }
    }
}
