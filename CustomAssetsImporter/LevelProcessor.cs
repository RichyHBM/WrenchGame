using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = CustomAssetsImporter.LoadedLevelFile;
using TOutput = CustomAssets.Level;
using CustomAssets;

namespace CustomAssetsImporter
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "CustomAssetsImporter.LevelProcessor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            char[] map = new char[input.width * input.height];

            int index = 0;
            foreach (String line in input.map)
            {
                char[] lineChar = line.ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    if (!String.IsNullOrEmpty(lineChar[j].ToString()))
                    {
                        map[index++] = lineChar[j];
                    }
                    else
                    {
                        map[index++] = '.';
                    }
                }
            }

            Level level = new Level();
            level.SetMapAndSize(map, input.width, input.height);
            return level;
        }
    }
}