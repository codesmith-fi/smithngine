using Microsoft.Xna.Framework;
using System;

namespace Codesmith.SmithNgine.MathUtil
{
    public static class MathValidator
    {
        /// <summary>
        /// This methods just ensures that a range is valid. It makes
        /// X always be lower than equal to Y. Switches the 
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Vector2 ValidateRange(Vector2 range)
        {
            return new Vector2(Math.Min(range.X, range.Y), Math.Max(range.X, range.Y));
        }
    }
}
