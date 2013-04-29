/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles.Generators
{
    using System;
    using Codesmith.SmithNgine.MathUtil;

    [Serializable]
    public class RandomDepthGenerator : RangePropertyGenerator
    {
        public RandomDepthGenerator()
        {
        }

        public RandomDepthGenerator(float start, float end, float variation)
            : base(start, end, variation)
        {
        }

        public override void Apply(Particle p)
        {
            p.InitialDepth = RandomValue;
            p.Depth = p.InitialDepth;
        }
    }
}
