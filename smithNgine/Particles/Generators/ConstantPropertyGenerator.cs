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
    /// Base class for PropertyGenerators which provide a 
    /// constant value
    /// </summary>
    [Serializable]    
    public class ConstantPropertyGenerator : PropertyGenerator
    {
        public float Value { get; set; }

        public ConstantPropertyGenerator()
        {
            Value = 1.0f;
        }

        public ConstantPropertyGenerator(float value)
        {
            Value = value;
        }

        public override void Apply(Particle p)
        {
        }
    }
}
