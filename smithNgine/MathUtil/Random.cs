/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.MathUtil
{
    using System;

    /// <summary>
    /// Extends Random class and gives new method for getting NextFloat()
    /// </summary>
    public class Random2 : Random
    {
        public Random2() 
            : base()
        {
        }

        public Random2(int seed)
            : base(seed)
        {
        }

        public float NextFloat()
        {
            return (float)NextDouble();
        }
    }
}

