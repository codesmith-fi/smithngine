/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Codesmith.SmithNgine.Primitives;

namespace Codesmith.SmithNgine.Particles
{
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
//            float angle = MathHelper.Lerp((float)-Math.PI, (float)Math.PI, (float)random.NextDouble());
            float angle = (float)random.NextDouble() * MathHelper.TwoPi;

            emitCircle.Position = Position;
            if (Configuration.Flags.HasFlag(EmitterCastStyle.RandomPosition))
            {
                p.Position = emitCircle.GetRandomContainedPoint(random);
            }
            else
            {
                p.Position = emitCircle.GetPoint(angle);                
            }

            if (Configuration.Flags.HasFlag(EmitterCastStyle.RandomDirection))
            {
                angle = (float)random.NextDouble() * MathHelper.TwoPi;
            }

            // This causes particles to go away along the normal line in this point
            p.LinearVelocity = new Vector2((float)Math.Sin(angle) * p.Speed, (float)Math.Cos(angle) * p.Speed);
        }
    }
}
