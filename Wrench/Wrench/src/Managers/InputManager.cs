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

        public void SetMatrixUpdate(bool matrixUpdate)
        {
            needsMatrixUpdate = matrixUpdate;
        }

        public void Initialize()
        {
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
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
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
        }

        public void Draw(GameTime gameTime)
        {
            
        }

        public bool HasBeenPressed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }

        public static void UpdateMatrix()
        { 
        
        }
    }
}
