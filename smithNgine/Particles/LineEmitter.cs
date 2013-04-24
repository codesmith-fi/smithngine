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
    public class LineEmitter : ParticleEmitter
    {
        private Vector2 startVector;
        private Vector2 endVector;

        public LineEmitter(Vector2 lineStart, Vector2 lineEnd)
            : base(lineStart)
        {
            startVector = lineStart;
            endVector = lineEnd;
            Name = "LineEmitter";
        }

        protected override void GenerateParticle(Particle p)
        {
            // Get a point along the line specified for this line emitter
            p.Position = Vector2.Lerp(startVector, endVector, (float)random.NextDouble());
            if (Configuration.Flags.HasFlag(EmitterModes.PositionRelative))
            {
                p.Position+=Effect.Position;
            }

            // Spawn randomly to any direction (should change probably to 
            // cast the particle to perpendicular direction from the line
            float direction = MathHelper.Lerp((float)-Math.PI, (float)Math.PI, (float)random.NextDouble());
            p.LinearVelocity = new Vector2(
                (float)Math.Sin(direction) * p.Speed, (float)-Math.Cos(direction) * p.Speed);
        }

    }
}
