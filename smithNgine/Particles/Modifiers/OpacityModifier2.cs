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
    public class OpacityModifier2 : ParticleModifier
    {
        public float InitialOpacity { get; set; }
        public float FinalOpacity { get; set; }

        public OpacityModifier2(float initial, float final)
        {
            InitialOpacity = initial;
            FinalOpacity = final;
        }

        public override void Apply(Particle p)
        {
            p.Opacity = Interpolations.LinearInterpolate(
                InitialOpacity, FinalOpacity, p.TTLPercent);
        }
    }
}
