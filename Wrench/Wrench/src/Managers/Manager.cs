using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Wrench.src.BaseClasses;

namespace Wrench.src.Managers
{
    public static class Manager
    {
        static InputManager inputManager;
        public static InputManager InputManager
        {
            get { return inputManager; }
            private set { }
        }

        static MatrixManager matrixManager;
        public static MatrixManager MatrixManager
        {
            get { return matrixManager; }
            private set { }
        }

        static StateManager stateManager;
        public static StateManager StateManager
        {
            get { return stateManager; }
            private set { }
        }

        public static void Initialize()
        {
            inputManager = new InputManager();
            matrixManager = new MatrixManager();
            stateManager = new StateManager();

            inputManager.Initialize();
            matrixManager.Initialize();
            stateManager.Initialize();


        }

        public static void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            matrixManager.Update(gameTime);
            stateManager.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            inputManager.Draw(gameTime);
            matrixManager.Draw(gameTime);
            stateManager.Draw(gameTime);
        }
    }
}
