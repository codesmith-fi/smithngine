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
    class ConstantTTLGenerator : ConstantPropertyGenerator
    {
        public ConstantTTLGenerator()
        {
        }

        public ConstantTTLGenerator(float ttl)
            : base(ttl)
        {
        }

        public override void Apply(Particle p)
        {
            p.TTL = Value;
            p.TTLPercent = 0f;
        }
    }
}
