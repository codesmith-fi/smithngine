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
    [Serializable]
    public class RandomRotationGenerator : RangePropertyGenerator
    {
        #region Constructors
        public RandomRotationGenerator()
        {
        }

        public RandomRotationGenerator(float start, float end, float variation) 
            : base(start, end, variation)
        {
        }
        #endregion

        #region Method from base class
        public override void Apply(Particle p)
        {
            p.InitialRotation = RandomValue;
            p.Rotation = p.InitialRotation;
        }
        #endregion
    }
}
