/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;

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
