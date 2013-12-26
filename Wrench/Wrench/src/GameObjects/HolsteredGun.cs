using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Wrench.src.Helpers;


namespace Wrench.src.GameObjects
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    //Gun billboard object
    public class HolsteredGun : Billboard
    {
        public HolsteredGun(Game game, Texture2D texture, Vector2 size)
            : base(game, texture, size)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        //Sets the position to be slightly off set from the player position and gives it a certain rotation
        public void SetPositionRotation(Vector3 pos, float rot)
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(rot);
            Vector3 right = Vector3.Transform(Vector3.Forward * 0.1f, Matrix.CreateRotationY(rot - MathHelper.ToRadians(90)));
            Vector3 forward = Vector3.Transform(Vector3.Forward * 0.19f, rotationMatrix);
            RotateZ(MathHelper.ToRadians(-5));
            RotateY(rot - MathHelper.ToRadians(90));
            Move(pos + forward + new Vector3(0, 0.42f, 0) + right);
        }
    }
}
