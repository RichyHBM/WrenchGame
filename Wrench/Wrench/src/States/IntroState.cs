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
using Wrench.src.BaseClasses;
using Wrench.src.Managers;


namespace Wrench.src.States
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    // Introduction splash screen
    public class IntroState : AState
    {
        public enum InnerState
        {
            Appearing,
            Showing,
            Fading
        }

        SpriteFont font;
        float alpha = 0;
        private float time = 0;

        protected InnerState state = InnerState.Appearing;

        public IntroState(Game game)
            : base(game)
        {

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //Make the state fade in and out, removing after certain amount of time
            switch (state)
            {
                case InnerState.Appearing:
                    if (alpha >= 254.0f)
                        state = InnerState.Showing;
                    alpha += (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.1f;
                    break;
                case InnerState.Showing:
                    if (time > 3000)
                        state = InnerState.Fading;
                    time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    break;
                case InnerState.Fading:
                    if (alpha <= 1.0f)
                        Manager.StateManager.RemoveState(this);
                    alpha -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.1f;
                    break;
            }

            alpha = MathHelper.Clamp(alpha, 0.0f, 255.0f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw state drawing a blank screen first
            spriteBatch.Begin();

            spriteBatch.Draw(backdrop, Game.GraphicsDevice.Viewport.Bounds, Color.Black);

            Vector2 center = new Vector2(Game.GraphicsDevice.Viewport.Bounds.Center.X,
                                         Game.GraphicsDevice.Viewport.Bounds.Center.Y);


            spriteBatch.DrawString(font, "Wrench", center, new Color((int)alpha, (int)alpha, (int)alpha), 0.0f, font.MeasureString("Wrench") / 2.0f, 1.0f, SpriteEffects.None, 1.0f);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Resume()
        {

        }

        public override void Pause()
        {

        }

        public override void Start()
        {
            font = ContentPreImporter.GetFont("LargeFont");
        }

        public override void Stop()
        {

        }
    }
}
