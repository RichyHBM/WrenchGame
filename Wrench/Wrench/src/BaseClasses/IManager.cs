using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wrench.src.BaseClasses
{
    public interface IManager
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);

    }
}
