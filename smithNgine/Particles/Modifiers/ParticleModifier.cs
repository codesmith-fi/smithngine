/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles.Modifiers
{
    using System;

    /// <summary>
    /// Abstract base class for all ParticleModifiers which apply
    /// some effect to a particle during its lifetime.
    /// </summary>
    public abstract class ParticleModifier
    {
        public ParticleModifier()
        {
        }

        /// <summary>
        /// Applies this modifier to the given particle with the given amount
        /// </summary>
        /// <param name="p">The particle to modify</param>
        /// <param name="elapsedSeconds">The amount of seconds passed since previous update</param>
        public abstract void Apply(Particle p, float elapsedSeconds);
    }
}
