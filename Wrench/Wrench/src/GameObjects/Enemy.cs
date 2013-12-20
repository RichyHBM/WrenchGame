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


namespace Wrench.src.GameObjects
{
    public class Enemy : GameObject
    {
        public static int EnemyLife = 5;
        Billboard billboard;
        Vector3 target;
        Random rand = new Random();
        int life = Enemy.EnemyLife;
        Level level;
        Texture2D[] textures;
        SoundEffect hurtSound;

        public Enemy(Game game, Vector3 pos, Level l)
            : base(game)
        {
            textures = new Texture2D[Enemy.EnemyLife];

            this.level = l;
            this.position = pos;
            RotationSpeed = 0.1f;
            ForwardSpeed = 6f;
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

            textures[4] = ContentPreImporter.GetTexture("enemy");
            textures[3] = ContentPreImporter.GetTexture("enemy4");
            textures[2] = ContentPreImporter.GetTexture("enemy3");
            textures[1] = ContentPreImporter.GetTexture("enemy2");
            textures[0] = ContentPreImporter.GetTexture("enemy1");
            hurtSound = ContentPreImporter.GetSound("enemyHurt");

            billboard = new Billboard(game, textures[4], Vector2.One / 2);
            billboard.Move(position + new Vector3(0, 0.25f, 0));
            billboard.ForceUpdate();
            target = pos;

            billboard.OverrideFog(GlobalSettings.FogEnabled, GlobalSettings.FogColor, GlobalSettings.FogStart, GlobalSettings.FogEnd * 2.1f);
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
                player.Hit();
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

        public override void Hit()
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
