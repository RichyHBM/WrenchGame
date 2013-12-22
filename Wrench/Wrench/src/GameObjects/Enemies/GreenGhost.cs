using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrench.src.BaseClass;
using Wrench.src.Managers;
using Microsoft.Xna.Framework;
using Wrench.src.Helpers;
using Microsoft.Xna.Framework.Graphics;
using CustomAssets;

namespace Wrench.src.GameObjects.Enemies
{
    public class GreenGhost : Enemy
    {
        public GreenGhost(Game game, Vector3 pos, Level l)
            : base(game, pos, l)
        {
            life = 8;
            textures = new Texture2D[life];
            Damage = 4;
            this.level = l;
            this.position = pos;
            RotationSpeed = 0.2f;
            ForwardSpeed = 9f;
            boxMin = new Vector3(-0.235f, 0, -0.235f);
            boxMax = new Vector3(0.235f, 0.8f, 0.235f);
            boundingBox = new BoundingBox(position + boxMin, position + boxMax);

            textures[7] = ContentPreImporter.GetTexture("GreenGhost");
            textures[6] = ContentPreImporter.GetTexture("GreenGhost7");
            textures[5] = ContentPreImporter.GetTexture("GreenGhost6");
            textures[4] = ContentPreImporter.GetTexture("GreenGhost5");
            textures[3] = ContentPreImporter.GetTexture("GreenGhost4");
            textures[2] = ContentPreImporter.GetTexture("GreenGhost3");
            textures[1] = ContentPreImporter.GetTexture("GreenGhost2");
            textures[0] = ContentPreImporter.GetTexture("GreenGhost1");
            hurtSound = ContentPreImporter.GetSound("enemyHurt");

            billboard = new Billboard(game, textures[life - 1], Vector2.One / 2);
            billboard.Move(position + new Vector3(0, 0.25f, 0));
            billboard.ForceUpdate();
            target = pos;

            billboard.OverrideFog(GlobalSettings.FogEnabled, GlobalSettings.FogColor, GlobalSettings.FogStart, GlobalSettings.FogEnd * 2.1f);
        } 
    }
}
