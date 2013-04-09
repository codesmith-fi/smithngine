/**
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

        protected override Particle GenerateParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Particle p = new Particle(texture);

            p.Position = this.Position;
            p.Velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            p.Rotation = 0;
            p.AngularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            p.Color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            p.Alpha = (float)random.NextDouble();
            p.Scale = (float)random.NextDouble();
            p.TimeToLive = TimeSpan.FromSeconds(1.0f + random.NextDouble() * 2);

            return p;
        }

    }
}
