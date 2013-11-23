using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wrench.src.Helpers;
using Wrench.src.Managers;

namespace Wrench.src.GameObjects
{
    public class Enemy : GameObject
    {
        Billboard billboard;
        public Enemy(Game game, Vector3 pos)
            : base(game)
        {
            this.position = pos;
            RotationSpeed = 0.1f;
            ForwardSpeed = 10f;

            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(boxMin, boxMax);
            billboard = new Billboard(game, ContentPreImporter.GetTexture("Textures/enemy"), Vector2.One / 2);
        }

        public void Update(GameTime gameTime, Vector3 playerPos)
        {
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

            billboard.Move(position + new Vector3(0, 0.25f, 0));

            Vector3 direction = playerPos - position;
            direction.Normalize();
            float rotation = (float)Math.Acos(Vector3.Dot(Vector3.Backward, direction));

            billboard.RotateY(rotation);
            
            billboard.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            billboard.Draw(gameTime);
#if DEBUG
            Helpers.DebugShapeRenderer.AddBoundingBox(boundingBox, Color.Red);
#endif
            base.Draw(gameTime);
        }
    }
}
