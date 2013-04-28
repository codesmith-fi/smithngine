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
    /// Generates a initial rotation (radians) for the particle
    /// The given rotation is always used for new particles
    /// </summary>
    [Serializable]
    public class ConstantRotationGenerator : ConstantPropertyGenerator
    {
        #region Constructors
        public ConstantRotationGenerator()
        {
        }

        public ConstantRotationGenerator(float rotation)
            : base(rotation)
        {
        }
        #endregion

        #region Method from base class
        public override void Apply(Particle p)
        {
            p.InitialRotation = Value;
            p.Rotation = p.InitialRotation;
        }
        #endregion
    }
}
