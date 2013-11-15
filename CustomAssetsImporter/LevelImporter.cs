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
    [ContentImporter(".lev", DisplayName = "LevelImporter")]
    public class LevelImporter : ContentImporter<Level>
    {
        public override Level Import(string filename, ContentImporterContext context)
        {
            StreamReader reader = new StreamReader(File.OpenRead(filename));
            int levelWidth = 0;
            List<String> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (levelWidth < line.Length)
                    levelWidth = line.Length;
                lines.Add(line);
            }

            char[] map = new char[levelWidth * lines.Count];

            int index = 0;
            foreach (String line in lines)
            {
                char[] lineChar = line.ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    if (!String.IsNullOrEmpty(lineChar[j].ToString()))
                    {
                        map[index++] = lineChar[j];
                    }
                    else {
                        map[index++] = ' ';
                    }
                }
            }
            Level level = new Level();
            level.SetMapAndSize(map, levelWidth, lines.Count);
            return level;
        }
    }
}
