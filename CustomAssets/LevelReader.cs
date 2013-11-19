using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// TODO: replace this with the type you want to read.
using TRead = CustomAssets.Level;

namespace CustomAssets
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class LevelReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            int width = input.ReadInt32();
            int height = input.ReadInt32();
            char[] mapData = new char[width * height];
            int index = 0;

            for (int row = 0; row < width; row++)
            {
                for (int column = 0; column < height; column++)
                {
                    mapData[index++] = input.ReadChar();
                }
            }

            Level l = new Level();
            l.SetMapAndSize(mapData, width, height);
            return l;
        }
    }
}
