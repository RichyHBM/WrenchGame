using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wrench.src.BaseClasses
{
    //Methods that managers must implement
    public interface IManager
    {
        void Initialize(Game game);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
