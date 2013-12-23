using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wrench.src.Helpers;
using Wrench.src.Managers;
using CustomAssets;
using Microsoft.Xna.Framework.Audio;
using Wrench.src.GameObjects;


namespace Wrench.src.BaseClasses
{
    public abstract class Enemy : GameObject
    {
        protected Billboard billboard;
        protected Vector3 target;
        protected Random rand = new Random();
        protected int life;
        protected Level level;
        protected Texture2D[] textures;
        protected SoundEffect hurtSound;
        public int Damage { protected set; get; }

        public Enemy(Game game, Vector3 pos, Level l)
            : base(game)
        {
            this.level = l;
            this.position = pos;
            
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

        }

        public void Update(GameTime gameTime, Player player, bool seesPlayer)
        {
            if (seesPlayer)
            {
                target = new Vector3((float)Math.Round(player.Position.X), 0, (float)Math.Round(player.Position.Z));
            }

            if (target == position)
            {
                target = position + new Vector3((float)rand.NextDouble() - 0.5f, 0, (float)rand.NextDouble() - 0.5f) * 20;
            }

            if (boundingBox.Intersects(player.BoundingBox))
            {
                position = GetRandomPosition();
                target = position + new Vector3((float)rand.NextDouble() - 0.5f, 0, (float)rand.NextDouble() - 0.5f) * 20;
                player.Hit(Damage);
            }

            amountOfRotation = -(float)(Math.Atan2(target.Z - position.Z, target.X - position.X) - MathHelper.ToRadians(90));
            
            Matrix forwardMovement = Matrix.CreateRotationY(amountOfRotation);
            Vector3 v = new Vector3(0, 0, ForwardSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            v = Vector3.Transform(v, forwardMovement);

            velocity += v;
            MoveForward(gameTime);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);
            billboard.Move(position + new Vector3(0, 0.25f, 0));
            billboard.RotateY(amountOfRotation);
            billboard.Update(gameTime);

            base.Update(gameTime);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 v = new Vector3(rand.Next(level.Width), 0, rand.Next(level.Depth));
            while (level.GetAt((int)v.X, (int)v.Z) == '#')
            {
                v = new Vector3(rand.Next(level.Width), 0, rand.Next(level.Depth));
            }
            return v;
        }

        public override void Backup(GameTime gameTime)
        {
            target = position + new Vector3((float)rand.NextDouble() - 0.5f, 0, (float)rand.NextDouble() - 0.5f) * 20;
            base.Backup(gameTime);
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

        public override void Hit(int damage)
        {
            hurtSound.Play();
            life--;
            if(life <=0)
                Alive = false;
            else
            {
                billboard.SetTexture(textures[life - 1]);
            }
        }
    }
}
