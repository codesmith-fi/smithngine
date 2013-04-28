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
    using System;
    using Codesmith.SmithNgine.MathUtil;

    [Serializable]
    public class ScaleModifier1 : ParticleModifier
    {
        public float Final { get; set; }

        public ScaleModifier1(float final)
        {
            Final = final;
        }

        public override void Apply(Particle p, float elapsedSeconds)
        {
            p.Scale = Interpolations.LinearInterpolate(
                p.InitialScale, Final, p.TTLPercent);
        }
    }
}
