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
    using Codesmith.SmithNgine.MathUtil;

    /// <summary>
    /// Generates a random speed (float) for the particle
    /// Speed is selected randomly from the given range.
    /// 
    /// Variation affects how the range is used. 
    /// 1.0 = full range is used
    /// 0.5 = half of the range is used
    /// 0.0 = always gives the range begin value
    /// 
    /// <remarks>
    /// Default range is 1.0f- 10.0f, with full variation (1.0f)
    /// </remarks>
    /// </summary>
    public class RandomSpeedGenerator : PropertyGenerator
    {
        #region Fields
        private PseudoRandom random = new PseudoRandom();
        #endregion

        #region Properties
        public float SpeedRangeStart { get; set; }
        public float SpeedRangeEnd { get; set; }
        public float Variation { get; set; }
        #endregion

        #region Constructors
        public RandomSpeedGenerator() : this(1.0f, 10.0f, 1.0f)
        {             
        }

        public RandomSpeedGenerator(float initial, float end, float variation)
        { 
        }
        #endregion

        #region Method from base class
        public override void Apply(Particle p)
        {
            p.InitialSpeed = Interpolations.LinearInterpolate(
                SpeedRangeStart, SpeedRangeEnd, random.NextFloat() * Variation);
        }
        #endregion
    }
}
