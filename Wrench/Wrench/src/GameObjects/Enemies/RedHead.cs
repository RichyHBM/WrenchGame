using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CustomAssets;
using Wrench.src.Managers;
using Wrench.src.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Wrench.src.BaseClasses;

namespace Wrench.src.GameObjects.Enemies
{
    public class RedHead : Enemy
    {
        public RedHead(Game game, Vector3 pos, Level l)
            : base(game, pos, l)
        {
            life = 5;
            textures = new Texture2D[life];
            Damage = 5;
            this.level = l;
            this.position = pos;
            RotationSpeed = 0.1f;
            ForwardSpeed = 6f;
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

            textures[4] = ContentPreImporter.GetTexture("RedHead");
            textures[3] = ContentPreImporter.GetTexture("RedHead4");
            textures[2] = ContentPreImporter.GetTexture("RedHead3");
            textures[1] = ContentPreImporter.GetTexture("RedHead2");
            textures[0] = ContentPreImporter.GetTexture("RedHead1");
            hurtSound = ContentPreImporter.GetSound("enemyHurt");

            billboard = new Billboard(game, textures[life - 1], Vector2.One / 2);
            billboard.Move(position + new Vector3(0, 0.25f, 0));
            billboard.ForceUpdate();
            target = pos;

            billboard.OverrideFog(GlobalSettings.FogEnabled, GlobalSettings.FogColor, GlobalSettings.FogStart, GlobalSettings.FogEnd * 2.1f);
        }
    }
}
