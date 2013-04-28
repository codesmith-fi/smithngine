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

    /// <summary>
    /// Base class for property generators
    /// A property generator applies initial values for particle properties.
    /// </summary>
    public abstract class PropertyGenerator
    {
        public PropertyGenerator()
        {
        }

        public abstract void Apply(Particle p);
    }
}
