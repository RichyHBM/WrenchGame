using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wrench.src
{
    class GlobalSettings
    {
        public const bool FogEnabled = true;
        public static readonly Vector3 FogColor = Color.Black.ToVector3();
        public const float FogStart = 0.3f;
        public const float FogEnd = 1.5f;
    }
}
