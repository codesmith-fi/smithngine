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
    using Microsoft.Xna.Framework;

    [Serializable]
    public class LinearVelocityModifier : ParticleModifier
    {
        public Vector2 Gravity { get; set; }

        public LinearVelocityModifier(Vector2 gravity)
        {
            Gravity = gravity;
        }

        public override void Apply(Particle p, float elapsedSeconds)
        {
            p.LinearVelocity += Gravity * elapsedSeconds;
        }
    }
}
