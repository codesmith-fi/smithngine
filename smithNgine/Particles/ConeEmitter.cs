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
    using Codesmith.SmithNgine.MathUtil;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Implements a cone emitter
    /// </summary>
    public class ConeEmitter : ParticleEmitter
    {
        /// <summary>
        /// Get or set the spread angle (Radians)
        /// <value>Spread angle in radians</value>
        /// </summary>
        public float Spread
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs the emitter with position and spread angle
        /// </summary>
        /// 
        /// <example>
        ///     // Emitter will spawn particles in cone of 45 degrees spread.
        ///     ParticleEmitter em = new ConeEmitter(Vector2.Zero, MathHelper.ToRadians(45));
        /// </example>
        /// <param name="position"></param>
        /// <param name="spread"></param>
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
            float spr = Interpolations.LinearInterpolate(
                Rotation - Spread / 2, 
                Rotation + Spread / 2, 
                random.NextFloat());
            p.LinearVelocity = new Vector2(
                MathFunctions.Sin(spr) * p.InitialSpeed, 
                -MathFunctions.Cos(spr) * p.InitialSpeed);
        }
    }
}
