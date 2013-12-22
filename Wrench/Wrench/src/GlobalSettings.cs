using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wrench.src
{
    public static class GlobalSettings
    {
        public enum DifficultyEnum { Easy, Medium, Hard };
        private static DifficultyEnum difficulty;
        public static DifficultyEnum Difficulty { 
            set 
            {
                switch (value)
                { 
                    case DifficultyEnum.Easy:
                        FogStart = 1.2f;
                        FogEnd = 2.5f;
                        EnemyFrequency = 3;
                        break;
                    case DifficultyEnum.Medium:
                        FogStart = 0.7f;
                        FogEnd = 1.8f;
                        EnemyFrequency = 2;
                        break;
                    case DifficultyEnum.Hard:
                        FogStart = 0.3f;
                        FogEnd = 1.0f;
                        EnemyFrequency = 1;
                        break;
                }
#if DEBUG
                FogEnabled = false;
#else
                FogEnabled = true;
#endif
                FogColor = Color.Black.ToVector3();
                difficulty = value; 

            } 
            get { return difficulty; } 
        }

        public static bool FogEnabled { private set; get; }
        public static Vector3 FogColor { private set; get; }
        public static float FogStart { private set; get; }
        public static float FogEnd { private set; get; }
        public static int EnemyFrequency { private set; get; }
    }
}
