/**
 * SmithNgine Game Framework
 * 
 * Copyright (C) 2013 by Erno Pakarinen / Codesmith (www.codesmith.fi)
 * All Rights Reserved
 * 
 * For licensing terms, see License.txt which reflects to the current license
 * of this framework.
 */
namespace Codesmith.SmithNgine.Particles
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Codesmith.SmithNgine.Datatypes;

    /// <summary>
    /// Tries to implement a pool for preallocated particles.
    /// 
    /// Pool has 
    /// - maximum size (should not grow over this)
    /// - GrowTreshold (more particles are added to the pool 
    ///   when there are less particles than this in the pool)
    /// - GrowAmount (how many particles are added when pool needs to grow)
    /// 
    /// If enabled in the ParticleSystem, pool is used.
    /// 
    /// Use Get() to get a particle out from the pool
    /// Use Insert() to put a particle back to the pool
    /// Or you can also use the Particle property get/set.
    /// </summary>
    public class ParticlePool : ObjectPool<Particle>
    {
        #region Constructors
        /// <summary>
        /// Initializes a cache for Particles with initial amount
        /// and maximum cache size.
        /// </summary>
        /// <param name="initialCache"></param>
        /// <param name="maxLimit"></param>
        public ParticlePool(int initialCache, int maxLimit)
            : base(initialCache, maxLimit)
        {
        }
        #endregion
    }
}
