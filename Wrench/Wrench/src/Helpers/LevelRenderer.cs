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
        protected List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        protected BasicEffect effect;

        public LevelRenderer(Game game, String levelName)
            : base(game)
        {
            this.levelName = levelName;
            level = Game.Content.Load<Level>(levelName);
            effect = new BasicEffect(Game.GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            for (int y = 0; y < level.Size; y++)
            {
                for (int x = 0; x < level.Size; x++)
                {
                    if (level.GetAt(x, y) == '#')
                    {
                        vertices.AddRange(CubeMesh.CubeMeshAt(x, y));
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
            effect.World = Matrix.CreateTranslation(Vector3.Zero);
            effect.View = Manager.MatrixManager.View;
            effect.Projection = Manager.MatrixManager.Perspective;
            effect.VertexColorEnabled = true;

            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices.ToArray(), 0, vertices.Count / 3, VertexPositionColor.VertexDeclaration);

            base.Draw(gameTime);
        }
    }
}
