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
    public class PseudoRandom : Random
    {
        public PseudoRandom() 
            : base()
        {
        }

        public PseudoRandom(int seed)
            : base(seed)
        {
        }

        public float NextFloat()
        {
            return (float)NextDouble();
        }
    }
}

