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
        public float FinalScale { get; set; }

        public ScaleModifier1(float final)
        {
            FinalScale = final;
        }

        public override void Apply(Particle p)
        {
            p.Scale = Interpolations.LinearInterpolate(
                p.InitialScale, FinalScale, p.TTLPercent);
        }
    }
}
