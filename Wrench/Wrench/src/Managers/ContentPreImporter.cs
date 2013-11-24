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
        static Dictionary<String, BasicEffect> effectList = new Dictionary<string, BasicEffect>();

        public static void Initialize(Game game)
        {
            string[] textureNames = {"Textures/bricks",
                                    "Textures/ceiling",
                                    "Textures/floor",
                                    "Textures/enemy",
                                    "Textures/GamePlay",
                                    "Textures/gun",
                                    "Textures/Intro",
                                    "Textures/Menu",
                                    "Textures/Win",
                                    "Textures/Lose"};

            string[] levelNames = {"level"};
            string[] fontNames = { "TextFont" };

            foreach (string name in textureNames)
            {
                textureList.Add(name, game.Content.Load<Texture2D>(name));
            }

            foreach (string name in levelNames)
            {
                levelList.Add(name, game.Content.Load<Level>(name));
            }

            foreach (string name in fontNames)
            {
                fontList.Add(name, game.Content.Load<SpriteFont>(name));
            }

            effectList.Add("DEFAULT", new BasicEffect(game.GraphicsDevice));
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

        public static BasicEffect GetEffect(string name)
        {
            BasicEffect be;
            if (!effectList.TryGetValue(name, out be))
                throw new Exception("Asset not preloaded");

            return be;
        }

        
    }
}
