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
using CustomAssets;
using System.IO;


namespace Wrench.src.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public static class ContentPreImporter
    {
        static Dictionary<String, Texture2D> textureList = new Dictionary<string, Texture2D>();
        static Dictionary<String, Level> levelList = new Dictionary<string, Level>();
        static Dictionary<String, SpriteFont> fontList = new Dictionary<string, SpriteFont>();
        static Dictionary<String, SoundEffect> soundList = new Dictionary<string, SoundEffect>();

        public static void Initialize(Game game)
        {
            string contentPath = game.Content.RootDirectory;

            DirectoryInfo textureDir = new DirectoryInfo(contentPath + "/Textures/");
            DirectoryInfo levelDir = new DirectoryInfo(contentPath + "/Levels/");
            DirectoryInfo fontDir = new DirectoryInfo(contentPath + "/Fonts/");
            DirectoryInfo soundDir = new DirectoryInfo(contentPath + "/Audio/");

            foreach (FileInfo file in textureDir.GetFiles())
            {
                string fileName = file.Name.Replace(file.Extension, "");
                string fullPath = "Textures/" + fileName;
                textureList.Add(fileName, game.Content.Load<Texture2D>(fullPath ));
            }

            foreach (FileInfo file in levelDir.GetFiles())
            {
                string fileName = file.Name.Replace(file.Extension, "");
                string fullPath = "Levels/" + fileName;
                levelList.Add(fileName, game.Content.Load<Level>(fullPath));
            }

            foreach (FileInfo file in fontDir.GetFiles())
            {
                string fileName = file.Name.Replace(file.Extension, "");
                string fullPath = "Fonts/" + fileName;
                fontList.Add(fileName, game.Content.Load<SpriteFont>(fullPath));
            }

            foreach (FileInfo file in soundDir.GetFiles())
            {
                string fileName = file.Name.Replace(file.Extension, "");
                string fullPath = "Audio/" + fileName;
                soundList.Add(fileName, game.Content.Load<SoundEffect>(fullPath));
            }

        }

        public static Texture2D GetTexture(string name)
        {
            Texture2D t;
            if (!textureList.TryGetValue(name, out t))
                throw new Exception("Asset not preloaded");

            return t;
        }

        public static Level GetLevel(string name)
        {
            Level l;
            if (!levelList.TryGetValue(name, out l))
                throw new Exception("Asset not preloaded");

            return l;
        }

        public static SpriteFont GetFont(string name)
        {
            SpriteFont f;
            if (!fontList.TryGetValue(name, out f))
                throw new Exception("Asset not preloaded");

            return f;
        }

        public static SoundEffect GetSound(string name)
        {
            SoundEffect s;
            if (!soundList.TryGetValue(name, out s))
                throw new Exception("Asset not preloaded");

            return s;
        }

        public static List<String> LevelNames()
        {
            return levelList.Keys.ToList<String>();
        }
    }
}
