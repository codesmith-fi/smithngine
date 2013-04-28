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
    using Codesmith.SmithNgine.Primitives;
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Implements a CircleEmitter
    /// 
    /// This emitter can be configured to spawn particles from the perimeter
    /// or from random point within the circle.
    /// <see cref="EmitterModes"/>
    /// </summary>
    public class CircleEmitter : ParticleEmitter
    {
        private Circle emitCircle;
        public CircleEmitter(Circle circle) : base(circle.Position)
        {
            emitCircle = circle;
            Name = "CircleEmitter";
        }

        protected override void GenerateParticle(Particle p)
        {
            float angle = (float)random.NextDouble() * MathHelper.TwoPi;

            emitCircle.Position = Position;
            if (Flags.HasFlag(EmitterModes.RandomPosition))
            {
                p.Position = emitCircle.GetRandomContainedPoint(random);
            }
            else
            {
                p.Position = emitCircle.GetPoint(angle);                
            }

            if (Flags.HasFlag(EmitterModes.RandomDirection))
            {
                angle = random.NextFloat() * MathConstants.PI;
            }

            // This causes particles to go away along the normal line in this point
            p.LinearVelocity = new Vector2(
                MathFunctions.Sin(angle) * p.InitialSpeed, 
                MathFunctions.Cos(angle) * p.InitialSpeed);
        }
    }
}
