/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.MathUtil
{
    using System;

    /// <summary>
    /// Implements float based Trigonometric math functions
    /// 
    /// Saves the need for doing casting from double to float 
    /// every time system Math functions are used.
    /// 
    /// Also offers an extra abstraction layer between XNA and SmithNgine
    /// </summary>
    public static class MathFunctions
    {
        public static float Sin(float angle)
        {
            return (float)Math.Sin(angle);
        }

        public static float Asin(float d)
        {
            return (float)Math.Asin(d);
        }

        public static float Cos(float angle)
        {
            return (float)Math.Cos(angle);
        }

        public static float Acos(float d)
        {
            return (float)Math.Acos(d);
        }

        public static float Tan(float angle)
        {
            return (float)Math.Tan(angle);
        }

        public static float Atan(float d)
        {
            return (float)Math.Atan(d);
        }

        public static float Atan2(float x, float y)
        {
            return (float)Math.Atan2(x, y);
        }        
    }
}
