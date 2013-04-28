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

    class ConstantOpacityGenerator : PropertyGenerator
    {
        public float Opacity { get; set; }

        public ConstantOpacityGenerator()
        {
            Opacity = 1.0f;
        }

        public ConstantOpacityGenerator(float opacity)
        {
            Opacity = opacity;
        }

        public override void Apply(Particle p)
        {
            p.InitialOpacity = Opacity;
            p.Opacity = Opacity;
        }
    }
}
