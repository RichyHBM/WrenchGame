using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrench.src.BaseClasses;
using Microsoft.Xna.Framework;
using Wrench.src.Managers;
using Wrench.src.Helpers;

namespace Wrench.src.GameObjects.Pickups
{
    //Health pickup
    public class Health : Pickup
    {
        public Health(Game game, Vector3 pos)
            : base(game, pos)
        {
            //Assets for the health pickup
            texture = ContentPreImporter.GetTexture("HealthPack");
            pickupSound = ContentPreImporter.GetSound("Pickup");

            billboard = new Billboard(game, texture, Vector2.One / 2);
            billboard.Move(position);
            billboard.ForceUpdate();
        }

        //Add health to the player and remove the pickup
        public override void DoEffect(Player p)
        {
            if (p.Health == 100) return;
            pickupSound.Play();
            Alive = false;
            p.AddLife(10);
        }
    }
}
