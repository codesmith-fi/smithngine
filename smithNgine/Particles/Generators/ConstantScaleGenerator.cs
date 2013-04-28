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
    class ConstantScaleGenerator : ConstantPropertyGenerator
    {
        public ConstantScaleGenerator()
        {
        }

        public ConstantScaleGenerator(float scale)
            : base(scale)
        {
        }

        public override void Apply(Particle p)
        {
            p.InitialScale = Value;
            p.Opacity = p.InitialScale;
        }
    }
}
