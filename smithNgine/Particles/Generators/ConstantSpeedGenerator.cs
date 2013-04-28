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
    /// Generates a initial speed (float) for the particle
    /// The given speed is always used for new particles
    /// </summary>
    [Serializable]
    public class ConstantSpeedGenerator : ConstantPropertyGenerator
    {
        #region Constructors
        public ConstantSpeedGenerator()
        {
        }

        public ConstantSpeedGenerator(float speed)
            : base(speed)
        {
        }
        #endregion

        #region Method from base class
        public override void Apply(Particle p)
        {
            p.InitialSpeed = Value;
        }
        #endregion
    }
}
