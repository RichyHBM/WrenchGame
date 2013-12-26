using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

// TODO: replace this with the type you want to write out.
using TWrite = CustomAssets.Level;

namespace CustomAssetsImporter
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class LevelWritter : ContentTypeWriter<TWrite>
    {
        //Compiles the level into an xnb file
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write((Int32)value.Width);
            output.Write((Int32)value.Depth);
            for (int row = 0; row < value.Depth; row++)
            {
                for (int column = 0; column < value.Width; column++)
                {
                    output.Write(value.GetAt(column, row));
                }
            }

            output.Flush();
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "CustomAssets.LevelReader, CustomAssets";
        }
    }
}
