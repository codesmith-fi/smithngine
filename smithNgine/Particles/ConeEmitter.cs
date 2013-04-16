/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithNgine.Particles
{
    public class ConeEmitter : ParticleEmitter
    {
        public float Spread
        {
            get;
            set;
        }

        public ConeEmitter(Vector2 position, float spread)
            : base(position)
        {
            Spread = spread;
            Name = "ConeEmitter";
        }

        protected override void GenerateParticle(Particle p)
        {
            // Get a point along the line specified for this line emitter
            p.Position = Position;
            float spr = MathHelper.Lerp(Rotation - Spread / 2, Rotation + Spread / 2, (float)random.NextDouble());
            p.LinearVelocity = new Vector2((float)Math.Sin(spr) * p.Speed, (float)-Math.Cos(spr) * p.Speed);
            p.AngularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
        }
    }
}
