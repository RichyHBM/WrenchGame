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
        public int Depth;

        public Level()
        {

        }

        public void SetMapAndSize(char[] mapArr, int width, int depth)
        {
            Map = mapArr;
            Width = width;
            Depth = depth;
        }

        public Char GetAt(int x, int y)
        {
            if (x + Width * y >= Map.Count())
                return '#';
            return Map[x + Width * y];
        }
    }
}
