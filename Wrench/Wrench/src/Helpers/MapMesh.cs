using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Wrench.src.Helpers
{
    //Creates vertices for the floor, ceiling and wall making their UV correct and normals point in the right direction
    static class MapMesh
    {
        static Vector2 uvx0y0 = new Vector2(0, 0);
        static Vector2 uvx0y1 = new Vector2(0, 1);
        static Vector2 uvx1y0 = new Vector2(1, 0);
        static Vector2 uvx1y1 = new Vector2(1, 1);

        public static VertexPositionNormalTexture[] WallMeshAt(float x, float y)
        {
            List<VertexPositionNormalTexture> nonIndexedCube = new List<VertexPositionNormalTexture>();
            x -= 0.5f;
            y -= 0.5f;

            Vector3 topLeftFront = new Vector3(0.0f + x, 1.0f, 1.0f + y);
            Vector3 bottomLeftFront = new Vector3(0.0f + x, 0.0f, 1.0f + y);
            Vector3 bottomRightFront = new Vector3(1.0f + x, 0.0f, 1.0f + y);
            Vector3 topRightFront = new Vector3(1.0f + x, 1.0f, 1.0f + y);
            Vector3 topLeftBack = new Vector3(0.0f + x, 1.0f, 0.0f + y);
            Vector3 bottomLeftBack = new Vector3(0.0f + x, 0.0f, 0.0f + y);
            Vector3 bottomRightBack = new Vector3(1.0f + x, 0.0f, 0.0f + y);
            Vector3 topRightBack = new Vector3(1.0f + x, 1.0f, 0.0f + y);

            // Back face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Backward, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Backward, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Backward, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Backward, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Backward, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Backward, uvx1y1));

            //*/

            // Bottom face
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Down, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Down, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Down, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Down, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Down, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Down, uvx1y1));
            //*/

            // Left face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Left, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Left, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Left, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Left, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Left, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Left, uvx1y1));
            //*/
            // Top face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Up, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Up, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Up, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Up, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Up, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Up, uvx1y1));

            // Right face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Right, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Right, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Right, uvx1y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Right, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Right, uvx1y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Right, uvx0y1));

            // Front face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Forward, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Forward, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Forward, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Forward, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Forward, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Forward, uvx1y1));
            //*/
            return nonIndexedCube.ToArray();
        }

        public static VertexPositionNormalTexture[] FloorMeshAt(float x, float y)
        {
            x -= 0.5f;
            y -= 0.5f;
            List<VertexPositionNormalTexture> nonIndexedCube = new List<VertexPositionNormalTexture>();
            Vector3 topLeftFront = new Vector3(0.0f + x, 0.0f, 1.0f + y);
            Vector3 topRightFront = new Vector3(1.0f + x, 0.0f, 1.0f + y);
            Vector3 topLeftBack = new Vector3(0.0f + x, 0.0f, 0.0f + y);
            Vector3 topRightBack = new Vector3(1.0f + x, 0.0f, 0.0f + y);

            // Back face
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftBack, Vector3.Up, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Up, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Up, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topLeftFront, Vector3.Up, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightBack, Vector3.Up, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(topRightFront, Vector3.Up, uvx1y1));
            return nonIndexedCube.ToArray();
        }

        public static VertexPositionNormalTexture[] CeilingMeshAt(float x, float y)
        {
            x -= 0.5f;
            y -= 0.5f;
            List<VertexPositionNormalTexture> nonIndexedCube = new List<VertexPositionNormalTexture>();
            Vector3 bottomLeftFront = new Vector3(0.0f + x, 1.0f, 1.0f + y);
            Vector3 bottomRightFront = new Vector3(1.0f + x, 1.0f, 1.0f + y);
            Vector3 bottomLeftBack = new Vector3(0.0f + x, 1.0f, 0.0f + y);
            Vector3 bottomRightBack = new Vector3(1.0f + x, 1.0f, 0.0f + y);

            // Back face
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Down, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Down, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftBack, Vector3.Down, uvx0y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightBack, Vector3.Down, uvx0y1));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomLeftFront, Vector3.Down, uvx1y0));
            nonIndexedCube.Add(new VertexPositionNormalTexture(bottomRightFront, Vector3.Down, uvx1y1));
            return nonIndexedCube.ToArray();
        }
    }
}
