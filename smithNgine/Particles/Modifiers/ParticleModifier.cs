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
        /// <param name="amount">The amount, value between 0.0 and 1.0</param>
        public abstract void Apply(Particle p);
    }
}
