/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles.Generators
{
    using System;
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Base class for PropertyGenerators which provide a random float
    /// value within a given range with desired variation.
    /// <remarks>
    /// Default range is 1.0f - 1.0f, with full variation (1.0f)
    /// </remarks>
    /// </summary>
    [Serializable]
    public abstract class RangePropertyGenerator : PropertyGenerator
    {
        #region Fields
        private PseudoRandom random = new PseudoRandom();
        private float variation; 
        #endregion

        #region Properties
        /// <summary>
        /// Get the random generator
        /// </summary>
        public PseudoRandom Random { 
            get 
            {
                return random; 
            } 
        }

        /// <summary>
        /// The start value of the range
        /// </summary>
        public float RangeStart { get; set; }

        /// <summary>
        /// The end value of the range
        /// </summary>
        public float RangeEnd { get; set; }

        /// <summary>
        /// The variation, will be clamped to 0.0f .. 1.0f
        /// </summary>
        public float Variation
        {
            get { return variation; }
            set
            {
                variation = MathFunctions.Clamp(value);
            }
        }

        /// <summary>
        /// Get a random value from the range with the given variation
        /// If variation is 
        ///     1.0, full range is used 
        ///     0.0, only the start value is returned
        ///     0.5, half of the range is used
        /// </summary>
        public float RandomValue
        {
            get 
            {
                return Interpolations.LinearInterpolate(
                    RangeStart, RangeEnd, random.NextFloat() * Variation);
            }            
        }
        #endregion

        #region Constructors
        public RangePropertyGenerator()
            : this(1.0f, 1.0f, 1.0f)
        {
        }

        public RangePropertyGenerator(float start, float end, float variation)
        {
            RangeStart = start;
            RangeEnd = end;
            Variation = variation;
        }
        #endregion

        #region Method from base class
        public override void Apply(Particle p)
        {
        }
        #endregion
    }
}
