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
    using Microsoft.Xna.Framework;

    [Serializable]
    public class ConstantColorGenerator : PropertyGenerator
    {
        public Color Color { get; set; }

        public ConstantColorGenerator() 
            : this(Color.White)
        {
        }

        public ConstantColorGenerator(Color color)
        {
            Color = color;
        }

        public override void Apply(Particle p)
        {
            p.Color = Color;
        }
    }
}
