/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;

namespace Codesmith.SmithNgine.MathUtil
{
    /// <summary>
    /// Extends Random class and gives new method for getting NextFloat()
    /// </summary>
    class Random2 : Random
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
            return (float)base.NextDouble();
        }
    }
}

