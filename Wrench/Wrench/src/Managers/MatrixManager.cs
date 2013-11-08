using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Wrench.src.BaseClasses;

namespace Wrench.src.Managers
{
    public class MatrixManager : IManager
    {
        int width = 1, height = 1;
        float nearPlane = 0.1f, farPlane = 100.0f;
        float fieldOfView = 75;
        Vector3 position = Vector3.UnitZ, lookAt = Vector3.Zero, up = Vector3.Up;
        Matrix orthogonal;
        Matrix perspective;
        Matrix view;
        Matrix viewPerspective;
        Matrix viewOrthographic;

        public void Initialize()
        {
            orthogonal = Matrix.CreateOrthographic(width, height, nearPlane, farPlane);
            view = Matrix.CreateLookAt(position, lookAt, up);
            perspective = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians( fieldOfView ), width / (float)height, nearPlane, farPlane);
            Matrix.Multiply(ref view, ref orthogonal, out viewOrthographic);
            Matrix.Multiply(ref view, ref perspective, out viewPerspective);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime)
        {
        }

        public void SetWidthHeight(int width, int height)
        {
            this.width = width;
            this.height = height;
            orthogonal = Matrix.CreateOrthographic(width, height, nearPlane, farPlane);
            perspective = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), width / (float)height, nearPlane, farPlane);
            Matrix.Multiply(ref view, ref orthogonal, out viewOrthographic);
            Matrix.Multiply(ref view, ref perspective, out viewPerspective);
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
            view = Matrix.CreateLookAt(position, lookAt, up);
            Matrix.Multiply(ref view, ref orthogonal, out viewOrthographic);
            Matrix.Multiply(ref view, ref perspective, out viewPerspective);
        }

        public void SetLookAt(Vector3 lookAt)
        {
            this.lookAt = lookAt;
            view = Matrix.CreateLookAt(position, lookAt, up);
            Matrix.Multiply(ref view, ref orthogonal, out viewOrthographic);
            Matrix.Multiply(ref view, ref perspective, out viewPerspective);
        }

        public void SetUp(Vector3 up)
        {
            this.up = up;
            view = Matrix.CreateLookAt(position, lookAt, up);
            Matrix.Multiply(ref view, ref orthogonal, out viewOrthographic);
            Matrix.Multiply(ref view, ref perspective, out viewPerspective);
        }

        public Matrix Orthographic
        {
            get { return orthogonal; }
            private set{}
        }

        public Matrix Perspective
        {
            get { return perspective; }
            private set{}
        }

        public Matrix View
        {
            get { return view; }
            private set { }
        }

        public Matrix ViewPerspective
        {
            get { return viewPerspective; }
            private set { }
        }

        public Matrix ViewOrthographic
        {
            get { return viewOrthographic; }
            private set { }
        }


    }
}
