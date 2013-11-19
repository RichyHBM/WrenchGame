using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

// TODO: replace this with the type you want to import.
using System.IO;
using CustomAssets;

using TOutput = CustomAssetsImporter.LoadedLevelFile;

namespace CustomAssetsImporter
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".lev", DisplayName = "LevelImporter", DefaultProcessor = "LevelProcessor")]
    public class LevelImporter : ContentImporter<TOutput>
    {
        public override TOutput Import(string filename, ContentImporterContext context)
        {
            StreamReader reader = new StreamReader(File.OpenRead(filename));
            int levelWidth = 0;
            List<String> lines = new List<string>();
            //Map size is dynamic, its guessed by the size of rows/amount of lines
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (levelWidth < line.Length)
                    levelWidth = line.Length;
                lines.Add(line);
            }
            //So these need to be passed over individually
            return new LoadedLevelFile(){
                width = levelWidth,
                height = lines.Count,
                map = lines
            };
        }
    }
}
