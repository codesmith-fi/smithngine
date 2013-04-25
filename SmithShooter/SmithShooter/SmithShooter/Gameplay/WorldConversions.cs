using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithShooter.Gameplay
{
    public static class WorldConversions
    {
        // 1 meter is 64 pixels
        public const float PlanetGravityStrenght = 0.09f;
        public const float PlanetGravityRadius = 15f;
        public const float WorldMeterInPixels = 64.0f;
        public const float PlanetDensity = 100.0f;
        public const float ShipDensity = PlanetDensity/4.0f;
        public const float BaseEnginePower = PlanetDensity * 7.0f;

        // Damage constants
        public const float ShipHullStrength = 1000f;
        public const float BulletBaseDamage = 100f;
        public const float PlanetCollisionDamage = 10000f;

        public static float ConvertFromWorld(float worldValue)
        {
            return worldValue * WorldMeterInPixels;
        }

        public static float ConvertFromPixels(float pixelValue)
        {
            return pixelValue / WorldMeterInPixels;
        }

        public static Vector2 ConvertFromPixels(Vector2 pixelValue)
        {
            return new Vector2(ConvertFromPixels(pixelValue.X), ConvertFromPixels(pixelValue.Y));
        }

        public static Vector2 ConvertFromWorld(Vector2 pixelValue)
        {
            return new Vector2(ConvertFromWorld(pixelValue.X), ConvertFromWorld(pixelValue.Y));
        }

    }
}
