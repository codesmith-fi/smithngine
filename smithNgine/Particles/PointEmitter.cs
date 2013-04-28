/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Implements a point emitter
    /// </summary>
    public class PointEmitter : ParticleEmitter
    {
        public PointEmitter(Vector2 position) : base(position)
        {
            Name = "PointEmitter";
        }

        protected override void GenerateParticle(Particle p)
        {
            // Spawn randomly to any direction from the point
            float direction = Rotation;
            if( Configuration.Flags.HasFlag( EmitterModes.RandomDirection ) )
            {
                direction = Interpolations.LinearInterpolate(
                    -MathConstants.PI, MathConstants.PI, random.NextFloat());
            }
            p.Position = this.Position;
            p.LinearVelocity = new Vector2(
                MathFunctions.Sin(direction) * p.InitialSpeed, 
                -MathFunctions.Cos(direction) * p.InitialSpeed);
        }
    }
}
