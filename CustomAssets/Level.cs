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
        public int Width;
        public int Height;

        public Level()
        {

        }

        public void SetMapAndSize(char[] mapArr, int width, int height)
        {
            Map = mapArr;
            Width = width;
            Height = height;
        }

        public char GetAt(int x, int y)
        {
            return Map[x + Width * y];
        }
    }
}
