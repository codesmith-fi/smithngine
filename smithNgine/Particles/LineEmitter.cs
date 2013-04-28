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
    using Codesmith.SmithNgine.Primitives;
    using Codesmith.SmithNgine.MathUtil;

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
            p.Position = line.Lerp(random.NextFloat());
            if (Configuration.Flags.HasFlag(EmitterModes.PositionRelative))
            {
                p.Position+=Effect.Position;
            }

            // Spawn randomly to any direction (should change probably to 
            // cast the particle to perpendicular direction from the line
            float direction = Rotation;
            if (Configuration.Flags.HasFlag(EmitterModes.RandomDirection))
            {
                direction = Interpolations.LinearInterpolate(
                    -MathConstants.PI, MathConstants.PI, random.NextFloat());
            }
            p.LinearVelocity = new Vector2(
                MathFunctions.Sin(direction) * p.InitialSpeed, 
                -MathFunctions.Cos(direction) * p.InitialSpeed);
        }

    }
}
