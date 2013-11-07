using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wrench.src.Managers
{
    public static class MatrixManager
    {
        int width = 1, height = 1;
        float nearPlane = 0.1f, farPlane = 100.0f;
        Matrix orthogonal;
        Matrix perspective;

        public MatrixManager()
        {
            Matrix.CreateOrthographic(width, height, nearPlane, farPlane, out orthogonal);
            
        }

        public void SetWidthHeight(int width, int height)
        {
            this.width = width;
            this.height = height;
            Matrix.CreateOrthographic(width, height, nearPlane, farPlane, out orthogonal);
        }

        public Matrix Orthographic
        {
            get { return orthogonal; }
            private set;
        }

        public Matrix Perspective
        {
            get { return perspective; }
            private set;
        }


    }
}
