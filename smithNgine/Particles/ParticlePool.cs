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
    public class ParticlePool : IDisposable
    {
        #region Fields
        private const int PoolGrowTreshold = 100;
        private const int PoolGrowAmount = 1000;
        private Queue<Particle> cache;
        private int initialCacheSize;
        private int maximumCache;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set a particle in the pool.
        /// <value>Particle</value>
        /// </summary>
        public Particle Particle
        {
            get
            {
                return Get();
            }

            set
            {
                Insert(value);
            }

        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a cache for 1000 Particles and maximum cache 
        /// set to 10000 particles.
        /// </summary>
        public ParticlePool() : this(1000, 10000)
        {
        }

        /// <summary>
        /// Initializes a cache for Particles with initial amount
        /// and maximum cache size.
        /// </summary>
        /// <param name="initialCache"></param>
        /// <param name="maxLimit"></param>
        public ParticlePool(int initialCache, int maxLimit = 10000)
        {
            initialCacheSize = initialCache;
            maximumCache = maxLimit;
            cache = new Queue<Particle>(initialCacheSize);
            Grow(initialCacheSize);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Put a particle back to the cache if there is room
        /// Particle is set to defaults.
        /// </summary>
        /// <param name="p">A particle to be pooled</param>
        public void Insert(Particle p)
        {
            Debug.Assert(p != null, "Trying to add null Particle to ParticlePool");
            if (cache.Count < maximumCache)
            {
                p.Reset();
                cache.Enqueue(p);
            }
        }

        /// <summary>
        /// Get a particle from the pool
        /// </summary>
        /// <returns>A particle</returns>
        public Particle Get()
        {
            Particle p = null;
            // Need to add more particles if we are under the treshold
            if (cache.Count < PoolGrowTreshold)
            {
                Grow(PoolGrowAmount - cache.Count);
            }

            // Get a first free radical (err. particle) from the pool 
            p = cache.Dequeue();
            Debug.Assert(p != null, "ParticlePool retrieved a null Particle, thats certainly a free radical!:)");
            return p;
        }

        #region From base classes
        /// <summary>
        /// Disposes this
        /// </summary>
        public void Dispose()
        {
            cache.Clear();
        }
        #endregion
        #endregion

        #region Private methods
        private void Grow(int amount = PoolGrowAmount)
        {
            Debug.Assert(cache != null, "Particle List is null");
            for (int i = 0; i < amount; ++i)
            {
                cache.Enqueue(new Particle(null));
            }
        }
        #endregion
    }
}
