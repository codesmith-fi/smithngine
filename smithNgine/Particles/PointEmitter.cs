﻿/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Codesmith.SmithNgine.Particles
{
    /// <summary>
    /// </summary>
    public class PointEmitter : ParticleEmitter
    {
        public PointEmitter(Vector2 position) : base(position)
        {
        }

        protected override void GenerateParticle(Particle p)
        {
            // Spawn randomly to any direction from the point
            float direction = MathHelper.Lerp((float)-Math.PI, (float)Math.PI, (float)random.NextDouble());
            p.Position = this.Position;
            p.LinearVelocity = new Vector2(
                (float)Math.Sin(direction) * p.Speed, (float)-Math.Cos(direction) * p.Speed);
        }

    }
}