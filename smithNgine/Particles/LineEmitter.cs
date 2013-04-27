/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 */

namespace Codesmith.SmithNgine.Particles
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using Codesmith.SmithNgine.Primitives;

    /// <summary>
    /// Implements a Line emitter
    /// 
    /// Particles are spawned along the given line
    /// </summary>
    public class LineEmitter : ParticleEmitter
    {
        //private Vector2 startVector;
        //private Vector2 endVector;
        private Line line;

        public LineEmitter(Line newLine) 
            : base(newLine.Start)
        {
            line = newLine;
            Name = "LineEmitter";
        }

        public LineEmitter(Vector2 lineStart, Vector2 lineEnd)
            : this(new Line(lineStart, lineEnd))
        {           
        }

        protected override void GenerateParticle(Particle p)
        {
            // Get a point along the line specified for this line emitter
            p.Position = line.Lerp((float)random.NextDouble());
            if (Configuration.Flags.HasFlag(EmitterModes.PositionRelative))
            {
                p.Position+=Effect.Position;
            }

            // Spawn randomly to any direction (should change probably to 
            // cast the particle to perpendicular direction from the line
            float direction = Rotation;
            if (Configuration.Flags.HasFlag(EmitterModes.RandomDirection))
            {
                direction = MathHelper.Lerp((float)-Math.PI, (float)Math.PI, (float)random.NextDouble());
            }
            p.LinearVelocity = new Vector2(
                (float)Math.Sin(direction) * p.Speed, (float)-Math.Cos(direction) * p.Speed);
        }

    }
}
