using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Wrench.src.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Wrench.src.GameObjects;

namespace Wrench.src.BaseClasses
{
    public abstract class Pickup : GameObject
    {
        protected Billboard billboard;
        protected Texture2D texture;
        protected SoundEffect pickupSound;

        public Pickup(Game game, Vector3 pos)
            : base(game)
        {
            this.position = pos;
            
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

        }

        public abstract void DoEffect(Player p);

        public void Update(GameTime gameTime, Player player)
        {
            if (boundingBox.Intersects(player.BoundingBox))
            {
                DoEffect(player);
            }

            amountOfRotation = -(float)(Math.Atan2(player.Position.Z - position.Z, player.Position.X - position.X) - MathHelper.ToRadians(90));
            
            billboard.RotateY(amountOfRotation);
            billboard.Update(gameTime);

            base.Update(gameTime);
        }
        public override void Hit(int damage)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            billboard.Draw(gameTime);
#if DEBUG
            Vector3 lineStart = position + new Vector3(0, 0.5f, 0);
            Matrix rotationMatrix = Matrix.CreateRotationY(amountOfRotation);
            Vector3 transformedReference = Vector3.Transform(Vector3.Backward * 0.3f, rotationMatrix);

            Helpers.DebugShapeRenderer.AddLine(lineStart, lineStart + transformedReference, Color.Purple);
            Helpers.DebugShapeRenderer.AddBoundingBox(boundingBox, Color.Red);
#endif
            base.Draw(gameTime);
        }

    }
}
