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
using Wrench.src.Managers;
using Wrench.src.States;
using CustomAssets;
using Wrench.src;

namespace Wrench
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SoundEffectInstance backgroundMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 576;
            graphics.PreferredBackBufferWidth = 1024;

            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GlobalSettings.Difficulty = GlobalSettings.DifficultyEnum.Easy;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentPreImporter.Initialize(this);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            src.Helpers.DebugShapeRenderer.Initialize(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            //Add the intro state and the menu state
            Manager.Initialize(this);
            Manager.StateManager.PushState(new MainMenuState(this));
            Manager.StateManager.PushState(new IntroState(this));
            //Load assets
            font = ContentPreImporter.GetFont("TextFont");
            backgroundMusic = ContentPreImporter.GetSound("Nerves").CreateInstance();  // Put the name of your song in instead of "song_title"
            backgroundMusic.Play();

#if VIEWDEBUG
            Manager.MatrixManager.SetFieldOfView(55);
#endif

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Loop the audio
            if (backgroundMusic.State == SoundState.Stopped)
                backgroundMusic.Play();
            Manager.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            Manager.Draw(gameTime);

#if DEBUG
            spriteBatch.Begin();
            if(gameTime.IsRunningSlowly)
                spriteBatch.DrawString(font, "Framerate: " + gameTime.ElapsedGameTime.TotalMilliseconds, new Vector2(10, 45), Color.Yellow);
            else
                spriteBatch.DrawString(font, "Framerate: " + gameTime.ElapsedGameTime.TotalMilliseconds, new Vector2(10, 45), Color.Green);
            spriteBatch.End();
#endif

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            src.Helpers.DebugShapeRenderer.Draw(gameTime, Manager.MatrixManager.View, Manager.MatrixManager.Perspective);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
