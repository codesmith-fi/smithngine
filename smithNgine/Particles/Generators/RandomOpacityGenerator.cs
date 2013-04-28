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
    using Codesmith.SmithNgine.MathUtil;

    class RandomOpacityGenerator : PropertyGenerator
    {
        private PseudoRandom random = new PseudoRandom();

        public override void Apply(Particle p)
        {
            p.InitialOpacity = random.NextFloat();
            p.Opacity = p.InitialOpacity;
        }
    }
}
