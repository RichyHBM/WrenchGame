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
    // Updates all input types and keeps track of their states
    public class InputManager : IManager
    {
        MouseState oldMouseState, currentMouseState;
        KeyboardState oldKeyboardState, currentKeyboardState;
        GamePadState oldGamepadState, currentGamepadState;

        Vector2 mouseChange = Vector2.Zero;
        public Vector2 MouseChange { get { return mouseChange; } private set { } }
        Vector2 mousePositionToCenter = Vector2.Zero;
        public Vector2 MousePositionToCenter { get { return mousePositionToCenter; } private set { } }

        protected Game game;

        public enum MouseButton
        {
            Left,
            Right,
            Middle
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
                game.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F5))
                game.Exit();
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
        //Checks for a single press
        public bool HasBeenPressed(Buttons button)
        {
            return oldGamepadState.IsButtonUp(button) && currentGamepadState.IsButtonDown(button);
        }

        public void Draw(GameTime gameTime)
        {

        }
        //Checks for a single press
        public bool HasBeenPressed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        //Checks for button held down
        public bool IsDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        //Checks for button held down
        public bool IsDown(Buttons button)
        {
            return currentGamepadState.IsButtonDown(button);
        }
        //Retrieves the right stick
        public Vector2 RightThumbstick()
        {
            return currentGamepadState.ThumbSticks.Right;
        }
        //Checks for a mouse button click
        public bool HasBeenClicked(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released);
                case MouseButton.Right:
                    return (currentMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released);
                case MouseButton.Middle:
                    return (currentMouseState.MiddleButton == ButtonState.Pressed && oldMouseState.MiddleButton == ButtonState.Released);
            }
            return false;
        }
    }
}
