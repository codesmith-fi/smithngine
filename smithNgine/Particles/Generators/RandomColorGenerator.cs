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
    public class RandomColorGenerator : PropertyGenerator
    {
        private PseudoRandom random = new PseudoRandom();

        public Color ColorStart { get; set; }
        public Color ColorEnd { get; set; }
        public float Variation { get; set; }

        public RandomColorGenerator()
            : this(Color.White, Color.White, 1.0f)
        {
        }

        public RandomColorGenerator(Color colorStart, Color colorEnd, float variation = 1.0f)
        {
            ColorStart = colorStart;
            ColorEnd = colorEnd;
            Variation = variation;
        }

        public override void Apply(Particle p)
        {
            p.Color = Color.Lerp(ColorStart, ColorEnd, random.NextFloat() * Variation);
        }
    }
}
