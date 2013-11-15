using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Wrench.src.Helpers
{
    static class CubeMesh
    {
        public static VertexPositionColor[] CubeMeshAt(int x, int y)
        {
            List<VertexPositionColor> nonIndexedCube = new List<VertexPositionColor>();

            Vector3 topLeftFront = new Vector3(     0.0f + x,  1.0f,   1.0f + y);
            Vector3 bottomLeftFront = new Vector3(  0.0f + x,  0.0f,   1.0f + y);
            Vector3 bottomRightFront = new Vector3( 1.0f + x,   0.0f,   1.0f + y);
            Vector3 topRightFront = new Vector3(    1.0f + x,   1.0f,   1.0f + y);
            Vector3 topLeftBack = new Vector3(      0.0f + x,  1.0f,   0.0f + y);
            Vector3 bottomLeftBack = new Vector3(   0.0f + x,  0.0f,   0.0f + y);
            Vector3 bottomRightBack = new Vector3(  1.0f + x,   0.0f,   0.0f + y);
            Vector3 topRightBack = new Vector3(     1.0f + x,   1.0f,   0.0f + y);

            // Define cube's vertex colors


            
            // Back face
            nonIndexedCube.Add( new VertexPositionColor(topLeftBack, Color.Blue));
            nonIndexedCube.Add(new VertexPositionColor(bottomRightBack , Color.Blue));
            nonIndexedCube.Add(new VertexPositionColor(topRightBack, Color.Blue));
            nonIndexedCube.Add(new VertexPositionColor(bottomRightBack, Color.Blue));
            nonIndexedCube.Add(new VertexPositionColor(topLeftBack , Color.Blue));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftBack, Color.Blue));
            
            //*/
            
            // Bottom face
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftFront, Color.Green));
            nonIndexedCube.Add( new VertexPositionColor(bottomRightBack , Color.Green));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftBack, Color.Green));
            nonIndexedCube.Add( new VertexPositionColor(bottomRightBack, Color.Green));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftFront , Color.Green));
            nonIndexedCube.Add( new VertexPositionColor(bottomRightFront, Color.Green));
            //*/
            
            // Left face
            nonIndexedCube.Add( new VertexPositionColor(topLeftFront, Color.LightSlateGray));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftBack , Color.LightSlateGray));
            nonIndexedCube.Add( new VertexPositionColor(topLeftBack, Color.LightSlateGray));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftBack, Color.LightSlateGray));
            nonIndexedCube.Add( new VertexPositionColor(topLeftFront , Color.LightSlateGray));
            nonIndexedCube.Add( new VertexPositionColor(bottomLeftFront, Color.LightSlateGray));
            //*/
            // Top face
            
            nonIndexedCube.Add(new VertexPositionColor(topLeftFront, Color.Yellow));
            nonIndexedCube.Add(new VertexPositionColor(topLeftBack, Color.Yellow));
            nonIndexedCube.Add(new VertexPositionColor(topRightBack, Color.Yellow));
            nonIndexedCube.Add(new VertexPositionColor(topLeftFront, Color.Yellow));
            nonIndexedCube.Add(new VertexPositionColor(topRightBack, Color.Yellow));
            nonIndexedCube.Add(new VertexPositionColor(topRightFront, Color.Yellow));
            
            // Right face
            nonIndexedCube.Add( new VertexPositionColor(topRightFront, Color.Chocolate));
            nonIndexedCube.Add( new VertexPositionColor(topRightBack , Color.Chocolate));
            nonIndexedCube.Add(new VertexPositionColor(bottomRightBack, Color.Chocolate));
            nonIndexedCube.Add( new VertexPositionColor(topRightFront, Color.Chocolate));
            nonIndexedCube.Add( new VertexPositionColor(bottomRightBack , Color.Chocolate));
            nonIndexedCube.Add(new VertexPositionColor(bottomRightFront, Color.Chocolate));

            // Front face
            nonIndexedCube.Add(new VertexPositionColor(topLeftFront, Color.Red));
            nonIndexedCube.Add(new VertexPositionColor(topRightFront , Color.Red));
            nonIndexedCube.Add(new VertexPositionColor(bottomLeftFront, Color.Red));
            nonIndexedCube.Add(new VertexPositionColor(bottomLeftFront, Color.Red));
            nonIndexedCube.Add(new VertexPositionColor(topRightFront , Color.Red));
            nonIndexedCube.Add(new VertexPositionColor(bottomRightFront, Color.Red));
            //*/
            return nonIndexedCube.ToArray();
        }
    }
}
