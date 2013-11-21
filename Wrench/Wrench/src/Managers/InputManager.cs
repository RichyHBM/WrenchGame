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


namespace Wrench.src.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputManager : IManager
    {
        bool needsMatrixUpdate = false;
        MouseState oldMouseState, currentMouseState;
        KeyboardState oldKeyboardState, currentKeyboardState;
        GamePadState oldGamepadState, currentGamepadState;

        Vector2 mouseChange = Vector2.Zero;
        public Vector2 MouseChange { get { return mouseChange; } private set { } }
        Vector2 mousePositionToCenter = Vector2.Zero;
        public Vector2 MousePositionToCenter { get { return mousePositionToCenter; } private set { } }

        protected Game game;

        public void SetMatrixUpdate(bool matrixUpdate)
        {
            needsMatrixUpdate = matrixUpdate;
        }

        public void Initialize(Game game)
        {
            this.game = game;
            // TODO: Add your initialization code here
            currentMouseState = Mouse.GetState();
            oldMouseState = currentMouseState;
            currentKeyboardState = Keyboard.GetState();
            oldKeyboardState = currentKeyboardState;
            currentGamepadState = GamePad.GetState(PlayerIndex.One);
            oldGamepadState = currentGamepadState;

            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

            updateStates();


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Game1.MainGame.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F5))
                Game1.MainGame.Exit();

            if (needsMatrixUpdate)
                UpdateMatrix();
        }

        private void updateStates()
        {
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            oldGamepadState = currentGamepadState;
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            Point centerOfScreen = new Point(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
            mouseChange.X = centerOfScreen.X - currentMouseState.X;
            mouseChange.Y = centerOfScreen.Y - currentMouseState.Y;
            mousePositionToCenter += mouseChange;
        }

        public void Draw(GameTime gameTime)
        {

        }

        public bool HasBeenPressed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }

        public bool IsDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static void UpdateMatrix()
        {

        }
    }
}
