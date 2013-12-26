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
        protected List<VertexPositionNormalTexture> wallVertices = new List<VertexPositionNormalTexture>();
        protected List<VertexPositionNormalTexture> floorVertices = new List<VertexPositionNormalTexture>();
        protected List<VertexPositionNormalTexture> ceilingVertices = new List<VertexPositionNormalTexture>();
        protected BasicEffect effect;
        protected Texture2D brickTexture;
        protected Texture2D floorTexture;
        protected Texture2D ceilingTexture;

        public LevelRenderer(Game game, Level levFile)
            : base(game)
        {
            level = levFile;
            //Textures for the different surfaces
            brickTexture = ContentPreImporter.GetTexture("bricks");
            floorTexture = ContentPreImporter.GetTexture("floor");
            ceilingTexture = ContentPreImporter.GetTexture("ceiling");
            effect = new BasicEffect(Game.GraphicsDevice);
            //Enable fog if required
            effect.FogEnabled = GlobalSettings.FogEnabled;
            effect.FogColor = GlobalSettings.FogColor;
            effect.FogStart = GlobalSettings.FogStart;
            effect.FogEnd = GlobalSettings.FogEnd;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //.lev characters that require both floor and ceiling or just floor
            string floorAndCeiling = ".pegh";
            string floor = ",PEGH";

            for (int y = 0; y < level.Depth; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    //Add the different types of map tiles for wall, floor, ceiling...
                    if (level.GetAt(x, y) == '#')
                    {
                        wallVertices.AddRange(MapMesh.WallMeshAt(x, y));
                    }
                    else if (floorAndCeiling.Contains(level.GetAt(x, y)))
                    {
                        floorVertices.AddRange(MapMesh.FloorMeshAt(x, y));
                        ceilingVertices.AddRange(MapMesh.CeilingMeshAt(x, y));
                    }
                    else if (floor.Contains(level.GetAt(x, y)))
                    {
                        floorVertices.AddRange(MapMesh.FloorMeshAt(x, y));
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
            //Pass all parameters to the effect
            effect.World = Matrix.Identity;
            effect.View = Manager.MatrixManager.View;
            effect.Projection = Manager.MatrixManager.Perspective;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            //Draw walls, floors & ceilings
            if (wallVertices.Count > 0)
            {
                effect.Texture = brickTexture;
                effect.CurrentTechnique.Passes[0].Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, wallVertices.ToArray(), 0, wallVertices.Count / 3, VertexPositionNormalTexture.VertexDeclaration);
            }

            if (floorVertices.Count > 0)
            {
                effect.Texture = floorTexture;
                effect.CurrentTechnique.Passes[0].Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, floorVertices.ToArray(), 0, floorVertices.Count / 3, VertexPositionNormalTexture.VertexDeclaration);
            }

            if (ceilingVertices.Count > 0)
            {
                effect.Texture = ceilingTexture;
                effect.CurrentTechnique.Passes[0].Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ceilingVertices.ToArray(), 0, ceilingVertices.Count / 3, VertexPositionNormalTexture.VertexDeclaration);
            }
            base.Draw(gameTime);
        }
    }
}
