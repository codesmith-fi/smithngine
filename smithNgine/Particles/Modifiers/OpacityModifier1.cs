/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */

namespace Codesmith.SmithNgine.Particles.Modifiers
{
    using Codesmith.SmithNgine.MathUtil;

    public class OpacityModifier1 : ParticleModifier
    {
        public float FinalOpacity { get; set; }

        public OpacityModifier1(float initial, float final)
        {
            FinalOpacity = final;
        }

        public override void Apply(Particle p, float amount)
        {
            p.Opacity = Interpolations.LinearInterpolate(
                p.InitialOpacity, FinalOpacity, amount);
        }
    }
}
