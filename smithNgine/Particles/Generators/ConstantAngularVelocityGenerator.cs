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

    [Serializable]
    class ConstantAngularVelocityGenerator : ConstantPropertyGenerator
    {
        public ConstantAngularVelocityGenerator()
        {
        }

        public ConstantAngularVelocityGenerator(float velocity)
            : base(velocity)
        {
        }

        public override void Apply(Particle p)
        {
            p.InitialAngularVelocity = Value;
            p.AngularVelocity = p.InitialAngularVelocity;
        }
    }
}
