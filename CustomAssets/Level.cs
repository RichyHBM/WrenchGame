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

namespace CustomAssets
{
    public class Level
    {
        public char[] Map;
        public int Size;

        public Level()
        {

        }

        public void SetMapAndSize(char[] mapArr, int size)
        {
            Map = mapArr;
            Size = size;
        }

        public char GetAt(int x, int y)
        {
            return Map[x + Size * y];
        }
    }
}
