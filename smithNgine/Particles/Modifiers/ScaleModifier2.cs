/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
namespace Codesmith.SmithNgine.Particles.Modifiers
{
    using System;
    using Codesmith.SmithNgine.MathUtil;

    [Serializable]
    public class ScaleModifier2 : ParticleModifier
    {
        public float InitialScale { get; set; }
        public float FinalScale { get; set; }

        public ScaleModifier2(float initial, float final)
        {
            InitialScale = initial;
            FinalScale = final;
        }

        public override void Apply(Particle p)
        {
            p.Opacity = Interpolations.LinearInterpolate(
                InitialScale, FinalScale, p.TTLPercent);
        }
    }
}
